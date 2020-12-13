using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Record.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class backController : ControllerBase
    {
        [HttpPost("okgrade")]
        public object OkGrade([FromBody] BackGradeJson bgj)
        {
            ResultReturn rr = new ResultReturn();
            string[] cookies = new readcookie().recognition(bgj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            if (cookies[0] == "1" && cookies[2] != "1")
            {
                string sqlstr = $"update  [userinfo] set grade=grade_flag,grade_flag=0,current_status=1,grade_time='{DateTime.Now.ToString()}'  where  [id] = @id and current_status=@current_status";
                Sql sc = new Sql();
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                sc.cmd.Parameters.Add(new SqlParameter("@current_status", 2));
                sc.cmd.Parameters.Add(new SqlParameter("@id", bgj.grade_id));
                if (sc.sqlExcute() > 0)
                {
                    rr.status = "1";
                    rr.message = "确认成功";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
                else
                {
                    rr.status = "0";
                    rr.message = "确认失败";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            else
            {
                rr.status = "0";
                rr.message = "用户验证失败";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }
        [HttpPost("account")]
        public object Account([FromBody] BackAccountJson baj)
        {
            ResultReturn rr = new ResultReturn();
            string[] cookies = new readcookie().recognition(baj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            if (cookies[0] == "1" && cookies[2] != "1")
            {
                string sqlstr = $"SELECT TOP {baj.pagesize} *  FROM  (  select ROW_NUMBER() OVER( order by createtime desc ) AS RowNumber, * from  userinfo  {(baj.status != "" ? " where current_status=" + baj.status : "")}   ) as A WHERE RowNumber >{baj.pagesize}*({baj.currentpage}-1)";
                Sql sc = new Sql();
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                DataTable dt = sc.sqlAdapter();
                List<AccountReturn> ars = new List<AccountReturn>();
                foreach (DataRow dr in dt.Rows)
                {
                    AccountReturn ar = new AccountReturn();
                    ar.company = dr["company"].ToString();
                    ar.grade = dr["grade"].ToString();
                    ar.current_status = dr["current_status"].ToString();
                    ar.status_time = dr["status_time"].ToString();
                    ar.principal = dr["principal"].ToString();
                    ar.contact_info = dr["contact_info"].ToString();
                    ar.company_addr = dr["company_addr"].ToString();
                    ar.certificate_pic = dr["certificate_pic"].ToString();
                    ar.bl_pic = dr["bl_pic"].ToString();
                    ar.createtime = dr["createtime"].ToString();
                    ars.Add(ar);
                }
                rr.status = "1";
                rr.message = "获取成功";
                rr.result = ars;
                rr.keychain = "";
                return rr;
            }
            else
            {
                rr.status = "0";
                rr.message = "用户验证失败";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }

        [HttpPost("addaccount")]
        public object AddAccount([FromBody] BackAddAccount baa)
        {
            ResultReturn rr = new ResultReturn();
            string[] cookies = new readcookie().recognition(baa.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            if (cookies[0] == "1" && cookies[2] != "1")
            {

                Sql sc = new Sql();
                string sqlstr = $"select id from [account] where username=@username ";
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                sc.cmd.Parameters.Add(new SqlParameter("@username", baa.account));
                string result = sc.sqlScalar();
                if (result != "")
                {
                    rr.status = "0";
                    rr.message = "账号已被注册";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
                else
                {
                    sc = new Sql();

                    bool rl = sc.DoTran(baa.account, baa.password, baa.company, baa.principal, baa.contact_info, baa.company_addr, baa.bl_pic);
                    if (rl == true)
                    {
                        rr.status = "1";
                        rr.message = "添加成功";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "添加失败";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                }
            }
            else
            {
                rr.status = "0";
                rr.message = "用户验证失败";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }


        [HttpPost("upuser")]
        public object Upuser([FromBody] BackUserInfoJson uij)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(uij.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1" && cookies[2] != "1")
                {
                    string stopstatus = "";
                    if (uij.status == "1")
                    {
                        stopstatus = ",current_status=3";
                    }
                    else if (uij.status == "0")
                    {
                        stopstatus = ",current_status=1,grade_flag=0";
                    }

                    Sql sc = new Sql();
                    string sqlstr = $"update  [userinfo]  set company=@company,principal=@principal,contact_info=@contactinfo,company_addr=@companyaddr,bl_pic=@blpic,createtime='{DateTime.Now.ToString()}'{stopstatus} where [id]=@id";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@company", uij.company));
                    sc.cmd.Parameters.Add(new SqlParameter("@principal", uij.principal));
                    sc.cmd.Parameters.Add(new SqlParameter("@contactinfo", uij.contact_info));
                    sc.cmd.Parameters.Add(new SqlParameter("@companyaddr", uij.company_addr));
                    sc.cmd.Parameters.Add(new SqlParameter("@blpic", uij.bl_pic));
                    sc.cmd.Parameters.Add(new SqlParameter("@id", uij.id));
                    if (sc.sqlExcute() > 0)
                    {
                        rr.status = "1";
                        rr.message = "更新成功";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "更新失败";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                }
                else
                {
                    rr.status = "0";
                    rr.message = "用户验证失败";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "更新异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }


        }

        [HttpPost("getgrade")]
        public object GetGrade([FromBody] BackUpGrade bg)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(bg.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1" && cookies[2] != "1")
                {
                    Sql sc = new Sql();
                    string sqlstr = $"select id,company,certificate_pic,grade_flag from [userinfo] where grade_flag!=0 ";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    DataTable dt = sc.sqlAdapter();
                    List<upgrade> ups = new List<upgrade>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        upgrade ar = new upgrade();
                        ar.id = Convert.ToInt32(dr["id"]);
                        ar.company = dr["company"].ToString();
                        ar.grade = dr["grade_flag"].ToString();
                        ar.certificate_pic = dr["certificate_pic"].ToString();
                        ups.Add(ar);
                    }
                    rr.status = "1";
                    rr.message = "获取数据成功";
                    rr.result = ups;
                    rr.keychain = "";
                    return rr;

                }
                else
                {
                    rr.status = "0";
                    rr.message = "用户验证失败";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "获取异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }

        [HttpPost("getrecord")]
        public object GetRecord([FromBody] BackRecordJson brj)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(brj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1" && cookies[2] != "1")
                {
                    List<BackRecordlist> mrdt = new List<BackRecordlist>();
                    string sqlstr = $"select * from backrecord where YEAR(createtime)='{Convert.ToDateTime(brj.time).ToString("yyyy")}'  and MONTH(createtime)='{Convert.ToDateTime(brj.time).ToString("MM")}'";
                    Sql sc = new Sql();
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                    DataTable dt = sc.sqlAdapter();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            BackRecordlist mrdo = new BackRecordlist();
                            mrdo.id = Convert.ToUInt16(dr["id"]);
                            mrdo.company=dr["company"].ToString();
                            mrdo.recordname = dr["recordname"].ToString();
                            mrdo.area = dr["area"].ToString();
                            mrdo.status = Convert.ToUInt16(dr["status"]);
                            mrdo.createtime = dr["createtime"].ToString();
                            mrdo.details = dr["details"].ToString() == "" ? (new MyRecord_detail()) : LitJson.JsonMapper.ToObject<MyRecord_detail>(dr["details"].ToString());
                            mrdt.Add(mrdo);
                        }
                        rr.status = "1";
                        rr.message = "获取成功";
                        rr.result = mrdt;
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "1";
                        rr.message = "获取为空";
                        rr.result = mrdt;
                        rr.keychain = "";
                        return rr;

                    }

                }
                else
                {
                    rr.status = "0";
                    rr.message = "用户验证失败";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "获取异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }

        [HttpPost("uprecord")]
        public object Upuser([FromBody] BackUpRecordJson uij)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(uij.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1" && cookies[2] != "1")
                {
                    string stopstatus = "";
                    if (uij.status == "1")
                    {
                        stopstatus = "1";
                    }
                    else if (uij.status == "2")
                    {
                        stopstatus = "2";
                    }

                    Sql sc = new Sql();
                    string sqlstr = $"update  [myrecord]  set status=@status  where [id]=@id";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@id", uij.id));
                    sc.cmd.Parameters.Add(new SqlParameter("@status", uij.id));
                    if (sc.sqlExcute() > 0)
                    {
                        rr.status = "1";
                        rr.message = "提交成功";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "提交失败";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                }
                else
                {
                    rr.status = "0";
                    rr.message = "用户验证失败";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "提交异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }


        }





    }
}
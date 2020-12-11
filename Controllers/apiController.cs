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
    public class apiController : ControllerBase
    {
        [HttpPost("login")]
        public object Login([FromBody] LoginJson ljs)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                Sql sc = new Sql();
                string sqlstr = "SELECT top 1 [id],[limit] FROM [account]  where [username]=@username and [password]=@password";
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                sc.cmd.Parameters.Add(new SqlParameter("@username", ljs.account));
                sc.cmd.Parameters.Add(new SqlParameter("@password", ljs.password));
                DataTable dt = sc.sqlAdapter();
                if (dt.Rows.Count == 0)
                {
                    rr.status = "0";
                    rr.message = "不存在此用户";
                    rr.result = "";
                    return rr;
                }
                string userid = dt.Rows[0]["id"].ToString();
                string limit = dt.Rows[0]["limit"].ToString();
                //用户存在
                if (!string.IsNullOrEmpty(userid))
                {
                    //客户端ip#账号id#时间戳
                    string cookiestr = new setcookie().DesEncrypt($"{Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()}#{userid}#{DateTime.Now.ToString()}#{limit}");
                    sc = new Sql();
                    sqlstr = "update  account  set cookie=@cookie where [id]=@id";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@cookie", cookiestr));
                    sc.cmd.Parameters.Add(new SqlParameter("@id", userid));
                    if (sc.sqlExcute() > 0)
                    {
                        rr.status = "1";
                        rr.message = "登陆成功";
                        rr.result = "";
                        rr.keychain = cookiestr;
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "登陆失败";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }

                }
                else
                {
                    rr.status = "0";
                    rr.message = "不存在此用户";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }

            }
            catch
            {
                rr.status = "0";
                rr.message = "登陆异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }

        [HttpPost("sign")]
        public object Sign([FromBody] SignJson sjs)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                Sql sc = new Sql();
                string sqlstr = $"select id from [account] where username=@username ";
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                sc.cmd.Parameters.Add(new SqlParameter("@username", sjs.account));
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

                    bool rl = sc.DoTran(sjs.account, sjs.password, sjs.company,"","","","");
                    if (rl == true)
                    {
                        rr.status = "1";
                        rr.message = "注册成功";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "注册失败";
                        rr.result = "";
                        rr.keychain = "";
                        return rr;
                    }
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "注册异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }
        [HttpPost("getuser")]
        public object Getuser([FromBody] GetUserInfo gui)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(gui.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1")
                {
                    Sql sc = new Sql();
                    string sqlstr = "select top 1 id,company,grade,current_status,principal,contact_info,company_addr,bl_pic,certificate_pic from [userinfo] where  userid=@userid";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                    DataTable dt = sc.sqlAdapter();
                    if (dt.Rows.Count > 0)
                    {
                        UserInfoReturn uif = new UserInfoReturn();
                        uif.id = Convert.ToInt16(dt.Rows[0]["id"]);
                        uif.company = dt.Rows[0]["company"].ToString();
                        uif.grade = dt.Rows[0]["grade"].ToString();
                        uif.current_status = dt.Rows[0]["current_status"].ToString();
                        uif.principal = dt.Rows[0]["principal"].ToString();
                        uif.contact_info = dt.Rows[0]["contact_info"].ToString();
                        uif.company_addr = dt.Rows[0]["company_addr"].ToString();
                        uif.bl_pic = dt.Rows[0]["bl_pic"].ToString();
                        uif.certificate_pic = dt.Rows[0]["certificate_pic"].ToString();

                        rr.status = "1";
                        rr.message = "获取成功";
                        rr.result = uif;
                        rr.keychain = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "未获取到数据";
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
                rr.message = "获取异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }

        }


        [HttpPost("upuser")]
        public object Upuser([FromBody] UserInfoJson uij)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(uij.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1")
                {
                    Sql sc = new Sql();
                    string sqlstr = $"update  [userinfo]  set company=@company,principal=@principal,contact_info=@contactinfo,company_addr=@companyaddr,bl_pic=@blpic,createtime='{DateTime.Now.ToString()}' where [id]=@id and userid=@userid";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@company", uij.company));
                    sc.cmd.Parameters.Add(new SqlParameter("@principal", uij.principal));
                    sc.cmd.Parameters.Add(new SqlParameter("@contactinfo", uij.contact_info));
                    sc.cmd.Parameters.Add(new SqlParameter("@companyaddr", uij.company_addr));
                    sc.cmd.Parameters.Add(new SqlParameter("@blpic", uij.bl_pic));
                    sc.cmd.Parameters.Add(new SqlParameter("@id", uij.id));
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
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

        [HttpPost("upgrade")]
        public object UpGrade([FromBody] GradeJson gj)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(gj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1")
                {
                    Sql sc = new Sql();
                    string sqlstr = $"select gra.id from (select id,grade from  [userinfo]  where  userid=@userid) as gra where gra.grade<@grade";
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                    sc.cmd.Parameters.Add(new SqlParameter("@grade", gj.apply_grade));
                    if (sc.sqlScalar() != "")//可升级
                    {
                        sqlstr = $"update  [userinfo] set   grade_flag=@grade_flag,current_status=@current_status  where  userid=@userid";
                        sc = new Sql();
                        sc.cmd = new SqlCommand();
                        sc.cmd.CommandText = sqlstr;
                        sc.cmd.Parameters.Add(new SqlParameter("@grade_flag", gj.apply_grade));
                        sc.cmd.Parameters.Add(new SqlParameter("@current_status", 2));
                        sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
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
                        rr.message = "请选择相应的级别";
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

        [HttpPost("rdls")]
        public object RecordList([FromBody] RecordListJson rjl)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                List<string> sqlwhere = new List<string>();
                if (rjl.recordname != "")
                {
                    sqlwhere.Add("recordname like @recordname");
                }
                if (rjl.area != "")
                {
                    sqlwhere.Add(" area like @area");
                }
                Sql sc = new Sql();
                string sqlstr = $"SELECT TOP {rjl.pagesize} [id],[recordname],[area],[createtime]  FROM  (  select ROW_NUMBER() OVER( order by createtime desc ) AS RowNumber, * from  recordlist  {(sqlwhere.Count > 0 ? " where " + string.Join(" and ", sqlwhere) : "")}   ) as A WHERE RowNumber >{rjl.pagesize}*({rjl.currentpage}-1)";
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                if (sqlstr.IndexOf("@recordname") > 0)
                    sc.cmd.Parameters.Add(new SqlParameter("@recordname", $"%{rjl.recordname}%"));
                if (sqlstr.IndexOf("@area") > 0)
                    sc.cmd.Parameters.Add(new SqlParameter("@area", $"%{rjl.area}%"));
                DataTable dt = sc.sqlAdapter();
                List<Recordlist> rds = new List<Recordlist>();
                foreach (DataRow dr in dt.Rows)
                {
                    Recordlist rd = new Recordlist();
                    rd.id = Convert.ToInt16(dr["id"]);
                    rd.recordname = dr["recordname"].ToString();
                    rd.area = dr["area"].ToString();
                    rd.createtime = dr["createtime"].ToString();
                    rds.Add(rd);
                }
                rr.status = "1";
                rr.message = "";
                rr.result = rds;
                rr.keychain = "";
                return rr;


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

        [HttpPost("uprc")]
        public object UpRecord([FromBody] RecordmJson rj)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                if (DateTime.Now < Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-06 00:00:00"))
                {
                    string[] cookies = new readcookie().recognition(rj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                    if (cookies[0] == "1")
                    {
                        string sqlstr = $"select count(id) from myrecord where userid=@userid and recordid in({string.Join(",", rj.recordid)})  and YEAR(createtime)='{DateTime.Now.ToString("yyyy")}'  and MONTH(createtime)='{DateTime.Now.ToString("MM")}' ";
                        Sql sc = new Sql();
                        sc.cmd = new SqlCommand();
                        sc.cmd.CommandText = sqlstr;
                        sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                        if (Convert.ToInt16(sc.sqlScalar()) == 0)
                        {
                            sqlstr = $"insert into [myrecord] (userid,recordid,status,details,createtime,recordname,area,recordflag) ( select  @userid,[id],0,'','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',recordname,area,0 from recordlist where  [id] in ({string.Join(",", rj.recordid)}) )";
                            sc = new Sql();
                            sc.cmd = new SqlCommand();
                            sc.cmd.CommandText = sqlstr;
                            sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));

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
                            rr.message = "重复提交";
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
                else
                {
                    rr.status = "0";
                    rr.message = "超过规定提交时间,不可提交!";
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

        [HttpPost("uprcs")]
        public object UpRecords([FromBody] RecordJson rj)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                if (DateTime.Now < Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-06 00:00:00"))
                {
                    string[] cookies = new readcookie().recognition(rj.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                    if (cookies[0] == "1")
                    {
                        string sqlstr = $"select id from myrecord where userid=@userid and recordname=@recordname and YEAR(createtime)='{DateTime.Now.ToString("yyyy")}'  and MONTH(createtime)='{DateTime.Now.ToString("MM")}'";
                        Sql sc = new Sql();
                        sc.cmd = new SqlCommand();
                        sc.cmd.CommandText = sqlstr;
                        sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                        sc.cmd.Parameters.Add(new SqlParameter("@recordname", rj.recordname));
                        if (string.IsNullOrEmpty(sc.sqlScalar()))
                        {
                            sqlstr = $"insert into [myrecord] (userid,recordid,status,details,createtime,recordname,area,recordflag) values  (@userid,0,0,'','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',@recordname,@area,1)  ";
                            sc = new Sql();
                            sc.cmd = new SqlCommand();
                            sc.cmd.CommandText = sqlstr;
                            sc.cmd.Parameters.Add(new SqlParameter("@recordname", rj.recordname));
                            sc.cmd.Parameters.Add(new SqlParameter("@area", rj.area));
                            sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
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
                            rr.message = "重复提交";
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
                else
                {
                    rr.status = "0";
                    rr.message = "超过规定提交时间,不可提交!";
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

        [HttpPost("myrecord")]
        public object MyRecord([FromBody] MyRecordJson mrd)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(mrd.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1")
                {
                    List<MyRecordlist> mrdt = new List<MyRecordlist>();
                    string sqlstr = $"select * from myrecord where userid=@userid and YEAR(createtime)='{Convert.ToDateTime(mrd.time).ToString("yyyy")}'  and MONTH(createtime)='{Convert.ToDateTime(mrd.time).ToString("MM")}'";
                    Sql sc = new Sql();
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                    DataTable dt = sc.sqlAdapter();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            MyRecordlist mrdo = new MyRecordlist();
                            mrdo.id = Convert.ToUInt16(dr["id"]);
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

        [HttpPost("upmr")]
        public object UpMyRecord_details([FromBody] UpRecordDetail urd)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                string[] cookies = new readcookie().recognition(urd.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                if (cookies[0] == "1")
                {
                    string sqlstr = $"update [myrecord] set details=@details where [id]=@id and userid=@userid";
                    Sql sc = new Sql();
                    sc.cmd = new SqlCommand();
                    sc.cmd.CommandText = sqlstr;
                    sc.cmd.Parameters.Add(new SqlParameter("@id", urd.id));
                    sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                    sc.cmd.Parameters.Add(new SqlParameter("@details", LitJson.JsonMapper.ToJson(urd.recordDetail)));
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

        [HttpPost("delmr")]
        public object DelMyRecord([FromBody] delrecord dl)
        {
            ResultReturn rr = new ResultReturn();
            try
            {
                if (DateTime.Now > Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00") || DateTime.Now < Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-06 00:00:00"))
                {
                    string[] cookies = new readcookie().recognition(dl.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                    if (cookies[0] == "1")
                    {
                        string sqlstr = $"delete from [myrecord]  where userid=@userid and [id] in ({string.Join(",",dl.id)})";
                        Sql sc = new Sql();
                        sc.cmd = new SqlCommand();
                        sc.cmd.CommandText = sqlstr;
                        sc.cmd.Parameters.Add(new SqlParameter("@userid", cookies[1]));
                        if (sc.sqlExcute() > 0)
                        {
                            rr.status = "1";
                            rr.message = "取消成功";
                            rr.result = "";
                            rr.keychain = "";
                            return rr;
                        }
                        else
                        {
                            rr.status = "0";
                            rr.message = "取消失败";
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
                else
                {
                    rr.status = "0";
                    rr.message = "当前时间,不可取消!";
                    rr.result = "";
                    rr.keychain = "";
                    return rr;
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "取消备案异常";
                rr.result = "";
                rr.keychain = "";
                return rr;
            }
        }

        

        





    }
}
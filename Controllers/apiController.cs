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
                string sqlstr = "SELECT [id] FROM [account]  where [username]=@username and [password]=@password";
                sc.cmd = new SqlCommand();
                sc.cmd.CommandText = sqlstr;
                sc.cmd.Parameters.Add(new SqlParameter("@username", ljs.account));
                sc.cmd.Parameters.Add(new SqlParameter("@password", ljs.password));
                string userid = sc.sqlScalar();
                //用户存在
                if (!string.IsNullOrEmpty(userid))
                {
                    //客户端ip#账号id#时间戳
                    string cookiestr = new setcookie().DesEncrypt($"{Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()}#{userid}#{DateTime.Now.ToString()}");
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
                    return rr;
                }

            }
            catch
            {
                rr.status = "0";
                rr.message = "登陆异常";
                rr.result = "";
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
                    return rr;
                }
                else
                {
                    sc = new Sql();

                    bool rl = sc.DoTran(sjs.account, sjs.password, sjs.company);
                    if (rl == true)
                    {
                        rr.status = "1";
                        rr.message = "注册成功";
                        rr.result = "";
                        return rr;
                    }
                    else
                    {
                        rr.status = "0";
                        rr.message = "注册失败";
                        rr.result = "";
                        return rr;
                    }
                }
            }
            catch
            {
                rr.status = "0";
                rr.message = "注册异常";
                rr.result = "";
                return rr;
            }
        }

        [HttpPost("upuser")]
        public object Upuser([FromBody] UserInfoJson UserInfoJson)
        {
            ResultReturn rr = new ResultReturn();
            if (new readcookie().recognition(UserInfoJson.keychain, Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()) == true)
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
    }
}
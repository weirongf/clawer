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

    }
}
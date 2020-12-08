using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Data.SqlClient;

namespace Record.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public string Login([FromBody] LoginJson ljs)
        {
           try
           {
           Sql sc=new Sql();
           string sqlstr="SELECT [id] FROM [account]  where [username]=@username and [password]=@password";
           sc.cmd=new SqlCommand();
           sc.cmd.CommandText=sqlstr;
           sc.cmd.Parameters.Add(new SqlParameter("@username",ljs.account));
           sc.cmd.Parameters.Add(new SqlParameter("@password",ljs.password));
           //用户存在
           if(!string.IsNullOrEmpty(sc.sqlScalar()))
           {
              return "登陆成果";
           }
           else
           {
              return "登陆失败";
           }

           }
           catch
           {
              return "登陆异常";
           }

                     
        }
        
    }
}
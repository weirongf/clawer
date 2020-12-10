using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Record
{
    /// <summary>
    /// cookie加密解密
    /// </summary>
    public class readcookie
    {
        /// <summary>
        /// cookie认证
        /// </summary>
        /// <param name="keychain"></param>
        /// <returns></returns>
        public string[] recognition(string keychain, string current_ip)
        {
            try
            {
                string cookiestr = new setcookie().DesDecrypt(keychain);
                ///验证是否符合
                string[] cookiestrs = cookiestr.Split("#");
                if (cookiestrs.Length == 4)//验证数据格式是否符合
                {
                    if (cookiestrs[0].Equals(current_ip))//验证IP是否符合
                    {
                        Sql s = new Sql();
                        s.cmd = new SqlCommand();
                        s.cmd.CommandText = "select [id] from [account] where [id]=@id and cookie=@cookie";
                        s.cmd.Parameters.Add(new SqlParameter("@id", cookiestrs[1]));
                        s.cmd.Parameters.Add(new SqlParameter("@cookie", keychain));
                        if (s.sqlScalar() != "")
                        {
                            return new string[3] { "1", cookiestrs[1],cookiestrs[3]};
                        }
                        else
                        {
                            return new string[3] { "0", "","" };
                        }

                    }
                    else
                    {
                        return new string[3] { "0", "","" };
                    }

                }
                else
                {
                    return new string[3] {"0","",""};
                }
            }
            catch
            {
                return new string[3] {"0","",""};
            }


        }
    }
}
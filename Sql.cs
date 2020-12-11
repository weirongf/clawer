using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;

namespace Record
{
    public class Sql
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public Sql()
        {
            try
            {
                string sqlcon = Config.GetSite("sqlcon");
                conn = new SqlConnection(sqlcon);
                if (conn.State.ToString() == "Closed") conn.Open();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable sqlAdapter()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = conn;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <returns></returns>
        public int sqlExcute()
        {
            int result = 0;
            try
            {
                cmd.Connection = conn;
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        /// <summary>
        /// 单个
        /// </summary>
        /// <returns></returns>
        public string sqlScalar()
        {
            string result = "";
            try
            {
                cmd.Connection = conn;
                result = Convert.ToString(cmd.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        //执行事务处理
        public bool DoTran(string username,string password,string company,string principal,string contactinfo,string companyaddr,string blpic)
        {
            SqlTransaction myTran;
            //创建一个事务
            myTran = conn.BeginTransaction();
            try
            {
                //从此开始，基于该连接的数据操作都被认为是事务的一部分
                //下面绑定连接和事务对象
                cmd=new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = myTran; //定位到pubs数据库
                // 执行第一条SQL语句
                cmd.CommandText = $"insert into account (username,[password],limit,cookie,createtime) output inserted.id  values (@username,@password,'1','','{DateTime.Now.ToString()}')";
                cmd.Parameters.Add(new SqlParameter("@username", username));   
                cmd.Parameters.Add(new SqlParameter("@password", password));       
                string userid=Convert.ToString(cmd.ExecuteScalar());

                if (userid=="")
                    throw new Exception();

                //执行第二条SQL语句

                cmd.CommandText = $"insert into userinfo ([userid],[company],[principal],[contact_info],[company_addr],[certificate_pic],[bl_pic],[createtime])   values (@userid,@company,@principal,@contactinfo,@companyaddr,'',@blpic,'{DateTime.Now.ToString()}')";
                cmd.Parameters.Add(new SqlParameter("@userid", userid));   
                cmd.Parameters.Add(new SqlParameter("@company", company));  
                cmd.Parameters.Add(new SqlParameter("@principal", principal));
                cmd.Parameters.Add(new SqlParameter("@contactinfo", contactinfo));
                cmd.Parameters.Add(new SqlParameter("@companyaddr", companyaddr)); 
                cmd.Parameters.Add(new SqlParameter("@blpic", blpic));
                if (cmd.ExecuteNonQuery() < 0)
                    throw new Exception();

                //提交事务
                myTran.Commit();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                myTran.Dispose();
                conn.Close();
            }
        }


    }
}

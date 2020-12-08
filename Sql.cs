using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;

namespace Record
{   
    public class Sql
    {
        public  SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public  Sql()
        {
          try
          {
            string sqlcon = Config.GetSite("sqlcon"); 
            conn=new SqlConnection(sqlcon);
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
          DataTable dt=new DataTable();
          try
          {
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
          int result=0;
          try
          {
            cmd.Connection=conn;
            result= cmd.ExecuteNonQuery();
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
        /// <summary>
        /// 单个
        /// </summary>
        /// <returns></returns>
        public string sqlScalar()
        {
          string result="";
          try
          {
             cmd.Connection=conn;
             result=Convert.ToString(cmd.ExecuteScalar());
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



    }
}

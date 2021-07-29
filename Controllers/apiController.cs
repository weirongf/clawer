using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Web;
using System.Text;

namespace Record.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class apiController : ControllerBase
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        [HttpPost("clawer")]
        public object clawer()
        {
            try
            {
                data.Add("cmdACT", "login");
                data.Add("furl", "");
                data.Add("loginid", "950100612");
                data.Add("rdpasswd", "2bc787266f445030119d6e5e97aca938");
                data.Add("loginCheckCode", "");
                string postData = "";
                foreach (string key in data.Keys)
                {
                    postData += HttpUtility.UrlEncode(key) + "="
                          + HttpUtility.UrlEncode(data[key]) + "&";
                }
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://elib.nblib.cn/SSO/main/OpacLoginServlet");
                myHttpWebRequest.Method = "POST";
                byte[] data_byte = Encoding.UTF8.GetBytes(postData);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.ContentLength = data_byte.Length;
                myHttpWebRequest.AllowAutoRedirect = false;
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.164 Safari/537.36";

                Stream requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(data_byte, 0, data_byte.Length);
                requestStream.Close();

                HttpWebResponse myHttpWebResponse;
                try
                {
                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                }
                catch (WebException e) 
                {
                    myHttpWebResponse =  (HttpWebResponse)e.Response;
                }
                string _setCookie = myHttpWebResponse.GetResponseHeader("Set-Cookie");

                string[] cookies = _setCookie.Split(";")[0].Split("=");
                myHttpWebResponse.Close();

                Cookie cookie = new Cookie(cookies[0], cookies[1]){Domain = "elib.nblib.cn" };
                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://elib.nblib.cn/SSO/goto/9");
                myHttpWebRequest.Method = "GET";
                CookieContainer cc = new CookieContainer();
                cc.Add(cookie);
                myHttpWebRequest.CookieContainer = cc;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                // Stream responseStream = myHttpWebResponse.GetResponseStream();
                // StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                // string pageContent = myStreamReader.ReadToEnd();
                return myHttpWebResponse.Cookies;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
                return "";

            }







    }
    }
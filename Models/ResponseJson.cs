namespace Record
{
    public class ResultReturn
    {
        /// <summary>
        /// 状态
        /// </summary>
        /// <value></value>
        public string status{get;set;}
        /// <summary>
        /// 返回信息
        /// </summary>
        /// <value></value>
        public string message{get;set;}
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain{get;set;}
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <value></value>
        public object result{get;set;} 
    }
    /// <summary>
    /// 登陆返回结果
    /// </summary>
    public class LoginReturn{
        public LoginReturn(string um)
        {
            username=um;
        }
        public string username{get;set;}
    }
}
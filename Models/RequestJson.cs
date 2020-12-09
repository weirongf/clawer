
namespace Record
{
    /// <summary>
    /// 登陆参数
    /// </summary>
    public class LoginJson
    {
        /// <summary>
        /// 账号
        /// </summary>
        /// <value></value>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        public string password { get; set; }
    }
    /// <summary>
    /// 注册参数
    /// </summary>
    public class SignJson
    {
        /// <summary>
        /// 账号密码
        /// </summary>
        /// <value></value>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        public string password { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string company { get; set; }
    }
    /// <summary>
    /// 用户基础数据提交
    /// </summary>
    public class UserInfoJson
    {
        /// <summary>
        /// 数据id
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string company { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        /// <value></value>
        public string principal { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <value></value>
        public string contact_info { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        /// <value></value>
        public string company_addr { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        /// <value></value>
        public string bl_pic { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }

    }




}
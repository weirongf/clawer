namespace Record
{
    public class ResultReturn
    {
        /// <summary>
        /// 状态
        /// </summary>
        /// <value></value>
        public string status { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        /// <value></value>
        public string message { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <value></value>
        public object result { get; set; }
    }
    /// <summary>
    /// 登陆返回结果
    /// </summary>
    public class LoginReturn
    {
        public LoginReturn(string um)
        {
            username = um;
        }
        public string username { get; set; }
    }
    /// <summary>
    /// 用户基础数据
    /// </summary>
    public class UserInfoReturn
    {
        public UserInfoReturn()
        {
            grade_des = "1.铜牌;2.银牌;3.金牌;";
            status_des = "1.正常;2.审核中;3.禁封期";
        }
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
        /// 等级(1.铜牌;2.银牌;3.金牌;)
        /// </summary>
        /// <value></value>
        public string grade { get; set; }
        /// <summary>
        /// 等级描述
        /// </summary>
        /// <value></value>
        public string grade_des { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <value></value>
        public string current_status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        /// <value></value>
        public string status_des { get; set; }
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
        /// 缴费凭证
        /// </summary>
        /// <value></value>
        public string certificate_pic { get; set; }

    }
    /// <summary>
    /// 备案列表
    /// </summary>
    public class Recordlist
    {
        /// <summary>
        /// 数据id
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// 备案名
        /// </summary>
        /// <value></value>
        public string recordname { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        /// <value></value>
        public string area { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public string createtime { get; set; }
    }
}
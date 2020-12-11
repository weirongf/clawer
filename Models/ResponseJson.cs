using System.Collections.Generic;

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
    /// <summary>
    /// 我的备案列表
    /// </summary>
    public class MyRecordlist
    {
        public MyRecordlist()
        {
            status_des = "0:未审核;1:已审核;2:取消;";
        }
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
        /// 备案地区
        /// </summary>
        /// <value></value>
        public string area { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <value></value>
        public int status { get; set; }
        /// <summary>
        /// 0:未审核;1:已审核;2:取消;
        /// </summary>
        /// <value></value>
        public string status_des { get; set; }
        /// <summary>
        /// 备案详细
        /// </summary>
        /// <value></value>
        public MyRecord_detail details { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public string createtime { get; set; }
    }
    /// <summary>
    /// 我的备份详细
    /// </summary>
    public class MyRecord_detail
    {
        public MyRecord_detail()
        {
            departName = "";
            departPeople = "";
            departStyle = "";
            projectPrice = "";
            projectEquip = "";
            projectTime = "";
            price = "";
            projectGrade = "";
            label = "";
            visits = new List<visit>();
        }
        /// <summary>
        /// 立项部门
        /// </summary>
        /// <value></value>
        public string departName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <value></value>
        public string departPeople { get; set; }
        /// <summary>
        /// 联系人方式
        /// </summary>
        /// <value></value>
        public string departStyle { get; set; }
        /// <summary>
        /// 项目预算
        /// </summary>
        /// <value></value>
        public string projectPrice { get; set; }
        /// <summary>
        /// 项目采购设备
        /// </summary>
        /// <value></value>
        public string projectEquip { get; set; }
        /// <summary>
        /// 预计采购时间
        /// </summary>
        /// <value></value>
        public string projectTime { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        /// <value></value>
        public string price { get; set; }
        /// <summary>
        /// 项目优先等级
        /// </summary>
        /// <value></value>
        public string projectGrade { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <value></value>
        public string label { get; set; }
        /// <summary>
        /// 拜访记录
        /// </summary>
        /// <value></value>
        public List<visit> visits { get; set; }
    }
    /// <summary>
    /// 单条拜访记录
    /// </summary>
    public class visit
    {
        /// <summary>
        /// 拜访记录
        /// </summary>
        /// <value></value>
        public string visitText { get; set; }
        /// <summary>
        /// 拜访图片
        /// </summary>
        /// <value></value>
        public string visitPic { get; set; }
        /// <summary>
        /// 拜访时间
        /// </summary>
        /// <value></value>
        public string visitTime { get; set; }
    }
    /// <summary>
    /// 账号列表
    /// </summary>
    public class AccountReturn
    {
        public AccountReturn()
        {
            company = "";
            grade = "1";
            grade_des = "1.铜牌;2.银牌;3.金牌;";
            current_status = "1";
            current_status_des = "1.正常;2.审核中;3.禁封期";
            status_time = "";
            principal = "";
            contact_info = "";
            company_addr = "";
            certificate_pic = "";
            bl_pic = "";
            createtime = "";
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string company { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        /// <value></value>
        public string grade { get; set; }
        /// <summary>
        /// 1.铜牌;2.银牌;3.金牌;
        /// </summary>
        /// <value></value>
        public string grade_des { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        /// <value></value>
        public string current_status { get; set; }
        /// <summary>
        /// 1.正常;2.审核中;3.禁封期
        /// </summary>
        /// <value></value>
        public string current_status_des { get; set; }
        /// <summary>
        /// 禁封结束期
        /// </summary>
        /// <value></value>
        public string status_time { get; set; }
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
        /// 缴费凭证
        /// </summary>
        /// <value></value>
        public string certificate_pic { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        /// <value></value>
        public string bl_pic { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <value></value>
        public string createtime { get; set; }
    }
    /// <summary>
    /// 代理列表
    /// </summary>
    public class upgrade
    {
        public upgrade()
        {
            grade_des = "1.铜牌;2.银牌;3.金牌";
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
        /// 缴费凭证
        /// </summary>
        /// <value></value>
        public string certificate_pic { get; set; }
        /// <summary>
        /// 申请等级
        /// </summary>
        /// <value></value>
        public string grade { get; set; }
        /// <summary>
        /// 等级描述
        /// </summary>
        /// <value></value>
        public string grade_des { get; set; }

    }
}
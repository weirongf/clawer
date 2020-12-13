
using System.Collections.Generic;

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
    /// 获取数据
    /// </summary>
    public class GetUserInfo
    {
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
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
    /// <summary>
    /// 代理升级
    /// </summary>
    public class GradeJson
    {
        /// <summary>
        /// 申请等级
        /// </summary>
        /// <value></value>
        public int apply_grade { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }



    /// <summary>
    /// 备案大厅
    /// </summary>
    public class RecordListJson
    {
        /// <summary>
        /// 备案名称
        /// </summary>
        /// <value></value>
        public string recordname { get; set; }
        /// <summary>
        /// 备案地区
        /// </summary>
        /// <value></value>
        public string area { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        /// <value></value>
        public int pagesize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        /// <value></value>
        public int currentpage { get; set; }
    }
    /// <summary>
    /// 官方备案
    /// </summary>
    public class RecordmJson
    {
        /// <summary>
        /// 备案id
        /// </summary>
        /// <value></value>
        public string[] recordid { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 手动备案
    /// </summary>
    public class RecordJson
    {
        /// <summary>
        /// 备案名称
        /// </summary>
        /// <value></value>
        public string recordname { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        /// <value></value>
        public string area { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 我的备案列表
    /// </summary>  
    public class MyRecordJson
    {
        /// <summary>
        /// 查询时间
        /// </summary>
        /// <value></value>
        public string time { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 我的备案详细提交
    /// </summary>
    public class UpRecordDetail
    {
        /// <summary>
        /// 数据id
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// 记录详细
        /// </summary>
        /// <value></value>
        public MyRecord_detail recordDetail { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 批量取消备案
    /// </summary>
    public class delrecord
    {
        /// <summary>
        /// 取消备案id
        /// </summary>
        /// <value></value>
        public string[] id { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }


    /// <summary>
    /// 管理员确认代理升级
    /// </summary>
    public class BackGradeJson
    {
        /// <summary>
        /// 申请等级
        /// </summary>
        /// <value></value>
        public string grade_id { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 账号查询
    /// </summary>
    public class BackAccountJson
    {
        /// <summary>
        /// 分类查询
        /// </summary>
        /// <value></value>
        public string status { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        /// <value></value>
        public int currentpage { get; set; }
        /// <summary>
        /// 一页数量
        /// </summary>
        /// <value></value>
        public int pagesize { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 添加账号
    /// </summary>
    public class BackAddAccount
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
        /// <summary>
        /// 公司名
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

    /// <summary>
    /// 管理员修改用户基础数据
    /// </summary>
    public class BackUserInfoJson
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
        /// 修改状态(0:正常;1:禁封期)
        /// </summary>
        /// <value></value>
        public string status { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }

    }

    public class BackUpGrade
    {
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }

    /// <summary>
    /// 备案列表
    /// </summary>  
    public class BackRecordJson
    {
        /// <summary>
        /// 查询时间
        /// </summary>
        /// <value></value>
        public string time { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <value></value>
        public string status{get;set;}
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }
    }
    /// <summary>
    /// 管理员数据
    /// </summary>
    public class BackUpRecordJson
    {
        /// <summary>
        /// 数据id
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// 修改状态(1:已审核;2:取消;)
        /// </summary>
        /// <value></value>
        public string status { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <value></value>
        public string keychain { get; set; }

    }
}
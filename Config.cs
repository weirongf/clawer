using Microsoft.Extensions.Configuration;
/// <summary>
/// 配置信息读取模型
/// </summary>
public static class Config
    {
        private static IConfigurationSection _appSection = null;
 
        /// <summary>
        /// api配置信息
        /// </summary>
        public static string AppSetting(string key)
        {
            string str = string.Empty;
            if (_appSection.GetSection(key) != null)
            {
                str = _appSection.GetSection(key).Value;
            }
            return str;
        }
        public static void SetAppSetting(IConfigurationSection section)
        {
            _appSection = section;
        }
        public static string GetSite(string apiName)
        {
            return AppSetting(apiName);
        }
    }
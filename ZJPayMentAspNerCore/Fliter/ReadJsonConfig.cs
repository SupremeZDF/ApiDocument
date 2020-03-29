using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ZJPayMentAspNerCore
{
    public class ReadJsonConfig
    {
        public static string JsonFileName = "BtsigSetting.json";

        public IConfiguration Configuration;

        public static ReadJsonConfig _ReadJsonConfig;

        /// <summary>
        /// 创建配置类并获取配置类
        /// </summary>
        /// <returns></returns>
        public static ReadJsonConfig GetConfig()
        {
            if (_ReadJsonConfig == null)
            {
                _ReadJsonConfig = new ReadJsonConfig();
                _ReadJsonConfig.Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(JsonFileName, optional: true, reloadOnChange: true)
                    .Build();
            }
            return _ReadJsonConfig;
        }

        /// <summary>
        /// 获取两级Node节点
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetSettingNode(string str)
        {
            try
            {
                if (str == null || str == "")
                    return "";
                return Configuration.GetSection("AppSetting").GetSection(str).Value;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取一级node节点
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetstairNode(string str)
        {
            try
            {
                if (str == null || str == "")
                    return "";
                return Configuration[str];
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

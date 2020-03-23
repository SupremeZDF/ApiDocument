using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Manage.Tools.NetCore.NLog
{

    public enum enum_logswitchtype
    {
        Http请求日志=1,
        管道消息日志=2
    }
    public class LogSwitchDto
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum_logswitchtype? LogType{get;set;}

        /// <summary>
        /// 匹配规则
        /// </summary>
        public string MatchRule { get; set; }
    }
}

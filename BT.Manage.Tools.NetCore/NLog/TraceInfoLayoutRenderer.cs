using System;
using System.Collections.Generic;
using System.Text;
using NLog.LayoutRenderers;
using Newtonsoft.Json;
using NLog;

namespace BT.Manage.Tools
{
    [LayoutRenderer("bttraceinfo")]
    public sealed class TraceInfoLayoutRenderer : LayoutRenderer
    {

        public string BTTraceInfo { get { return JsonConvert.SerializeObject( GetTraceLog()); } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(BTTraceInfo);

        }

        public ThreadLocalData GetTraceLog()
        {
            ThreadLocalData data = new ThreadLocalData();
            try

            {
                object o = BTCallContextData.GetData(BTCallContextData.LogTraceKey);
                if (o != null)
                    data = o as ThreadLocalData;
                else
                {
                    data = new ThreadLocalData() { TraceId = Guid.NewGuid().ToString("N"), StartTime = DateTime.Now, TraceSecondId = System.Environment.MachineName, FromUrl = "Localhost" };
                    //BTCallContextData.SetData("BTTrace", data);
                }
            }
            catch (Exception)
            {
                //
            }
            return data;
        }
    }
}

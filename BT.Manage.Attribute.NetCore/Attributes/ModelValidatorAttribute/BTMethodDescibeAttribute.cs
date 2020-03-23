using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 方法描述
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class BTMethodDescibeAttribute:System.Attribute
    {

        /// <summary>
        /// 方法输入类型
        /// </summary>
        public Type InParam { get; set; }

        /// <summary>
        /// 方法输出类型
        /// </summary>
        public Type OutParam { get; set; }
        /// <summary>
        /// 方法描述信息
        /// </summary>
        public string Decription { get; set; }
        /// <summary>
        /// 字段描叙信息
        /// </summary>
        public string ParaDecription { get; set; }

        /// <summary>
        /// 通过哪个服务转发到当前接口
        /// </summary>
        public string FrameForwardService { get; set; }

        public BTMethodDescibeAttribute()
        {

        }

    }
}

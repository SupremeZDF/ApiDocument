using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Document.AssemblyOperation
{
    public class AssemblyMethodInfo
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDescibe { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string MethodRequestType { get; set; }
        /// <summary>
        /// 描述特性
        /// </summary>
        public object MethodAttr { get; set; }
        /// <summary>
        /// 输入参数
        /// </summary>
        public Type InPara { get; set; }
        /// <summary>
        /// 输出参数
        /// </summary>
        public Type OutPara { get; set; }

        public Type FInPara { get; set; }

        public Type FoutPara { get; set; }
        /// <summary>
        /// 输入json
        /// </summary>
        public string InJson { get; set; }
        /// <summary>
        /// 输出json
        /// </summary>
        public string OutJson { get; set; }

        public int FIsAuthor { get; set; }
        /// <summary>
        /// 方法输入字段
        /// </summary>
        public List<AssemblyFiledInfo> InParamList { get; set; }
        /// <summary>
        /// 方法输出字段
        /// </summary>
        public List<AssemblyFiledInfo> OutParamList { get; set; }
        public string FrameForwardService { get; set; }

        public bool IsPost { get; set; }

        public string ControllerName { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Document.AssemblyOperation
{
    public class AssemblyFiledInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FiledName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string FiledType { get; set; }

        public string FileClassName { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string FiledDescript { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string FMoudleName { get; set; }
        /// <summary>
        /// 字段层级
        /// </summary>
        public int FiledLayer { get; set; }
        /// <summary>
        /// 字段左值
        /// </summary>
        public int FiledLeft { get; set; }
        /// <summary>
        /// 字段右值
        /// </summary>
        public int FiledRight { get; set; }
        /// <summary>
        /// 输入或者输出 1 输入 2 输出
        /// </summary>
        public int FiledInOrOut { get; set; }
        /// <summary>
        /// 标识输入参数是否是 ResultRequset 1 是
        /// </summary>
        public int FIsResultRequset { get; set; }
        /// <summary>
        /// 标识返回参数是 Result  1 是
        /// </summary>
        public int FIsResult { get; set; }
    }
}

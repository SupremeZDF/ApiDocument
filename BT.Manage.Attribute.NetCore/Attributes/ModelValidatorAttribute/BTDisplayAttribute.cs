using BT.Manage.Frame.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 提供字段描述信息
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTDisplayAttribute : BTBaseModelValidator
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ParaName { get; set; }
        /// <summary>
        /// 对应表模型名称
        /// </summary>
        public string TableModelName { get; set; }

        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            return null;
        }
    }
}

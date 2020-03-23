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
    /// 提供验证模块
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTModuleCodeAttribute : BTBaseModelValidator
    {
        private string modulecode;
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode
        {
            get { return modulecode; }
            set { modulecode = value; }
        }

        public BTModuleCodeAttribute(string _modulecode)
        {
            modulecode = _modulecode;
        }

        
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            return null;
        }

    }
}

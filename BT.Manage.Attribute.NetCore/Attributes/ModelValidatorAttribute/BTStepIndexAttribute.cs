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
    /// 提供第几期次验证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTStepIndexAttribute : BTBaseModelValidator
    {
        private int stepIndex;
        
        /// <summary>
        /// 字段验证期次
        /// </summary>
        public int StepIndex
        {
            get { return stepIndex; }
        }

        public BTStepIndexAttribute(int stepindex)
        {
            stepIndex = stepindex;
        }
       
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            return null;
        }
    }
}

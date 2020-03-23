using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BT.Manage.Frame.Base;

namespace BT.Manage.Attribute
{

    /// <summary>
    /// 身份证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTIDCardAttribute : BTBaseModelValidator
    {
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            return null;
        }
    }



    /// <summary>
    /// 电话号码
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTMobileAttribute : BTBaseModelValidator
    {
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            return null;
        }
    }
}

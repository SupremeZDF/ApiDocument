using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BT.Manage.Frame.Base;
using BT.Manage.Frame;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 提供手机号码验证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTPhoneNumberAttribute : BTBaseModelValidator
    {
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            bool b = true;
            var paratype = GetPropertyinfoType(p);
            if (paratype.ToLower() == "string")
            {
                string strvalue = (string)t;
                try
                {
                    strvalue.IsPhoneNumber(p.Name).Throw();
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(Message))
                        Message = ex.Message.Split('★')[0];
                    b = false;
                }

            }
            if (!b)
            {
                return new ValidationResult() { Member = p.Name, ModuleName = modulename, Message = Message };
            }
            else
                return null;
        }
    }
}

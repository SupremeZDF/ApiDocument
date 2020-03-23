using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BT.Manage.Frame.Base;
using BT.Manage.Frame;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 提供字符串的长度约束
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTStringLengthAttribute : BTBaseModelValidator
    {
        
        private int maximunLength;
        
        //
        // 摘要:
        //     获取或设置字符串的最大长度。
        //
        // 返回结果:
        //     字符串的最大长度。
        public int MaximumLength { get { return maximunLength; } }

      
        //
        // 摘要:
        //     获取或设置字符串的最小长度。
        //
        // 返回结果:
        //     字符串的最小长度。
        public int MinimumLength { get; set; }

        public BTStringLengthAttribute(int maxmumlength)
        {
            maximunLength = maxmumlength;
        }
        /// <summary>
        /// 实现验证方法
        /// </summary>
        /// <param name="p">验证属性</param>
        /// <param name="t">属性值</param>
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            bool b = true;
           //判断当前属性类型是否为string 或者stringbulider
            var filedType = GetPropertyinfoType(p);
            if (filedType.ToLower() == "string")
            {
                string paravalue = (string)t;
                try
                {
                    paravalue.CheckStringLength(MaximumLength, MinimumLength, paraname).Throw();
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

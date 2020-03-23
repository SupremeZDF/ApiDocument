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
    /// 提供正则表达式验证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTRegularExpressionAttribute : BTBaseModelValidator
    {
        private string _pattern { get; set; }
        /// <summary>
        /// 用户验证的正则表达式
        /// </summary>
        public string Pattern { get { return _pattern; } }

        public BTRegularExpressionAttribute(string pattern)
        {
            _pattern = pattern;
        }
        /// <summary>
        /// 重写验证函数
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public override ValidationResult Valid(PropertyInfo p, object t,string paraname,string modulename)
        {
            bool b = true;
            var filedType = GetPropertyinfoType(p);
            var canvalidType = new[] { "string", "int", "int32", "int16","int64","decimal","double","float","long" };
            if (canvalidType.Contains(filedType.ToLower()))
            {
                //验证
                try
                {
                    string paravalue = (string)t;
                    paravalue.CheckRegular(Pattern, paraname).Throw();
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

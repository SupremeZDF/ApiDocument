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
    /// 模型验证抽象基类
    /// </summary>
    public abstract class BTBaseModelValidator : System.Attribute
    {
        /// <summary>
        /// 错误提示消息
        /// </summary>
        public string Message { get; set; }
       
      

        public BTBaseModelValidator()
        {

        }

        public BTBaseModelValidator(string message)
        {
            Message = message;
        }
        /// <summary>
        /// 验证方法抽象
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        public abstract ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename);
        
        /// <summary>
        /// 获取属性类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual string GetPropertyinfoType(PropertyInfo p)
        {
            string paratype;
            if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                paratype = p.PropertyType.GetGenericArguments()[0].Name;
            }
            else
                paratype = p.PropertyType.Name;
            return paratype;
        }
    }
}

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
    /// 提供必须有值验证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTRequiredAttribute : BTBaseModelValidator
    {

        private bool allowstringempty = true;
        private bool allowguidempty = true;
        private bool allowzero = true;
        /// <summary>
        /// string 类型 是否允许为string.empty 默认 true 允许 置为false 不允许
        /// </summary>
        public bool AllowStringEmpty
        {
            get { return allowstringempty; }
            set { allowstringempty = value; }
        }
        /// <summary>
        /// guid 类型 是否允许为gui.empty 默认 true 允许 置为false 不允许
        /// </summary>
        public bool AllowGuidEmpty
        {
            get { return allowguidempty; }
            set { allowguidempty = value; }
        }
        /// <summary>
        /// int decimal 类型 是否允许为0 默认 true 允许 置为false 不允许
        /// </summary>
        public bool AllowZero
        {
            get { return allowzero; }
            set { allowzero = value; }
        }
        public BTRequiredAttribute()
        {
           
        }
        /// <summary>
        /// 实现验证方法
        /// </summary>
        /// <param name="p">验证属性</param>
        /// <param name="t">属性值</param>
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            bool b = true;
            try
            {
                string paratype = GetPropertyinfoType(p);
                t.CheckNotNull(paraname).Throw();
                switch (paratype.ToLower())
                {
                    case "string":
                        string sparavalue = (string)t;
                        if (!AllowStringEmpty)
                            sparavalue.CheckNotNullOrEmpty(paraname).Throw();
                        else
                            sparavalue.CheckNotNull(paraname).Throw();
                        break;
                    case "guid":
                        Guid gparavalue = (Guid)t;
                        if (AllowGuidEmpty)
                            gparavalue.CheckNotNull(paraname).Throw();
                        else
                        {
                            gparavalue.CheckNotEmpty(paraname).Throw();
                        }
                        break;
                    case "long":
                        long? lparavalue = (long?)t;
                        if (AllowZero)
                            lparavalue.CheckNotNull(paraname).Throw();
                        else
                            lparavalue.CheckNotNullAndNotIsZero(paraname).Throw();
                        break;
                    case "int":
                    case "int16":
                    case "int32":
                    case "int64":
                        int? iparavalue = (int?)t;
                        if (AllowZero)
                            iparavalue.CheckNotNull(paraname).Throw();
                        else
                            iparavalue.CheckNotNullAndNotIsZero(paraname).Throw();
                        break;
                    case "decimal":
                        decimal? dparavalue = (decimal?)t;
                        if (AllowZero)
                        {
                            dparavalue.CheckNotNull(paraname).Throw();
                        }
                        else
                            dparavalue.CheckNotNullAndNotIsZero(paraname).Throw();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(Message))
                    Message = ex.Message.Split('★')[0];
                b = false;
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

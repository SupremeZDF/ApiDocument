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
    /// 提供必须有值验证
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTSQLRequiredAttribute : BTBaseModelValidator
    {
         private bool allowstringempty = false;
        private bool allowguidempty = false;
        private bool allowzero = false;
        /// <summary>
        /// string 类型 是否允许为string.empty 默认 false 不允许 置为true 允许
        /// </summary>
        public bool AllowStringEmpty
        {
            get { return allowstringempty; }
            set { allowstringempty = value; }
        }
        /// <summary>
        /// guid 类型 是否允许为gui.empty 默认 false 不允许 置为true 允许
        /// </summary>
        public bool AllowGuidEmpty
        {
            get { return allowguidempty; }
            set { allowguidempty = value; }
        }
        /// <summary>
        /// int decimal 类型 是否允许为0 默认 false 不允许 置为true 允许
        /// </summary>
        public bool AllowZero
        {
            get { return allowzero; }
            set { allowzero = value; }
        }
        public BTSQLRequiredAttribute()
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
                t.CheckNotNull(paraname);
                switch (paratype.ToLower())
                {
                    case "string":
                        string sparavalue = (string)t;
                        if (!AllowStringEmpty)
                            sparavalue.CheckNotNullOrEmpty(paraname);
                        else
                            sparavalue.CheckNotNull(paraname);
                        break;
                    case "guid":
                        Guid gparavalue = (Guid)t;
                        if (AllowGuidEmpty)
                            gparavalue.CheckNotNull(paraname);
                        else
                        {
                            gparavalue.CheckNotEmpty(paraname);
                        }
                        break;
                    case "long":
                        long? lparavalue = (long?)t;
                        if (AllowZero)
                            lparavalue.CheckNotNull(paraname);
                        else
                            lparavalue.CheckNotNullAndNotIsZero(paraname);
                        break;
                    case "int":
                    case "int16":
                    case "int32":
                    case "int64":
                        int? iparavalue = (int?)t;
                        if (AllowZero)
                            iparavalue.CheckNotNull(paraname);
                        else
                            iparavalue.CheckNotNullAndNotIsZero(paraname);
                        break;
                    case "decimal":
                        decimal? dparavalue = (decimal?)t;
                        if (AllowZero)
                        {
                            dparavalue.CheckNotNull(paraname);
                        }
                        else
                            dparavalue.CheckNotNullAndNotIsZero(paraname);
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

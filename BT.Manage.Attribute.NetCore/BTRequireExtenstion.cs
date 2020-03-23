using BT.Manage.Frame.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BT.Manage.Attribute
{
    public static class BTRequireExtenstion
    {
        /// <summary>
        /// BTRequired校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result RequiredValidate<T>(this T o)
        {
            Result rz = new Result();
            List<ValidationResult> results = new List<ValidationResult>();
            var type = o.GetType();
            List<string> proNamelist = new List<string>();

            //获取属性
            PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //propertyinfo = propertyinfo.Where(p => proNamelist.Contains(p.Name)).ToArray();
            //属性验证
            foreach (PropertyInfo p in propertyinfo)
            {
                string paraname = string.Empty;
                string modulename = string.Empty;
                var paraValue = p.GetValue(o);
                //获取BTDisplay
                var attrdis = p.GetCustomAttribute(typeof(BTDisplayAttribute));
                if (attrdis != null)
                {
                    paraname = ((BTDisplayAttribute)attrdis).ParaName;
                    modulename = ((BTDisplayAttribute)attrdis).ModuleName;
                }
                if (string.IsNullOrEmpty(paraname))
                    paraname = p.Name;
                //默认验证BTRequired
                BTRequiredAttribute requireAttr = new BTRequiredAttribute();
                //判断当前字段是否标记BTRequired特性
                var stepattr = p.GetCustomAttribute(typeof(BTRequiredAttribute));
                if (stepattr != null)
                {
                    requireAttr.AllowGuidEmpty = ((BTRequiredAttribute)stepattr).AllowGuidEmpty;
                    requireAttr.AllowStringEmpty = ((BTRequiredAttribute)stepattr).AllowStringEmpty;
                    requireAttr.AllowZero = ((BTRequiredAttribute)stepattr).AllowZero;
                    requireAttr.Message = ((BTRequiredAttribute)stepattr).Message;
                    var validResult = requireAttr.Valid(p, paraValue, paraname, modulename);
                    if (validResult != null)
                    {
                        results.Add(validResult);
                    }
                }
            }
            if (results.Count == 0)
            {
                rz.code = 1;
                rz.message = "验证通过";
            }
            else
            {
                rz.code = 0;
                rz.message = "验证失败";
                rz.@object = results;
            }
            return rz;
        }
    }
}

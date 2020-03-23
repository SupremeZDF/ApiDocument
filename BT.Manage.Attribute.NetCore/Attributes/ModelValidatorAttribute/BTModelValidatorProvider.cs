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
    /// 验证提供类
    /// </summary>
    public  class BTModelValidatorProvider
    {
        /// <summary>
        /// 验证特性
        /// </summary>
        private readonly string[] ValidateAttribute = { "BTRequiredAttribute", "BTPhoneNumberAttribute", "BTRegularExpressionAttribute", "BTStringLengthAttribute", "BTCompareAttribute" };
        /// <summary>
        /// 模块名称
        /// </summary>
        private string modulename;
        /// <summary>
        /// 字段名称
        /// </summary>
        private string paraname;
        /// <summary>
        /// 当前字段验证期次
        /// </summary>
        private int? prostepIndex;
        /// <summary>
        /// 当前验证类验证期次值
        /// </summary>
        private int? stepIndexValue;

        /// <summary>
        /// 当前字段所属验证模块
        /// </summary>
        private string promoduleCode;
        /// <summary>
        /// 当前验证类模块代码值
        /// </summary>
        private string moduleCodeVaue;

        /// <summary>
        /// 当前字段值
        /// </summary>
        private object paraValue;
        /// <summary>
        /// 验证类别 0 模块 1 期次
        /// </summary>
        public int? validateType { get; set; }
        /// <summary>
        /// 自定义规则验证
        /// </summary>
        /// <param name="result"></param>
        /// <param name="o"></param>
        public  void CustomValidation(List<ValidationResult> results,object o)
        {
            var type = o.GetType();
            object[] parameters = new object[] { results };

            if (type.GetInterface("ICustomValidate") != null)
            {
                MethodInfo meinfo = type.GetMethod("AddValidationErrors");
                meinfo.Invoke(o, parameters);
            }
        }

        /// <summary>
        /// 处理自定义验证消息
        /// </summary>
        /// <param name="o"></param>
        public  string CustomValidaMesage(List<ValidationResult> results, object o)
        {
            var type = o.GetType();
            object[] parameters = new object[] { results };
            string mes = "";
            if (type.GetInterface("ICustomValidateMessage") != null)
            {
                MethodInfo meinfo = type.GetMethod("FormatValidateMessage");
                mes=(string)meinfo.Invoke(o, parameters);
            }
            return mes;
        }
        
        /// <summary>
        /// 判断是否自定义验证消息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public  bool IsCustomValidaMessage(object o)
        {
            var type = o.GetType();
            if (type.GetInterface("ICustomValidateMessage") != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断是否验证全部
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public  bool IsValidAll(object o)
        {
            var type = o.GetType();
            if (type.GetInterface("IValidateAllPro") != null)
                return true;
            else
                return false;
        }
        public void initClassValidateConfig(object o)
        {
            //获取当前类中StepIndex属性值
            var type = o.GetType();
            var steppro = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(p => p.Name == "StepIndex");
            var svalue = steppro.GetValue(o);
            if (svalue != null)
            {
                stepIndexValue = (int)svalue;
            }
            //获取当前类中ModuleCode属性值
            var modulepro = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(p => p.Name == "ModuleCode");
            var mvalue = modulepro.GetValue(o);
            if (mvalue != null)
            {
                moduleCodeVaue = (string)mvalue;
            }
        }
        /// <summary>
        /// 按自定义配置验证
        /// </summary>
        public void ModelFiledValidationExtend(List<ValidationResult> results,object o,List<ValidateExtendModel> extendmodels)
        {
            var type = o.GetType();
            List<string> proNamelist=new List<string>();
            foreach(var item in extendmodels)
            {
                proNamelist.Add(item.MemberName);
            }
            //获取属性
            PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            propertyinfo = propertyinfo.Where(p => proNamelist.Contains(p.Name)).ToArray();
            //属性验证
            foreach (PropertyInfo p in propertyinfo)
            {
                paraValue = p.GetValue(o);
                var extendmodel=extendmodels.FirstOrDefault(m=>m.MemberName==p.Name);
                //获取BTDisplay
                var attrdis = p.GetCustomAttribute(typeof(BTDisplayAttribute));
                if (attrdis != null)
                {
                    paraname = ((BTDisplayAttribute)attrdis).ParaName;
                    modulename = ((BTDisplayAttribute)attrdis).ModuleName;
                }
                if(string.IsNullOrEmpty(paraname))
                    paraname=p.Name;
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
                }
                var validResult= requireAttr.Valid(p, paraValue, paraname, modulename);
                if (validResult != null)
                {
                    results.Add(validResult);
                }
            }
        }
        /// <summary>
        /// 按数据库配置验证
        /// </summary>
        /// <param name="results"></param>
        /// <param name="o"></param>
        /// <param name="extendmodels"></param>
        public void ModelFiledValidationSQLExtend(List<ValidationResult> results, object o, List<ValidateExtendModel> extendmodels)
        {
            var type = o.GetType();
            List<string> proNamelist = new List<string>();
            foreach (var item in extendmodels)
            {
                proNamelist.Add(item.MemberName);
            }
            //获取属性
            PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            propertyinfo = propertyinfo.Where(p => proNamelist.Contains(p.Name)).ToArray();
            //属性验证
            foreach (PropertyInfo p in propertyinfo)
            {
                paraValue = p.GetValue(o);
                var extendmodel = extendmodels.FirstOrDefault(m => m.MemberName == p.Name);
                //获取BTDisplay
                var attrdis = p.GetCustomAttribute(typeof(BTDisplayAttribute));
                if (attrdis != null)
                {
                    paraname = ((BTDisplayAttribute)attrdis).ParaName;
                    modulename = ((BTDisplayAttribute)attrdis).ModuleName;
                }
                if (string.IsNullOrEmpty(paraname))
                    paraname = p.Name;
                //默认验证BTSQLRequired
                BTSQLRequiredAttribute requireAttr = new BTSQLRequiredAttribute();
                //判断当前字段是否标记BTSQLRequired特性
                var stepattr = p.GetCustomAttribute(typeof(BTSQLRequiredAttribute));
                if (stepattr != null)
                {
                    requireAttr.AllowGuidEmpty = ((BTSQLRequiredAttribute)stepattr).AllowGuidEmpty;
                    requireAttr.AllowStringEmpty = ((BTSQLRequiredAttribute)stepattr).AllowStringEmpty;
                    requireAttr.AllowZero = ((BTSQLRequiredAttribute)stepattr).AllowZero;
                    requireAttr.Message = ((BTSQLRequiredAttribute)stepattr).Message;
                }
                var validResult = requireAttr.Valid(p, paraValue, paraname, modulename);
                if (validResult != null)
                {
                    results.Add(validResult);
                }
            }
        }
        /// <summary>
        /// 字段验证
        /// </summary>
        /// <param name="result"></param>
        /// <param name="o"></param>
        public  void ModelFiledValidation(List<ValidationResult> results, object o,bool isallValid)
        {
            var type = o.GetType();
            //获取属性
            PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Instance  | BindingFlags.Public);
            if (validateType == 0)//按模块验证
                propertyinfo = propertyinfo.Where(p => p.GetCustomAttribute(typeof(BTModuleCodeAttribute)) != null).ToArray();
            if (validateType == 1)//按步骤验证
                propertyinfo = propertyinfo.Where(p => p.GetCustomAttribute(typeof(BTStepIndexAttribute)) != null).ToArray();
            foreach (PropertyInfo p in propertyinfo)
            {
               
                //过滤掉不验证的特性
                object[] Attribute = p.GetCustomAttributes(true).Where(n => ValidateAttribute.Contains(n.GetType().Name)).ToArray();
                //包含验证特性
                if (Attribute.Length > 0)
                {
                    paraValue = p.GetValue(o);
                    //获取配置参数值
                    GetConfigAttributeValue(p);
                    if (string.IsNullOrEmpty(paraname))
                        paraname = p.Name;
                    bool b = IsCanValidate();//是否验证
                    if (b)
                    {
                        //验证当前字段否标记BTRequired
                        var reqattribute = Attribute.FirstOrDefault(a => a.GetType().Name == "BTRequiredAttribute");
                        if (reqattribute != null)//存在BTRequired特性
                        {
                            var reb = AttributeValid(reqattribute, p, results, o);
                            if (reb)//验证通过
                            {
                                //判断是否允许Empty,GuidEmpty 或 Zero
                                var _allowEmpty = ((BTRequiredAttribute)reqattribute).AllowStringEmpty;
                                var _allowGuidEmpty = ((BTRequiredAttribute)reqattribute).AllowGuidEmpty;
                                var _allowZero = ((BTRequiredAttribute)reqattribute).AllowZero;
                                if (paraValue.Equals((object)0) || paraValue.Equals(string.Empty) || paraValue.Equals((object)Guid.Empty))
                                    continue;
                            }
                            else
                            {
                                if (!isallValid)
                                    return;
                            }
                        }
                        //过滤BTRequired
                        Attribute = Attribute.Where(a => a.GetType().Name != "BTRequiredAttribute").ToArray();
                        foreach (object ao in Attribute)
                        {

                            var validb = AttributeValid(ao, p, results, o);
                            if (!validb)
                                break;
                            if (!isallValid)
                                return;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 判断是否可以继续验证
        /// </summary>
        /// <returns></returns>
        private bool IsCanValidate()
        {
            bool b = false;
            if (validateType.HasValue)//按步骤或模块验证
            {
                if (validateType == 0)//按模块
                {
                    if (promoduleCode == moduleCodeVaue)
                        b = true;
                }
                if (validateType == 1)//按期次
                {

                    if (prostepIndex <= stepIndexValue && prostepIndex.HasValue)
                        b = true;
                }
            }
            else
                b = true;
            return b;
        }
        /// <summary>
        /// 调用特性验证方法
        /// </summary>
        /// <param name="_ao"></param>
        /// <param name="_p"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private bool AttributeValid(object _ao,PropertyInfo _p, List<ValidationResult> results,object _o)
        {

            //调用验证方法
            var aotype = _ao.GetType();
            var _otype=_o.GetType();
            //判断是否是BTCompareAttribute
            if (aotype.Name == "BTCompareAttribute")
            {
                //初始化要比较的对象
                var aopros = _otype.GetProperties(BindingFlags.Instance  | BindingFlags.Public);
                var startname=((BTCompareAttribute)_ao).StartPro;
                var startPro = aopros.FirstOrDefault(o => o.Name == startname); 
                var endname = ((BTCompareAttribute)_ao).EndPro;
                var endPro = aopros.FirstOrDefault(o => o.Name == endname);
                //调用初始化方法
                ((BTCompareAttribute)_ao).InitCompareProInfo(startPro, endPro, _o);
            }

            MethodInfo validmeinfo = aotype.GetMethod("Valid");
            object[] vaparameters = new object[] { _p, paraValue, paraname, modulename };
            var validResult = validmeinfo.Invoke(_ao, vaparameters);
            if (validResult != null)
            {
                results.Add((ValidationResult)validResult);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取BTDisplay ， BTStepIndex,BTModuleCode特性配置值
        /// </summary>
        /// <param name="p"></param>
        /// <param name="_moudlename"></param>
        /// <param name="_paraname"></param>
        /// <param name="_stepIndex"></param>
        private  void  GetConfigAttributeValue(PropertyInfo p)
        {
            //获取DIsplay特性
            var dispattribute = p.GetCustomAttribute(typeof(BTDisplayAttribute));
            if (dispattribute != null)
            {
                //获取自定义模块名称，字段名称
                modulename = ((BTDisplayAttribute)dispattribute).ModuleName;
                paraname = ((BTDisplayAttribute)dispattribute).ParaName;
            }
            else
            {
                modulename = string.Empty;
                paraname = string.Empty;
            }
                
            //获取StepIndex特性
            var stepattribute = p.GetCustomAttribute((typeof(BTStepIndexAttribute)));
            if (stepattribute != null)
            {
                prostepIndex = ((BTStepIndexAttribute)stepattribute).StepIndex;
            }
            else
            {
                prostepIndex = null;
            }
            //获取ModuleCode特性
            var moduleattribute = p.GetCustomAttribute(typeof(BTModuleCodeAttribute));
            if (moduleattribute != null)
            {
                promoduleCode = ((BTModuleCodeAttribute)moduleattribute).ModuleCode;
            }
            else
            {
                promoduleCode = string.Empty;
            }
        }
    }
}

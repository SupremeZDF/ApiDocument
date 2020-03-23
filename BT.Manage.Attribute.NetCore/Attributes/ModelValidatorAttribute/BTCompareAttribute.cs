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
    /// 提供字段之间值的比较
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class BTCompareAttribute : BTBaseModelValidator
    {


        private string startPro;
        private string endPro;
        private bool isequalStartPro = false;
        private bool isequalEndPro = false;
        /// <summary>
        /// 起始字段名称
        /// </summary>
        public string StartPro {
            get 
            {
                return startPro;
            }
        }

        /// <summary>
        /// 终止字段名称
        /// </summary>
        public string EndPro {
            get {
                return endPro;
            }
        }
        /// <summary>
        /// 是否可等于起始字段
        /// </summary>
        public bool IsEqualStartPro{
            get {
                return isequalStartPro;
            }
            set {
                isequalStartPro = value;
            }
        }
        /// <summary>
        /// 是否可等于结束字段
        /// </summary>
        public bool IsEqualEndPro{
            get {
                return isequalEndPro;
            }
            set {
                isequalEndPro = value;             
            }
        }
        /// <summary>
        /// 起始字段反射信息
        /// </summary>
        private PropertyInfo StartProInfo { get; set; }
        /// <summary>
        /// 终止字段反射信息
        /// </summary>
        private PropertyInfo EndProInfo { get; set; }
        /// <summary>
        /// 当前验证实例
        /// </summary>
        private object CurentObject { get; set; }

        public BTCompareAttribute(string _startPro, string _endPro)
        {
            endPro = _endPro;
            startPro = _startPro;
        }

        /// <summary>
        /// 规则验证
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <param name="paraname"></param>
        /// <param name="modulename"></param>
        /// <returns></returns>
        public override ValidationResult Valid(PropertyInfo p, object t, string paraname, string modulename)
        {
            var paratype = base.GetPropertyinfoType(p);
            
            //判断当前字段类型是否可用于比较
            var isc = IsCompare(p);
            var b = true;
            if (isc)
            {
                //获取比较字段类型
                var starttype = base.GetPropertyinfoType(StartProInfo);
                var endtype = base.GetPropertyinfoType(EndProInfo);
                if ((paratype == starttype) && (paratype == endtype))//判断比较字段类型是否一致
                {
                    //获取比较字段值
                    var startvalue = StartProInfo.GetValue(CurentObject);
                    var endvalue = EndProInfo.GetValue(CurentObject);
                    if (t == null)
                    {
                        return new ValidationResult() { Member = paraname, Message = "字段" + paraname + "值为空，不可进行比较", ModuleName = modulename };
                    }
                    if (startvalue == null || endvalue == null)
                    {
                        return new ValidationResult() { Member = paraname, Message = "被比较字段值为空，不可进行比较", ModuleName = modulename };
                    }
                    try
                    {
                        RefalctProInitType(paratype, paraname, t, startvalue, endvalue);
                    }
                    catch (Exception ex)
                    {
                        if (string.IsNullOrEmpty(Message))
                        {
                            Message = ex.Message.Split('★')[0];
                        }
                        b = false;
                    }
                } 
            }
            if (!b)
            {
                return new ValidationResult() { Member = paraname, Message = Message, ModuleName = modulename };
            }
            else
                return null;
        }

        /// <summary>
        /// 字段匹配类型初始化
        /// </summary>
        /// <param name="paravalue"></param>
        /// <param name="startvalue"></param>
        /// <param name="endvalue"></param>
        private void RefalctProInitType(string paratype, string paraname, object paravalue, object startvalue, object endvalue)
        {

            switch (paratype.ToLower())
            {
                case "datetime":
                    DateTime dtparavlue = (DateTime)paravalue;
                    DateTime dtstartvalue = (DateTime)startvalue;
                    DateTime dtendvalue = (DateTime)endvalue;
                    dtparavlue.CheckBetween(paraname, dtstartvalue, dtendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "string":
                    string sparavalue = (string)paravalue;
                    var sstartvalue = (string)startvalue;
                    var sendvalue = (string)endvalue;
                    sparavalue.CheckBetween(paraname, sstartvalue, sendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "guid":
                    Guid gparavalue = (Guid)paravalue;
                    var gstartvalue = (Guid)startvalue;
                    var gendvalue = (Guid)EndProInfo.GetValue(CurentObject);
                    gparavalue.CheckBetween(paraname, gstartvalue, gendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "long":
                    long lparavalue = (long)paravalue;
                    var lstartvalue = (long)startvalue;
                    var lendvalue = (long)endvalue;
                    lparavalue.CheckBetween(paraname, lstartvalue, lendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "int":
                case "int16":
                case "int32":
                case "int64":
                    int iparavalue = (int)paravalue;
                    var istartvalue = (int)startvalue;
                    var iendvalue = (int)endvalue;
                    iparavalue.CheckBetween(paraname, istartvalue, iendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "decimal":
                    decimal dparavalue = (decimal)paravalue;
                    var dstartvalue = (decimal)startvalue;
                    var dendvalue = (decimal)endvalue;
                    dparavalue.CheckBetween(paraname, dstartvalue, dendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                case "float":
                case "double":
                    double dfparavalue = (double)paravalue;
                    var dfstartvalue = (double)startvalue;
                    var dfendvalue = (double)endvalue;
                    dfparavalue.CheckBetween(paraname, dfstartvalue, dfendvalue, IsEqualStartPro, IsEqualEndPro).Throw();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 判断当前字段类型是否可用于比较
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool IsCompare(PropertyInfo p)
        {
            var type = p.PropertyType;
            if (type.GetInterface("IComparable") != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 初始化比较字段信息
        /// </summary>
        /// <param name="_startproInfo"></param>
        /// <param name="_endproInfo"></param>
        public void InitCompareProInfo(PropertyInfo _startproInfo, PropertyInfo _endproInfo,object _curentObject)
        {
            StartProInfo = _startproInfo;
            EndProInfo = _endproInfo;
            CurentObject = _curentObject;
        }
    }
}

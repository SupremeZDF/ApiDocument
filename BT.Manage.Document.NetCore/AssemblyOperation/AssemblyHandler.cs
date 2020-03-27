using BT.Manage.Attribute;
using BT.Manage.Frame.Base;
using BT.Manage.Frame.Base.NetCore.ConfigManage;
using BT.Manage.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;

namespace BT.Manage.Document.AssemblyOperation
{
    public class AssemblyHandler
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + JsonConfigMange.GetInstance().AppSettings["DLLName"];

        string basePath = PlatformServices.Default.Application.ApplicationBasePath;



        string systemPath = AppDomain.CurrentDomain.BaseDirectory;

        public void GetApiDetialInfo(AssemblyResult result)
        {
            //添加控制器信息
            GetClassName(result);
            GetClassMethodInfo(result);
            GetInAndOutParaInfo(result);
        }

        /// <summary>  
        /// 获取程序集中的类名称  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        public void GetClassName(AssemblyResult result)
        {
            var assemblyName = path;
            if (!String.IsNullOrEmpty(assemblyName))
            {
                //assemblyName = path + assemblyName;  通过提供的程序集名称或路径加载程序集
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                //获取程序集的显示名称 返回的结果:程序集的显示和名称  full 完全 fullname 获取完全的名称
                result.AssemblyName = assembly.FullName;
                //返回结果:包含此程序集中定义的所有类型的数组。  获取此程序集中定义的类型。0
                Type[] ts = assembly.GetTypes();
                //获取所有控制器
                ts = ts.Where(o => o.Name.Contains("Controller") && o.Name != "FathreControllerBase").ToArray();
                foreach (Type t in ts)
                {
                    AssemblyClassInfo classinfo = new AssemblyClassInfo();
                    //var f= typeof(AssemblyClassInfo);
                    //var ff = f.GetCustomAttribute(typeof(DescriptionAttribute)); fullname 获取完全的名称
                    classinfo.FullName = t.FullName;
                    classinfo.ClassName = t.Name;
                    //获取描述信息  获取自定义特性 custom 自定义  返回自定的特性 与attributeType匹配的自定义属性，如果没有这样的属性，则为null0
                    var desattr = t.GetCustomAttribute(typeof(DescriptionAttribute));
                    if (desattr != null)
                        classinfo.ClassDescribe = ((DescriptionAttribute)desattr).Description;
                    if (result.ClassList == null)
                        result.ClassList = new List<AssemblyClassInfo>();
                    //判断是否token验证
                    //var attrs = t.GetCustomAttributes();
                    //var count = attrs.Count(o => o.GetType().Name == "BtAppAuthorize" || o.GetType().BaseType.Name == "BtAuthorize");
                    //if (count > 0)
                    //{
                    //    classinfo.FIsAuthor = 1;
                    //}
                    //else
                    //    classinfo.FIsAuthor = 0;
                    Console.WriteLine("写入控制器信息:" + JsonConvert.SerializeObject(classinfo));
                    result.ClassList.Add(classinfo);
                }
            }
        }
        /// <summary>  
        /// 获取类的方法  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        /// <param name="className">类名</param>  
        public void GetClassMethodInfo(AssemblyResult result)
        {
            try
            {
                foreach (var item in result.ClassList)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(item.FullName))
                        {
                            var assemblyName = path;
                            Assembly assembly = Assembly.LoadFrom(assemblyName);
                            //根据相应的名称获取相应的特性  parameter 第一个参数 为true没有值则返回异常 flase 未有值则返回 null  parameter 第二个参数 则是否区分大小写
                            Type type = assembly.GetType(item.FullName, true, true);
                            if (type != null)
                            {
                                //类的方法   DeclaringType 获取声明此成员的类的对象。  
                                MethodInfo[] methodinfos = type.GetMethods().Where(o => o.DeclaringType.Name.ToLower() == item.ClassName.ToLower()).ToArray();
                                foreach (MethodInfo m in methodinfos)
                                {
                                    try
                                    {
                                        AssemblyMethodInfo method = new AssemblyMethodInfo();
                                        method.MethodName = m.Name;
                                        //获取是否token验证
                                        //判断是否token验证 获取所有特性
                                        var attrs = m.GetCustomAttributes();
                                        //foreach (var i in attrs) 
                                        //{
                                        //    var TypeName = i.GetType().Name;
                                        //    var BaseName = i.GetType().BaseType.Name;
                                        //}
                                        //获取
                                        var count = attrs.Count(o => o.GetType().Name == "BtAppAuthorize" || o.GetType().BaseType.Name == "BtAuthorize");
                                        if (count > 0)
                                        {
                                            method.FIsAuthor = 1;
                                        }
                                        else
                                            method.FIsAuthor = 0;
                                        //获取方法请求类型
                                        if (m.GetCustomAttributes().FirstOrDefault(o => o.GetType().Name == "HttpPostAttribute") != null)
                                            method.MethodRequestType = "Post";
                                        if (m.GetCustomAttributes().FirstOrDefault(o => o.GetType().Name == "HttpGetAttribute") != null)
                                            method.MethodRequestType = "Get";
                                        //获取方法描述特性
                                        var desaatr = m.GetCustomAttribute(typeof(BTMethodDescibeAttribute));
                                        if (desaatr != null)
                                        {
                                            string FrameForwardService = ((BTMethodDescibeAttribute)desaatr).FrameForwardService;
                                            method.FrameForwardService = FrameForwardService;
                                            method.MethodAttr = desaatr;
                                        }

                                        //获取方法上定义的输入输出参数 获取此方法的返回类型 并且判断该方法是否为泛型类型
                                        if (m.ReturnType.IsGenericType)
                                        {
                                            // 获取此返回类型的 类型
                                            if (m.ReturnType.GetInterface("IList") != null)
                                            {
                                                method.FoutPara = m.ReturnType;
                                            }
                                            else
                                                method.FoutPara = m.ReturnType.GenericTypeArguments[0];
                                        }
                                        else
                                            method.FoutPara = m.ReturnType;
                                        if (m.GetParameters().Length > 0)
                                            method.FInPara = m.GetParameters()[0].GetType();


                                        method.ControllerName = item.ClassName;

                                        if (method.MethodRequestType == "Post")
                                        {

                                            method.IsPost = true;
                                            //获取方法描述具体信息
                                            if (desaatr != null)
                                            {
                                                method.MethodDescibe = ((BTMethodDescibeAttribute)desaatr).Decription;
                                                //输入参数
                                                Type inpara = ((BTMethodDescibeAttribute)desaatr).InParam;

                                                method.InPara = inpara;
                                                //输出参数
                                                Type outpara = ((BTMethodDescibeAttribute)desaatr).OutParam;
                                                method.OutPara = outpara;

                                            }

                                        }
                                        if (method.MethodRequestType == "Get")
                                        {
                                            method.IsPost = false;
                                            //获取输入字段描述信息
                                            if (desaatr != null)
                                            {
                                                method.MethodDescibe = ((BTMethodDescibeAttribute)desaatr).Decription;
                                                var paradescs = ((BTMethodDescibeAttribute)desaatr).ParaDecription;
                                                if (paradescs == null)
                                                    paradescs = "";
                                                var paradeslist = paradescs.Split('|');
                                                Dictionary<string, string> oscar = new Dictionary<string, string>();
                                                foreach (var pditem in paradeslist)
                                                {
                                                    if (pditem.Contains(':') || pditem.Contains('：'))
                                                    {
                                                        var singleparas = pditem.Split(':');
                                                        if (pditem.Contains('：'))
                                                            singleparas = pditem.Split('：');
                                                        if (singleparas.Length > 1)
                                                        {
                                                            oscar.Add(singleparas[0].ToLower(), singleparas[1]);
                                                        }
                                                    }

                                                }
                                                if (oscar.Count > 0)
                                                {
                                                    //获取方法输入参数
                                                    var parameters = m.GetParameters();
                                                    var isresultrequest = 0;
                                                    //获取输入参数
                                                    List<AssemblyFiledInfo> infiledList = new List<AssemblyFiledInfo>();
                                                    infiledList.Add(new AssemblyFiledInfo() { FiledInOrOut = 1, FiledLayer = 0, FiledLeft = 1, FiledRight = 2, FiledName = "object", FiledType = "class" });
                                                    foreach (var pitem in parameters)
                                                    {
                                                        AssemblyFiledInfo filedinfo = new AssemblyFiledInfo();
                                                        filedinfo.FiledName = pitem.Name;
                                                        filedinfo.FiledDescript = oscar[pitem.Name.ToLower()];
                                                        filedinfo.FiledInOrOut = 1;
                                                        filedinfo.FiledLayer = 1;
                                                        filedinfo.FIsResultRequset = isresultrequest;
                                                        if (pitem.ParameterType.IsGenericType && pitem.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                                        {
                                                            filedinfo.FiledType = pitem.ParameterType.GetGenericArguments()[0].Name;
                                                        }
                                                        else
                                                            filedinfo.FiledType = pitem.ParameterType.Name;
                                                        int prgt = 2;
                                                        filedinfo.FiledLeft = prgt;
                                                        filedinfo.FiledRight = prgt + 1;
                                                        UpdateTreeLeftAndRightValue(infiledList, prgt);
                                                        prgt = prgt + 2; ;
                                                        infiledList.Add(filedinfo);
                                                    }
                                                    method.InParamList = infiledList;
                                                    //输出参数
                                                    Type outpara = ((BTMethodDescibeAttribute)desaatr).OutParam;
                                                    method.OutPara = outpara;
                                                }
                                                else
                                                {
                                                    method.MethodDescibe = ((BTMethodDescibeAttribute)desaatr).Decription;
                                                    //输入参数
                                                    Type inpara = ((BTMethodDescibeAttribute)desaatr).InParam;

                                                    method.InPara = inpara;
                                                    //输出参数
                                                    Type outpara = ((BTMethodDescibeAttribute)desaatr).OutParam;
                                                    method.OutPara = outpara;
                                                }

                                            }

                                        }
                                        //判断 类中的方法是否为空 实列话对象
                                        if (item.ClasMethList == null)
                                            item.ClasMethList = new List<AssemblyMethodInfo>();
                                        Console.WriteLine("写入控制器" + item.ClassName + "中的方法信息:" + JsonConvert.SerializeObject(method));
                                        item.ClasMethList.Add(method);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogService.Default.Fatal("方法映射错误:" + ex.Message + " 堆栈信息：" + ex.StackTrace + "类名:" + item.ClassName + "方法名：" + m.Name);
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.Default.Fatal("方法映射错误:" + ex.Message + " 堆栈信息：" + ex.StackTrace + "类名:" + item.ClassName);
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                LogService.Default.Fatal("方法映射错误:" + ex.Message + " 堆栈信息：" + ex.StackTrace);
                Console.WriteLine("错误:" + ex.Message + " 堆栈信息：" + ex.StackTrace);
                throw ex;
            }

        }
        /// <summary>
        /// 获取输入输出参数信息
        /// </summary>
        /// <param name="result"></param>
        public void GetInAndOutParaInfo(AssemblyResult result)
        {
            foreach (var item in result.ClassList)
            {
                if (item.ClasMethList != null)
                {
                    foreach (var pitem in item.ClasMethList)
                    {
                        //获取输入参数
                        List<AssemblyFiledInfo> infiledList = new List<AssemblyFiledInfo>();
                        string filedtype = "class";
                        bool b = false;
                        Type inptype = pitem.InPara;
                        var isresultrequest = 0;
                        if (pitem.FInPara == typeof(ResultRequset))
                            isresultrequest = 1;
                        var isresult = 0;
                        if (pitem.FoutPara == typeof(Result))
                            isresult = 1;
                        if (pitem.InPara != null)
                        {
                            if (pitem.InPara.IsGenericType)
                            {
                                if (pitem.InPara.GetInterface("IList") != null)
                                    filedtype = "List";
                                else
                                    filedtype = pitem.InPara.GetGenericArguments()[0].Name;
                                inptype = pitem.InPara.GetGenericArguments()[0];
                            }
                            b = true;
                        }
                        infiledList.Add(new AssemblyFiledInfo() { FiledInOrOut = 1, FiledLayer = 0, FiledLeft = 1, FiledRight = 2, FiledName = "object", FiledType = filedtype });
                        if (b)
                            GetParaFiledInfo(inptype, infiledList, 1, 1, 1, 2, isresultrequest, isresult);
                        if (pitem.InParamList == null)
                            pitem.InParamList = infiledList;
                        Console.WriteLine("写入控制器" + item.ClassName + "中的方法" + pitem.MethodName + "的输入参数" + JsonConvert.SerializeObject(infiledList));
                        //获取输出参数
                        List<AssemblyFiledInfo> outfiledList = new List<AssemblyFiledInfo>();
                        string outfiletype = "class";
                        bool outb = false;
                        Type outtype = pitem.OutPara;

                        if (pitem.OutPara != null)
                        {
                            if (pitem.OutPara.IsGenericType)
                            {
                                if (pitem.OutPara.GetInterface("IList") != null)
                                    outfiletype = "List";
                                else
                                    outfiletype = pitem.OutPara.GetGenericArguments()[0].Name;
                                outtype = pitem.OutPara.GetGenericArguments()[0];
                            }
                            outb = true;
                        }
                        outfiledList.Add(new AssemblyFiledInfo() { FiledInOrOut = 2, FiledLayer = 0, FiledLeft = 1, FiledRight = 2, FiledName = "object", FiledType = outfiletype });
                        if (outb)
                            GetParaFiledInfo(outtype, outfiledList, 2, 1, 1, 2, isresultrequest, isresult);
                        if (pitem.OutParamList == null)
                            pitem.OutParamList = outfiledList;
                        Console.WriteLine("写入控制器" + item.ClassName + "中的方法" + pitem.MethodName + "的输出参数" + JsonConvert.SerializeObject(outfiledList));
                    }
                }

            }
        }
        /// <summary>
        /// 获取参数属性信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="FiledList"></param>
        public void GetParaFiledInfo(Type t, List<AssemblyFiledInfo> FiledList, int inorout, int layer, int plft, int prgt, int fisresultrequest, int fisresult)
        {

            PropertyInfo[] propers = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(o => o.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null).ToArray();
            foreach (var p in propers)
            {
                bool b = true;
                AssemblyFiledInfo filedinfo = new AssemblyFiledInfo();
                filedinfo.FileClassName = t.Name;
                filedinfo.FiledInOrOut = inorout;
                filedinfo.FiledName = p.Name;
                filedinfo.FiledLayer = layer;
                filedinfo.FiledLeft = prgt;
                filedinfo.FiledRight = prgt + 1;
                filedinfo.FIsResult = fisresult;
                filedinfo.FIsResultRequset = fisresultrequest;
                //更新结构左右值
                UpdateTreeLeftAndRightValue(FiledList, prgt);
                //更新父级右值
                prgt = prgt + 2;
                //获取属性的BTDisplay标记
                var disattr = p.GetCustomAttribute((typeof(BTDisplayAttribute)));
                if (disattr != null)
                {
                    filedinfo.FiledDescript = ((BTDisplayAttribute)disattr).ParaName;
                    filedinfo.FMoudleName = ((BTDisplayAttribute)disattr).ModuleName;
                }

                //字段类型
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    filedinfo.FiledType = p.PropertyType.GetGenericArguments()[0].Name + "?";
                    //if (filedinfo.FiledType.Trim().ToLower() == "int32?" || filedinfo.FiledType.Trim().ToLower() == "int16?" || filedinfo.FiledType.Trim().ToLower() == "int64?")
                    //{
                    //    filedinfo.FiledType = "int?";
                    //}
                }
                else if (p.PropertyType.IsGenericType)
                {
                    if (p.PropertyType.Name == "List`1")
                    {
                        filedinfo.FiledType = "List<" + p.PropertyType.GetGenericArguments()[0].Name + ">";
                    }
                    else
                        filedinfo.FiledType = p.PropertyType.GetGenericArguments()[0].Name;
                    FiledList.Add(filedinfo);
                    b = false;
                    if (p.PropertyType.GetGenericArguments()[0].IsClass && !p.PropertyType.GetGenericArguments()[0].IsPrimitive && p.PropertyType.GetGenericArguments()[0] != typeof(string) && p.PropertyType.GetGenericArguments()[0] != typeof(string[]))
                    {
                        GetParaFiledInfo(p.PropertyType.GetGenericArguments()[0], FiledList, inorout, layer + 1, filedinfo.FiledLeft, filedinfo.FiledRight, fisresultrequest, fisresult);
                        prgt = filedinfo.FiledRight + 1;
                    }

                }
                else if (p.PropertyType.IsClass && !p.PropertyType.IsPrimitive && p.PropertyType != typeof(string) && p.PropertyType != typeof(string[]))
                {
                    filedinfo.FiledType = p.PropertyType.Name;
                    FiledList.Add(filedinfo);
                    b = false;
                    GetParaFiledInfo(p.PropertyType, FiledList, inorout, layer + 1, filedinfo.FiledLeft, filedinfo.FiledRight, fisresultrequest, fisresult);
                    prgt = filedinfo.FiledRight + 1;
                }
                else
                    filedinfo.FiledType = p.PropertyType.Name;
                //if (filedinfo.FiledType.Trim().ToLower() == "int32" || filedinfo.FiledType.Trim().ToLower() == "int16" || filedinfo.FiledType.Trim().ToLower() == "int64")
                //{
                //    filedinfo.FiledType = "int";
                //}
                if (b)
                    FiledList.Add(filedinfo);
            }



        }
        /// <summary>
        /// 更新左右值
        /// </summary>
        /// <param name="_FiledList"></param>
        /// <param name="_prgt"></param>
        private void UpdateTreeLeftAndRightValue(List<AssemblyFiledInfo> _FiledList, int _prgt)
        {
            foreach (var item in _FiledList)
            {
                if (item.FiledRight >= _prgt)
                    item.FiledRight = item.FiledRight + 2;
                if (item.FiledLeft >= _prgt)
                    item.FiledLeft = item.FiledLeft + 2;
            }
        }
    }
}

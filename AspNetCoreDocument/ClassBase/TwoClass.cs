using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BT.Manage.Attribute;

namespace AspNetCoreDocument
{
    public class TwoClass
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        [BTDisplay(ParaName ="客户姓名")]
        [BTRequired(AllowStringEmpty =false,Message ="请输出客户姓名")]
       public string Name { get; set; }

        /// <summary>
        /// 授权ID
        /// </summary>
        [BTDisplay(ParaName ="客户密码")]
        [BTRequired(AllowStringEmpty = false, Message = "请输出客户密码")]
        public string pswd { get; set; }
    }
}

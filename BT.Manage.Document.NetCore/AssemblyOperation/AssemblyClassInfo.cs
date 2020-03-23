using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Document.AssemblyOperation
{
    public class AssemblyClassInfo
    {

        public string FullName { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 控制器描叙
        /// </summary>
        public string ClassDescribe { get; set; }

        /// <summary>
        /// 是否token验证
        /// </summary>
        public int FIsAuthor { get; set; }
        /// <summary>
        /// 控制器中的方法
        /// </summary>
        public List<AssemblyMethodInfo> ClasMethList { get; set; }
    }
}

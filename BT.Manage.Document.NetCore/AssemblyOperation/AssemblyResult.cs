using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Document.AssemblyOperation
{
    public class AssemblyResult
    {
        /// <summary>  
        /// 程序集名称  
        /// </summary>  
        public string AssemblyName { get; set; }

        /// <summary>  
        /// 类名  
        /// </summary>  
        public List<AssemblyClassInfo> ClassList { get; set; }
    }
}

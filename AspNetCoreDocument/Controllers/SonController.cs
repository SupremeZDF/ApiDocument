using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BT.Manage.Attribute;

namespace AspNetCoreDocument.Controllers
{
    [Description("练习学习")]
    public class SonController : FathreControllerBase
    {
        /// <summary>
        /// oneGet
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BTMethodDescibe(Decription ="oneGet",FrameForwardService ="api",InParam =typeof(OneClass),OutParam =typeof(TwoClass))]
        public string oneGet() 
        {
            return "2";
        }

        /// <summary>
        /// OnePoset
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BTMethodDescibe(Decription = "onePost", FrameForwardService = "api", InParam = typeof(OneClass), OutParam = typeof(TwoClass))]
        public string onePost() 
        {
            return "1";
        }

    }
}
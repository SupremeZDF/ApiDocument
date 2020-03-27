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

        [Route("")]
        //[ActionName("")]
        [AcceptVerbs("get", "set")]
        [HttpPost]
        [BTMethodDescibe(Decription ="练习One",FrameForwardService ="api",InParam =typeof(OneClass),OutParam =typeof(TwoClass))]
        public TwoClass oneGet([FromBody] OneClass oneClass) 
        {
            return new TwoClass() 
            {
                Name="123",
                pswd="123"
            };
        }

        /// <summary>
        /// OnePoset
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BTMethodDescibe(Decription = "练习 Two", FrameForwardService = "api", InParam = typeof(OneClass), OutParam = typeof(TwoClass))]
        public TwoClass onePost([FromBody] OneClass oneClass) 
        {
            return new TwoClass()
            {
                Name = "123",
                pswd = "123"
            };
        }

    }
}
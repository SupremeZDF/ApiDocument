using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetFrameWork.WebApi.Controllers
{
    public class PlaseController : ApiController
    {
        [AcceptVerbs("get","post")]
        public string GetOne()
        {
            return "GetOne";
        }

        [HttpGet]
        public string GetTwo() 
        {
            return "GetTwo";
        }

        [HttpGet]
        public string GetThree() 
        {
            return "GetThree";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetFrameWork.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class TestController : ApiController
    {
        [Route("demo/{id:int}")]
        public object Get1() 
        {
            return "d1";
        }

        [Route("demo/get/{name}")]
        [HttpPost]
        public object Get2() 
        {
            return "d2";
        }

    }
}

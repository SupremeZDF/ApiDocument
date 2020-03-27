using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetFrameWork.WebApi.Controllers
{
    public class GetValueController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> GetOne(string name,string pswd) 
        {
            return new string[] { "value1","value2" };
        }

    }
}

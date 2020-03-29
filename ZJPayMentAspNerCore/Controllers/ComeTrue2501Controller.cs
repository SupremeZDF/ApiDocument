using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZJ_Interface;
using ZJPayMent.DAL;

namespace ZJPayMentAspNerCore.Controllers
{
    public class ComeTrue2501Controller : FathreControllerBase
    {

        public readonly InterFace2501 _interFace2501;

        public ComeTrue2501Controller(InterFace2501 interFace2501) 
        {
            this._interFace2501 = interFace2501;
        }
        
        [HttpPost]
        public Class2501Response Get2501Response([FromBody]Class2501Request Prameter2501Request) 
        {
            _interFace2501.Ruquest2501Method(Prameter2501Request);
            return null;
        }
    }
}
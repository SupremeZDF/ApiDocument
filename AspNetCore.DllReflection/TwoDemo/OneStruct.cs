using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DllReflection
{
    public struct OneStruct : IStrustOne
    {
        public void Name()
        {
            throw new NotImplementedException();
        }

        public void Pswd()
        {
            throw new NotImplementedException();
        }
    }

    public class TwoStruct 
    {
        OneStruct One = new OneStruct();
    }
}

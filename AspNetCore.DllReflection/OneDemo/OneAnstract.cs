using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DllReflection
{
    public abstract class OneAnstract : OneInterface
    {

        public abstract string Pswd();
      
        public abstract int A { get; set; }

        public abstract void Name();

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}

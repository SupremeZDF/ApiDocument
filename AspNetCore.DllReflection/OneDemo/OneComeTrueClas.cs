using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DllReflection
{
    public abstract class OneComeTrueClas :  OneAnstract
    {
        public override void Name() 
        {
        
        }


        public override int A { get { return 1; } set { value = 1; } }

    }


    //子类继承抽象类，需要override抽象类中的抽象属性和抽象方法，如果有未override的，则子类也必须为抽象类
    public class A : OneComeTrueClas
    {
        public override string Pswd()
        {
            throw new NotImplementedException();
        }
    }
}

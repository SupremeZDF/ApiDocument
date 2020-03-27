using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Consolo.Reflection
{
    public class ObjectOneClass
    {
        public void OneRun() 
        {

            //Assembly assembly = Assembly.LoadFrom("");

            //Type type = assembly.GetTypes()[0];

            //type.GUID;

            //Console.WriteLine(type.GUID);

            

            object[] values = { "world", true, 120, 136.34, 'a' };

            
            foreach (var value in values)
            {
                //Console.WriteLine($"{value}-{value.GetType().Name}-{value.GetType().GetProperties()[0].GetValue()}-");
            }
        }
    }
}

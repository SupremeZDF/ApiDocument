using System;
using Consolo.Reflection.OneDemo;
using System.Reflection;

namespace Consolo.Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //ObjectOneClass oneClass = new ObjectOneClass();

            //oneClass.OneRun();

            var assembly = Assembly.GetExecutingAssembly().GetType("Consolo.Reflection.OneDemo.OneClass");

            Console.ReadLine();
        }
    }
}

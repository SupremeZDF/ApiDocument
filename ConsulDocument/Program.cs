using System;
using BT.Manage.Attribute;
using BT.Manage.Core.NetCore;
using BT.Manage.Document;
using BT.Manage.Frame.Base.NetCore;

namespace ConsulDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("是否开始文档生成 Y/N");
            var k = Console.ReadKey();
            if (k.KeyChar == 'y' || k.KeyChar == 'Y') 
            {
                Console.WriteLine("开始执行生成......");
                DocumentServices services = new DocumentServices();
                
                services.CreateDocument();
                Console.WriteLine("生成成功！");
            }
            Console.WriteLine("按ENTER键退出！");
            Console.ReadLine();
        }
    }
}

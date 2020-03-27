using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AspNetFrameWork.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由  //启用特性路由
            config.MapHttpAttributeRoutes();

            
            config.Routes.MapHttpRoute(
                name: "DefaultApi", //表示此路由的名称
                routeTemplate: "api/{controller}/{one}", //表示路由的匹配规则
                defaults: new { one = RouteParameter.Optional },
                constraints: new { one=@"\d*" }  //表示为id添加约束。形参id不能为空而且必须是整数，优先匹配含参方法，也能匹配无参方法（详情请回看上一部分的示例一）。

            );


            config.Routes.MapHttpRoute(
                name:"ActionAP1",
                routeTemplate:"api/{controller}/{action}/{two}",
                defaults:new { two=RouteParameter.Optional}
                );

        }
    }
}

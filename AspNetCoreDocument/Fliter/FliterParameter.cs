using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle;
using Microsoft.OpenApi.Models;

namespace AspNetCoreDocument
{
    public class FliterParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //在此添加参数
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "UserID",  //参数名
                Description = "用户ID",  //描述
                In = ParameterLocation.Header,//query header body path formData
                Style = ParameterStyle.Simple,  //类型
                Required = false
            });
        }
    }
}

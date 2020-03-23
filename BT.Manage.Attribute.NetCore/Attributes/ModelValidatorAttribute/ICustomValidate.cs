using BT.Manage.Frame.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 自定义验证规则约束
    /// </summary>
    public interface ICustomValidate
    {
        void AddValidationErrors(List<ValidationResult> results);

       
    }
}

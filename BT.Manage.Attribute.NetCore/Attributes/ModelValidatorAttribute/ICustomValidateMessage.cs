using BT.Manage.Frame.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Attribute
{
    /// <summary>
    /// 自定义验证消息约束
    /// </summary>
    public interface ICustomValidateMessage
    {
        /// <summary>
        /// 自定义验证消息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        string FormatValidateMessage(List<ValidationResult> result);
    }
}

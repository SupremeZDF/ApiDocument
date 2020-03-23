using BT.Manage.Core;
using BT.Manage.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.Document.Model
{
    /// <summary>
    /// 控制器方法信息
    /// </summary>
    [TableName("d_MethodInfo")]
    class MethodInfo : BaseModel
    {
        [Key]
        [Display(Name = @" ID")]
        public Guid FID { get; set; }
        [Display(Name = @"外键关联控制器")]
        public Guid FConId { get; set; }
        [Display(Name = @"项目id")]
        public Guid? FProId { get; set; }
        [Display(Name = @" 方法名称")]
        public string FMethodName { get; set; }
        [Display(Name = @"方法描述")]
        public string FMethodDescibe { get; set; }
        [Display(Name = @"请求类别")]
        public string FRequestType { get; set; }
        [Display(Name = @"输入json串")]
        public string FInJson { get; set; }
        [Display(Name = @"输出json")]
        public string FOutJson { get; set; }
        [Display(Name = @"添加时间")]
        public DateTime? FAddTime { get; set; }


    }
}

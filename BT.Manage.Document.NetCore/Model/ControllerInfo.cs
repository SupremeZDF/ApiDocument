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
    /// 控制器信息
    /// </summary>
    [TableName("d_ControllerInfo")]
    public class ControllerInfo : BaseModel
    {
        [Key]
        [Display(Name = @" ID")]
        public Guid FID { get; set; }
        [Display(Name = @"外键关联项目")]
        public Guid FProId { get; set; }
        [Display(Name = @" 控制器名称")]
        public string FControllerName { get; set; }
        [Display(Name = @"控制器描述")]
        public string FControllerDescibe { get; set; }
        [Display(Name = @"添加时间")]
        public DateTime? FAddTime { get; set; }
    }
}

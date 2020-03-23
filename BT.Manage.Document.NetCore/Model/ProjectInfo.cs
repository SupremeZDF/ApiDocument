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
    /// 项目名称表
    /// </summary>
    [TableName("d_ProJectInfo")]
    public class ProjectInfo : BaseModel
    {
        [Key]
        [Display(Name = @" ID")]
        public Guid FID { get; set; }
        [Display(Name = @" 项目名称")]
        public string FProName { get; set; }
        [Display(Name = @"访问URL")]
        public string FBaseUrl { get; set; }
        [Display(Name = @"添加时间")]
        public DateTime? FAddTime { get; set; }
    }
}

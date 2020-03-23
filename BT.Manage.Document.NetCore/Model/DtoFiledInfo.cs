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
    /// Dto信息
    /// </summary>
    [TableName("d_DtoFiledInfo")]
    public class DtoFiledInfo : BaseModel
    {
        [Key]
        [Display(Name = @" ID")]
        public Guid FID { get; set; }
        [Display(Name = @"外键关联方法信息")]
        public Guid FMethId { get; set; }
        [Display(Name = @"项目id")]
        public Guid? FProId { get; set; }
        [Display(Name = @" 字段名称")]
        public string FMemberName { get; set; }
        [Display(Name = @"字段描述")]
        public string FMemberDescibe { get; set; }
        [Display(Name = @"字段类型")]
        public string FMemberType { get; set; }
        [Display(Name = @"模块名称")]
        public string FMoudelName { get; set; }
        [Display(Name = @"输入/输出 1 输入 2 输出")]
        public int? FIsInOrOut { get; set; }
        [Display(Name = @"层级")]
        public int? FLayer { get; set; }
        [Display(Name = @"结构左值")]
        public int? FLeft { get; set; }
        [Display(Name = @"结构右值")]
        public int? FRight { get; set; }
        [Display(Name = @"添加时间")]
        public DateTime? FAddTime { get; set; }
        [Display(Name = @"所属类名称")]
        public string FClassName { get; set; }
    }
}

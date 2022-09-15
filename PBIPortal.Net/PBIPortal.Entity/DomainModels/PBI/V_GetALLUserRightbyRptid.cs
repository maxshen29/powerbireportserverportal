/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.Entity.SystemModels;

namespace PBIPortal.Entity.DomainModels
{
    [Entity(TableCnName = "报表权限视图",TableName = "V_GetALLUserRightbyRptid")]
    public class V_GetALLUserRightbyRptid:BaseEntity
    {
        /// <summary>
       ///用户登录名
       /// </summary>
       [Display(Name ="用户登录名")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string USER_Login_Name { get; set; }

       /// <summary>
       ///用户显示名
       /// </summary>
       [Display(Name ="用户显示名")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_TrueName { get; set; }

       /// <summary>
       ///用户ID
       /// </summary>
       [Display(Name ="用户ID")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int USER_ID { get; set; }

       /// <summary>
       ///部门ID
       /// </summary>
       [Display(Name ="部门ID")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string ODEPT_CODE { get; set; }

       /// <summary>
       ///报表ID
       /// </summary>
       [Display(Name ="报表ID")]
       [Column(TypeName="int")]
       public int? PBI_ID { get; set; }

       /// <summary>
       ///报表路径
       /// </summary>
       [Display(Name ="报表路径")]
       [Column(TypeName="varchar(max)")]
       public string Path { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="id")]
       [Column(TypeName="bigint")]
       [Required(AllowEmptyStrings=false)]
       public long id { get; set; }

       
    }
}
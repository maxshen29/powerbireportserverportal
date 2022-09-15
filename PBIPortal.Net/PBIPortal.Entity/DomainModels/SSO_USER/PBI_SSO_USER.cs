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
    [Entity(TableCnName = "SSO所有用户",TableName = "PBI_SSO_USER")]
    public class PBI_SSO_USER:BaseEntity
    {
        /// <summary>
       ///用户ID
       /// </summary>
       [Key]
       [Display(Name ="用户ID")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int USER_ID { get; set; }

       /// <summary>
       ///用户登录名
       /// </summary>
       [Display(Name ="用户登录名")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string USER_LOGIN_NAME { get; set; }

       /// <summary>
       ///部门ID
       /// </summary>
       [Display(Name ="部门ID")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string ODEPT_CODE { get; set; }

       /// <summary>
       ///用户显示名
       /// </summary>
       [Display(Name ="用户显示名")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string USER_TrueName { get; set; }

       /// <summary>
       ///身份证号码
       /// </summary>
       [Display(Name ="身份证号码")]
       [MaxLength(18)]
       [Column(TypeName="char(18)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string USER_IDCODE { get; set; }

       
    }
}
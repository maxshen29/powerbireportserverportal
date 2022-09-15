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
    [Entity(TableCnName = "SSO所有用户",TableName = "PBI_SSO_ALL")]
    public class PBI_SSO_ALL:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="USER_ID")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int USER_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_LOGIN_NAME")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_LOGIN_NAME { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_CODE")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string DEPT_CODE { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_SORT")]
       [Column(TypeName="smallint")]
       public int? USER_SORT { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_POST_CODE")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_POST_CODE { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_POST")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_POST { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_NAME")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string DEPT_NAME { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_LEVEL")]
       [Column(TypeName="tinyint")]
       public byte? DEPT_LEVEL { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_SORT")]
       [Column(TypeName="smallint")]
       public int? DEPT_SORT { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TDEPT_CODE")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string TDEPT_CODE { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ODEPT_CODE")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string ODEPT_CODE { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CODE_PATH")]
       [MaxLength(2000)]
       [Column(TypeName="nvarchar(2000)")]
       public string CODE_PATH { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_FLAG")]
       [Column(TypeName="tinyint")]
       public byte? DEPT_FLAG { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_PT_ID")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string DEPT_PT_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_LEVEL")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_LEVEL { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_LEVEL_NAME")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_LEVEL_NAME { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_POST_NAME")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_POST_NAME { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ORGI_DEPT_CODE")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string ORGI_DEPT_CODE { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DEPT_PCODE")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string DEPT_PCODE { get; set; }

       
    }
}
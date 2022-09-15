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
    [Entity(TableCnName = "部门表",TableName = "PBI_SSO_DEPT")]
    public class PBI_SSO_DEPT:BaseEntity
    {
        /// <summary>
       ///部门id
       /// </summary>
       [Key]
       [Display(Name ="部门id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int DEPT_ID { get; set; }

       /// <summary>
       ///部门代码
       /// </summary>
       [Display(Name ="部门代码")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DEPT_CODE { get; set; }

       /// <summary>
       ///部门名称
       /// </summary>
       [Display(Name ="部门名称")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DEPT_NAME { get; set; }

       /// <summary>
       ///部门层级
       /// </summary>
       [Display(Name ="部门层级")]
       [Column(TypeName="tinyint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public byte DEPT_LEVEL { get; set; }

       /// <summary>
       ///父部门ID
       /// </summary>
       [Display(Name ="父部门ID")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DEPT_PCODE { get; set; }

       
    }
}
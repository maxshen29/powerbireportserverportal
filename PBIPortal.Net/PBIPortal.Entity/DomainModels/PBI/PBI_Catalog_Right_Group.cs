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
    [Entity(TableCnName = "目录组权限表",TableName = "PBI_Catalog_Right_Group")]
    public class PBI_Catalog_Right_Group:BaseEntity
    {
        /// <summary>
       ///id
       /// </summary>
       [Key]
       [Display(Name ="id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int Id { get; set; }

       /// <summary>
       ///PBI目录ID
       /// </summary>
       [Display(Name ="PBI目录ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? CatalogId { get; set; }

       /// <summary>
       ///组名称
       /// </summary>
       [Display(Name ="组名称")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       [Editable(true)]
       public string Group_Name { get; set; }

       /// <summary>
       ///组ID
       /// </summary>
       [Display(Name ="组ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? Group_Id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifiedBy")]
       [MaxLength(30)]
       [Column(TypeName="varchar(30)")]
       public string ModifiedBy { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifiedDate")]
       [Column(TypeName="datetime")]
       public DateTime? ModifiedDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreatedBy")]
       [MaxLength(30)]
       [Column(TypeName="varchar(30)")]
       public string CreatedBy { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? CreatedDate { get; set; }

       
    }
}
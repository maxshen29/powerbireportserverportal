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
    [Entity(TableCnName = "PBI自定义目录管理",TableName = "PBI_Catalog")]
    public class PBI_Catalog:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Display(Name ="Name")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Name { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Description")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Description { get; set; }

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
       ///
       /// </summary>
       [Display(Name ="CreatedDate")]
       [Column(TypeName="datetime")]
       public DateTime? CreatedDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="CatalogId")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int CatalogId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ParentCatalogId")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? ParentCatalogId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Sort")]
       [Column(TypeName="int")]
       public int? Sort { get; set; }

       
    }
}
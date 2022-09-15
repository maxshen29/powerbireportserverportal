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
    [Entity(TableCnName = "PBI自定义报表管理",TableName = "PBI_CustormCatalogItems")]
    public class PBI_CustormCatalogItems:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="reportid")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid reportid { get; set; }

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
       [Display(Name ="Path")]
       [Column(TypeName="varchar(max)")]
       [Editable(true)]
       public string Path { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Type")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string Type { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Hidden")]
       [MaxLength(10)]
       [Column(TypeName="varchar(10)")]
       public string Hidden { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Size")]
       [Column(TypeName="int")]
       public int? Size { get; set; }

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
       [Display(Name ="ParentFolderId")]
       [Column(TypeName="uniqueidentifier")]
       public Guid? ParentFolderId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ParentCustormCatalogId")]
       [Column(TypeName="int")]
       public int? ParentCustormCatalogId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="IsFavorite")]
       [MaxLength(10)]
       [Column(TypeName="varchar(10)")]
       public string IsFavorite { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Roles")]
       [Column(TypeName="varchar(max)")]
       public string Roles { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ContentType")]
       [MaxLength(1000)]
       [Column(TypeName="varchar(1000)")]
       public string ContentType { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Content")]
       [MaxLength(1000)]
       [Column(TypeName="varchar(1000)")]
       public string Content { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Sort")]
       [Column(TypeName="int")]
       public int? Sort { get; set; }

       
    }
}
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
    [Entity(TableCnName = "PBI目录用户权限",TableName = "PBI_Catalog_Right_User")]
    public class PBI_Catalog_Right_User:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="Id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int Id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CatalogId")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? CatalogId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_LOGIN_NAME")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string USER_LOGIN_NAME { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_TrueName")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string USER_TrueName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="User_Id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? User_Id { get; set; }

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
       [Editable(true)]
       public DateTime? CreatedDate { get; set; }

       
    }
}
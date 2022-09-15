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
    [Entity(TableCnName = "报表行角色和组管理",TableName = "PBI_CatalogItems_Role_Group")]
    public class PBI_CatalogItems_Role_Group:BaseEntity
    {
        /// <summary>
       ///ID
       /// </summary>
       [Key]
       [Display(Name ="ID")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int ID { get; set; }

       /// <summary>
       ///报表ID
       /// </summary>
       [Display(Name ="报表ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? PBI_ID { get; set; }

       /// <summary>
       ///报表名称
       /// </summary>
       [Display(Name ="报表名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       [Editable(true)]
       public string PBI_Name { get; set; }

       /// <summary>
       ///报表路径
       /// </summary>
       [Display(Name ="报表路径")]
       [MaxLength(2000)]
       [Column(TypeName="nvarchar(2000)")]
       [Editable(true)]
       public string PBI_Path { get; set; }

       /// <summary>
       ///组名称
       /// </summary>
       [Display(Name ="组名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
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
       ///报表角色名称
       /// </summary>
       [Display(Name ="报表角色名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       [Editable(true)]
       public string PBI_RoleName { get; set; }

       /// <summary>
       ///报表角色ID
       /// </summary>
       [Display(Name ="报表角色ID")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       public Guid? PBI_RoleID { get; set; }

       /// <summary>
       ///创建日期
       /// </summary>
       [Display(Name ="创建日期")]
       [Column(TypeName="datetime")]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateID")]
       [Column(TypeName="int")]
       public int? CreateID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Creator")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string Creator { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Modifier")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string Modifier { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifyDate")]
       [Column(TypeName="datetime")]
       public DateTime? ModifyDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifyID")]
       [Column(TypeName="int")]
       public int? ModifyID { get; set; }

       
    }
}
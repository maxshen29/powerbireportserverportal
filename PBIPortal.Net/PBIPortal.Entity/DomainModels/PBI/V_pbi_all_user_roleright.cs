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
    [Entity(TableCnName = "角色权限视图",TableName = "V_pbi_all_user_roleright")]
    public class V_pbi_all_user_roleright:BaseEntity
    {
        /// <summary>
       ///报表ID
       /// </summary>
       [Display(Name ="报表ID")]
       [Column(TypeName="int")]
       public int? PBI_ID { get; set; }

       /// <summary>
       ///报表名称
       /// </summary>
       [Display(Name ="报表名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string PBI_Name { get; set; }

       /// <summary>
       ///报表路径
       /// </summary>
       [Display(Name ="报表路径")]
       [MaxLength(2000)]
       [Column(TypeName="nvarchar(2000)")]
       public string PBI_Path { get; set; }

       /// <summary>
       ///登录名
       /// </summary>
       [Display(Name ="登录名")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string User_LoginName { get; set; }

       /// <summary>
       ///报表角色名称
       /// </summary>
       [Display(Name ="报表角色名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string PBI_RoleName { get; set; }

       /// <summary>
       ///报表角色ID
       /// </summary>
       [Display(Name ="报表角色ID")]
       [Column(TypeName="uniqueidentifier")]
       public Guid? PBI_RoleID { get; set; }

       /// <summary>
       ///ID
       /// </summary>
       [Key]
       [Display(Name ="ID")]
       [Column(TypeName="bigint")]
       [Required(AllowEmptyStrings=false)]
       public long id { get; set; }

       
    }
}
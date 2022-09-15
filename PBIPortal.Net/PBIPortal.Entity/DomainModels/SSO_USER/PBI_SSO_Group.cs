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
    [Entity(TableCnName = "用户组管理",TableName = "PBI_SSO_Group")]
    public class PBI_SSO_Group:BaseEntity
    {
        /// <summary>
       ///群组ID
       /// </summary>
       [Key]
       [Display(Name ="群组ID")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int Group_Id { get; set; }

       /// <summary>
       ///群组名称
       /// </summary>
       [Display(Name ="群组名称")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Group_Name { get; set; }

       /// <summary>
       ///群组级别
       /// </summary>
       [Display(Name ="群组级别")]
       [Column(TypeName="int")]
       public int? Group_Level { get; set; }

       /// <summary>
       ///父ID（未用）
       /// </summary>
       [Display(Name ="父ID（未用）")]
       [Column(TypeName="int")]
       public int? Group_PID { get; set; }

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

       /// <summary>
       ///优先级
       /// </summary>
       [Display(Name ="优先级")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Group_Priority { get; set; }

       /// <summary>
       ///首页路径
       /// </summary>
       [Display(Name ="首页路径")]
       [MaxLength(1000)]
       [Column(TypeName="nvarchar(1000)")]
       [Editable(true)]
       public string PBI_Index_Path { get; set; }

       /// <summary>
       ///首页报表类型
       /// </summary>
       [Display(Name ="首页报表类型")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string RPT_Type { get; set; }

       
    }
}
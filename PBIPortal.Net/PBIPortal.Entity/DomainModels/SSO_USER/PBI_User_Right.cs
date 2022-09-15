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
    [Entity(TableCnName = "用户权限",TableName = "PBI_User_Right")]
    public class PBI_User_Right:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="R_Id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int R_Id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PBI_ID")]
       [Column(TypeName="int")]
       public int? PBI_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_ID")]
       [Column(TypeName="int")]
       public int? USER_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Right")]
       [Column(TypeName="int")]
       public int? Right { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Path")]
       [Column(TypeName="varchar(max)")]
       public string Path { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateDate")]
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
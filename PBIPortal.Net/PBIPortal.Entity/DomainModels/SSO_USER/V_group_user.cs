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
    [Entity(TableCnName = "组和用户视图",TableName = "V_group_user")]
    public class V_group_user:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Display(Name ="id")]
       [Column(TypeName="int")]
       public int? id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="name")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       public string name { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="truename")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       public string truename { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="pid")]
       [Column(TypeName="int")]
       public int? pid { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateDate")]
       [Column(TypeName="datetime")]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="type")]
       [MaxLength(4)]
       [Column(TypeName="varchar(4)")]
       [Required(AllowEmptyStrings=false)]
       public string type { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="vid")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int vid { get; set; }

       
    }
}
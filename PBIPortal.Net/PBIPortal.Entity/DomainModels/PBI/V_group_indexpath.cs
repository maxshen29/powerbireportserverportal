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
    [Entity(TableCnName = "PBI组首页地址",TableName = "V_group_indexpath")]
    public class V_group_indexpath:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Display(Name ="Group_ID")]
       [Column(TypeName="int")]
       public int? Group_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_Login_Name")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       public string USER_Login_Name { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PBI_Index_Path")]
       [MaxLength(1000)]
       [Column(TypeName="nvarchar(1000)")]
       public string PBI_Index_Path { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Group_Priority")]
       [Column(TypeName="int")]
       public int? Group_Priority { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="id")]
       [Column(TypeName="bigint")]
       [Required(AllowEmptyStrings=false)]
       public long id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="rpt_type")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string rpt_type { get; set; }

       
    }
}
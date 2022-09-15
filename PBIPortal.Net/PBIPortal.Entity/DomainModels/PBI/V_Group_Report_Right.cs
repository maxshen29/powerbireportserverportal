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
    [Entity(TableCnName = "组权限视图",TableName = "V_Group_Report_Right")]
    public class V_Group_Report_Right:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Display(Name ="Group_Id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int Group_Id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Group_Name")]
       [MaxLength(400)]
       [Column(TypeName="nvarchar(400)")]
       public string Group_Name { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Name")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string Name { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PBI_ID")]
       [Column(TypeName="int")]
       public int? PBI_ID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="R_Id")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int R_Id { get; set; }

       
    }
}
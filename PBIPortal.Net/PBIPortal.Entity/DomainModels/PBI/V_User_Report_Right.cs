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
    [Entity(TableCnName = "用户权限表视图",TableName = "V_User_Report_Right")]
    public class V_User_Report_Right:BaseEntity
    {
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

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_TrueName")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string USER_TrueName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="USER_LOGIN_NAME")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       [Required(AllowEmptyStrings=false)]
       public string USER_LOGIN_NAME { get; set; }

       
    }
}
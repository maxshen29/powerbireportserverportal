using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.Entity;

namespace PBIPortal.Entity.DomainModels
{
    public class ApiUserEditInput
    {

        [Column(TypeName = "int")]
        public int EditType { get; set; }
        [Column(TypeName = "Nvarchar(200)")]
        public string Value { get; set; }
    }
}

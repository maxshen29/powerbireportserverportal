using System;
using System.Collections.Generic;
using System.Text;

namespace PBIPortal.Entity.DomainModels.PBI
{
    public class Policie
    {
        public string GroupUserName { get; set; }
        public List<PBIRoles> Roles { get; set; }


    }
}

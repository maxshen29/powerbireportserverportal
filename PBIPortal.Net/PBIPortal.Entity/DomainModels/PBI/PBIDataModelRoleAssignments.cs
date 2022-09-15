using System;
using System.Collections.Generic;
using System.Text;

namespace PBIPortal.Entity.DomainModels.PBI
{
    public class PBIDataModelRoleAssignments
    {
        public string GroupUserName { get; set; }
        public Guid[] DataModelRoles { get; set; }
    }
}

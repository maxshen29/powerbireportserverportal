using System;
using System.Collections.Generic;
using System.Text;

namespace PBIPortal.Entity.DomainModels.PBI
{
    public class PBIPolicie
    {
        public bool InheritParentPolicy { get; set; }

        public List<Policie> Policies { get; set; }
       
    }
}

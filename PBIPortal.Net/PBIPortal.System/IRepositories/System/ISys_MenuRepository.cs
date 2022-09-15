using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Extensions.AutofacManager;
namespace PBIPortal.System.IRepositories
{
    public partial interface ISys_MenuRepository : IDependency,IRepository<Sys_Menu>
    {
    }
}


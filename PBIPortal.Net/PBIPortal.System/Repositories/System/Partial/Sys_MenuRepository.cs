using PBIPortal.System.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Repositories
{
    public partial class Sys_MenuRepository
    {
        public override PBIPortalContext DbContext => base.DbContext;
    }
}


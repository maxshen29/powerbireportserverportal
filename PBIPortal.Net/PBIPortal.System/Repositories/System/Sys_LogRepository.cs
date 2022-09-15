using PBIPortal.System.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Repositories
{
    public partial class Sys_LogRepository : RepositoryBase<Sys_Log>, ISys_LogRepository
    {
        public Sys_LogRepository(PBIPortalContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_LogRepository GetService
        {
            get { return AutofacContainerModule.GetService<ISys_LogRepository>(); }
        }
    }
}


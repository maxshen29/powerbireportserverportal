using DairyStar.Builder.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace DairyStar.Builder.Repositories
{
    public partial class Sys_TableInfoRepository : RepositoryBase<Sys_TableInfo>, ISys_TableInfoRepository
    {
        public Sys_TableInfoRepository(PBIPortalContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_TableInfoRepository GetService
        {
            get { return AutofacContainerModule.GetService<ISys_TableInfoRepository>(); }
        }
    }
}


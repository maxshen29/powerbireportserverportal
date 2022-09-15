/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹PBI_SSO_USERRepository编写代码
 */
using PBI.APP.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Repositories
{
    public partial class PBI_SSO_USERRepository : RepositoryBase<PBI_SSO_USER> , IPBI_SSO_USERRepository
    {
    public PBI_SSO_USERRepository(PBIPortalContext dbContext)
    : base(dbContext)
    {

    }
    public static IPBI_SSO_USERRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IPBI_SSO_USERRepository>(); } }
    }
}

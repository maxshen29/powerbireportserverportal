/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹PBI_CatalogItemsRepository编写代码
 */
using PBI.APP.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Repositories
{
    public partial class PBI_CatalogItemsRepository : RepositoryBase<PBI_CatalogItems> , IPBI_CatalogItemsRepository
    {
    public PBI_CatalogItemsRepository(PBIPortalContext dbContext)
    : base(dbContext)
    {

    }
    public static IPBI_CatalogItemsRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IPBI_CatalogItemsRepository>(); } }
    }
}

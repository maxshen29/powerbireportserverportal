/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_CatalogItemsService与IPBI_CatalogItemsService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogItemsService : ServiceBase<PBI_CatalogItems, IPBI_CatalogItemsRepository>, IPBI_CatalogItemsService, IDependency
    {
        public PBI_CatalogItemsService(IPBI_CatalogItemsRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_CatalogItemsService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_CatalogItemsService>(); }
        }
    }
}

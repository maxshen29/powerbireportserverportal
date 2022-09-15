/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_CustormCatalogItemsService与IPBI_CustormCatalogItemsService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_CustormCatalogItemsService : ServiceBase<PBI_CustormCatalogItems, IPBI_CustormCatalogItemsRepository>, IPBI_CustormCatalogItemsService, IDependency
    {
        public PBI_CustormCatalogItemsService(IPBI_CustormCatalogItemsRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_CustormCatalogItemsService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_CustormCatalogItemsService>(); }
        }
    }
}

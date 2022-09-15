/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_CatalogService与IPBI_CatalogService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogService : ServiceBase<PBI_Catalog, IPBI_CatalogRepository>, IPBI_CatalogService, IDependency
    {
        public PBI_CatalogService(IPBI_CatalogRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_CatalogService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_CatalogService>(); }
        }
    }
}

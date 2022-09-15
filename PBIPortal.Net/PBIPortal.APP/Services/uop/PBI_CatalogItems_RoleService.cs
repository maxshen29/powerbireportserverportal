/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_CatalogItems_RoleService与IPBI_CatalogItems_RoleService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogItems_RoleService : ServiceBase<PBI_CatalogItems_Role, IPBI_CatalogItems_RoleRepository>, IPBI_CatalogItems_RoleService, IDependency
    {
        public PBI_CatalogItems_RoleService(IPBI_CatalogItems_RoleRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_CatalogItems_RoleService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_CatalogItems_RoleService>(); }
        }
    }
}

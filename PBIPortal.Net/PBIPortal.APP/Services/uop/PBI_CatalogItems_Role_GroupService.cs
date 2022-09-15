/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_CatalogItems_Role_GroupService与IPBI_CatalogItems_Role_GroupService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogItems_Role_GroupService : ServiceBase<PBI_CatalogItems_Role_Group, IPBI_CatalogItems_Role_GroupRepository>, IPBI_CatalogItems_Role_GroupService, IDependency
    {
        public PBI_CatalogItems_Role_GroupService(IPBI_CatalogItems_Role_GroupRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_CatalogItems_Role_GroupService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_CatalogItems_Role_GroupService>(); }
        }
    }
}

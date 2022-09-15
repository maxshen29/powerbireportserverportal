/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_Catalog_Right_GroupService与IPBI_Catalog_Right_GroupService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_Catalog_Right_GroupService : ServiceBase<PBI_Catalog_Right_Group, IPBI_Catalog_Right_GroupRepository>, IPBI_Catalog_Right_GroupService, IDependency
    {
        public PBI_Catalog_Right_GroupService(IPBI_Catalog_Right_GroupRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_Catalog_Right_GroupService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_Catalog_Right_GroupService>(); }
        }
    }
}

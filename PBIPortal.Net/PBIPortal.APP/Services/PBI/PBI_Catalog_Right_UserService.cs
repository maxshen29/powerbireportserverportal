/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_Catalog_Right_UserService与IPBI_Catalog_Right_UserService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_Catalog_Right_UserService : ServiceBase<PBI_Catalog_Right_User, IPBI_Catalog_Right_UserRepository>, IPBI_Catalog_Right_UserService, IDependency
    {
        public PBI_Catalog_Right_UserService(IPBI_Catalog_Right_UserRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_Catalog_Right_UserService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_Catalog_Right_UserService>(); }
        }
    }
}

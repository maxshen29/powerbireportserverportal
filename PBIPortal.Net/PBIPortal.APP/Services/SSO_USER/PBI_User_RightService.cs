/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_User_RightService与IPBI_User_RightService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_User_RightService : ServiceBase<PBI_User_Right, IPBI_User_RightRepository>, IPBI_User_RightService, IDependency
    {
        public PBI_User_RightService(IPBI_User_RightRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_User_RightService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_User_RightService>(); }
        }
    }
}

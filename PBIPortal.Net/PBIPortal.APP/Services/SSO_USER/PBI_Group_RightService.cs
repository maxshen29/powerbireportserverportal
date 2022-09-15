/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_Group_RightService与IPBI_Group_RightService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_Group_RightService : ServiceBase<PBI_Group_Right, IPBI_Group_RightRepository>, IPBI_Group_RightService, IDependency
    {
        public PBI_Group_RightService(IPBI_Group_RightRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_Group_RightService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_Group_RightService>(); }
        }
    }
}

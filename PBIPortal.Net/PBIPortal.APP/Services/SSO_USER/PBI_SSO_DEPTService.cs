/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_SSO_DEPTService与IPBI_SSO_DEPTService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_DEPTService : ServiceBase<PBI_SSO_DEPT, IPBI_SSO_DEPTRepository>, IPBI_SSO_DEPTService, IDependency
    {
        public PBI_SSO_DEPTService(IPBI_SSO_DEPTRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_SSO_DEPTService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_SSO_DEPTService>(); }
        }
    }
}

/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下PBI_SSO_ALLService与IPBI_SSO_ALLService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_ALLService : ServiceBase<PBI_SSO_ALL, IPBI_SSO_ALLRepository>, IPBI_SSO_ALLService, IDependency
    {
        public PBI_SSO_ALLService(IPBI_SSO_ALLRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IPBI_SSO_ALLService Instance
        {
           get { return AutofacContainerModule.GetService<IPBI_SSO_ALLService>(); }
        }
    }
}

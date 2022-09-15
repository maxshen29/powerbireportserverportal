using PBIPortal.System.IRepositories;
using PBIPortal.System.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Services
{
    public partial class Sys_LogService : ServiceBase<Sys_Log, ISys_LogRepository>, ISys_LogService, IDependency
    {
        public Sys_LogService(ISys_LogRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static ISys_LogService Instance
        {
           get { return AutofacContainerModule.GetService<ISys_LogService>(); }
        }
    }
}


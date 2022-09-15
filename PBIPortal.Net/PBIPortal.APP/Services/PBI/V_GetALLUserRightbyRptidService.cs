/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下V_GetALLUserRightbyRptidService与IV_GetALLUserRightbyRptidService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class V_GetALLUserRightbyRptidService : ServiceBase<V_GetALLUserRightbyRptid, IV_GetALLUserRightbyRptidRepository>, IV_GetALLUserRightbyRptidService, IDependency
    {
        public V_GetALLUserRightbyRptidService(IV_GetALLUserRightbyRptidRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IV_GetALLUserRightbyRptidService Instance
        {
           get { return AutofacContainerModule.GetService<IV_GetALLUserRightbyRptidService>(); }
        }
    }
}

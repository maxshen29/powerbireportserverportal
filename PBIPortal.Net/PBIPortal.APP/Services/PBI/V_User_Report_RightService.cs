/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下V_User_Report_RightService与IV_User_Report_RightService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class V_User_Report_RightService : ServiceBase<V_User_Report_Right, IV_User_Report_RightRepository>, IV_User_Report_RightService, IDependency
    {
        public V_User_Report_RightService(IV_User_Report_RightRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IV_User_Report_RightService Instance
        {
           get { return AutofacContainerModule.GetService<IV_User_Report_RightService>(); }
        }
    }
}

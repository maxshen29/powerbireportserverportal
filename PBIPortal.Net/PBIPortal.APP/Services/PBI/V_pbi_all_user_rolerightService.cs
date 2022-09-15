/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下V_pbi_all_user_rolerightService与IV_pbi_all_user_rolerightService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class V_pbi_all_user_rolerightService : ServiceBase<V_pbi_all_user_roleright, IV_pbi_all_user_rolerightRepository>, IV_pbi_all_user_rolerightService, IDependency
    {
        public V_pbi_all_user_rolerightService(IV_pbi_all_user_rolerightRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IV_pbi_all_user_rolerightService Instance
        {
           get { return AutofacContainerModule.GetService<IV_pbi_all_user_rolerightService>(); }
        }
    }
}

/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下V_group_userService与IV_group_userService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class V_group_userService : ServiceBase<V_group_user, IV_group_userRepository>, IV_group_userService, IDependency
    {
        public V_group_userService(IV_group_userRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IV_group_userService Instance
        {
           get { return AutofacContainerModule.GetService<IV_group_userService>(); }
        }
    }
}

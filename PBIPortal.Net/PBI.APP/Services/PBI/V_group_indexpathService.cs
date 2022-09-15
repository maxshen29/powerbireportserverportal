/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下V_group_indexpathService与IV_group_indexpathService中编写
 */
using PBI.APP.IRepositories;
using PBI.APP.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Services
{
    public partial class V_group_indexpathService : ServiceBase<V_group_indexpath, IV_group_indexpathRepository>, IV_group_indexpathService, IDependency
    {
        public V_group_indexpathService(IV_group_indexpathRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static IV_group_indexpathService Instance
        {
           get { return AutofacContainerModule.GetService<IV_group_indexpathService>(); }
        }
    }
}

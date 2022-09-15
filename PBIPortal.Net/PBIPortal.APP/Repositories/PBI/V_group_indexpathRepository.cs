/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹V_group_indexpathRepository编写代码
 */
using PBI.APP.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Repositories
{
    public partial class V_group_indexpathRepository : RepositoryBase<V_group_indexpath> , IV_group_indexpathRepository
    {
    public V_group_indexpathRepository(PBIPortalContext dbContext)
    : base(dbContext)
    {

    }
    public static IV_group_indexpathRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IV_group_indexpathRepository>(); } }
    }
}

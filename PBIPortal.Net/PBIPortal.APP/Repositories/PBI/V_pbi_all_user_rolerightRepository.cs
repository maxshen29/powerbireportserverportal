/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹V_pbi_all_user_rolerightRepository编写代码
 */
using PBI.APP.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Repositories
{
    public partial class V_pbi_all_user_rolerightRepository : RepositoryBase<V_pbi_all_user_roleright> , IV_pbi_all_user_rolerightRepository
    {
    public V_pbi_all_user_rolerightRepository(PBIPortalContext dbContext)
    : base(dbContext)
    {

    }
    public static IV_pbi_all_user_rolerightRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IV_pbi_all_user_rolerightRepository>(); } }
    }
}

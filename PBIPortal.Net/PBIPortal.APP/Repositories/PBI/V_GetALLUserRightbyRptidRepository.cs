/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹V_GetALLUserRightbyRptidRepository编写代码
 */
using PBI.APP.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Repositories
{
    public partial class V_GetALLUserRightbyRptidRepository : RepositoryBase<V_GetALLUserRightbyRptid> , IV_GetALLUserRightbyRptidRepository
    {
    public V_GetALLUserRightbyRptidRepository(PBIPortalContext dbContext)
    : base(dbContext)
    {

    }
    public static IV_GetALLUserRightbyRptidRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IV_GetALLUserRightbyRptidRepository>(); } }
    }
}

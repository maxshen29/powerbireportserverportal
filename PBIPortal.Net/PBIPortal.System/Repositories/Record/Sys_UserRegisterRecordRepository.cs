/*
 *Author：jxx
 *Contact：283591387@qq.com
 *Date：2018-07-01
 * 此代码由框架生成，请勿随意更改
 */
using PBIPortal.System.IRepositories;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Repositories
{
    public partial class Sys_UserRegisterRecordRepository : RepositoryBase<Sys_UserRegisterRecord>, ISys_UserRegisterRecordRepository
    {
        public Sys_UserRegisterRecordRepository(PBIPortalContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_UserRegisterRecordRepository Instance
        {
            get { return AutofacContainerModule.GetService<ISys_UserRegisterRecordRepository>(); }
        }
    }
}


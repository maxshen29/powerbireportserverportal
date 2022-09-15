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
    public partial class vSys_DictionaryRepository : RepositoryBase<vSys_Dictionary>, IvSys_DictionaryRepository
    {
        public vSys_DictionaryRepository(PBIPortalContext dbContext)
        : base(dbContext)
        {

        }
        public static IvSys_DictionaryRepository Instance
        {
            get { return AutofacContainerModule.GetService<IvSys_DictionaryRepository>(); }
        }
    }
}


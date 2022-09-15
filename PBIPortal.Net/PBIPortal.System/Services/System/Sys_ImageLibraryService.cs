/*
 *Author：jxx
 *Contact：283591387@qq.com
 *Date：2018-07-01
 * 此代码由框架生成，请勿随意更改
 */
using PBIPortal.System.IRepositories;
using PBIPortal.System.IServices;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Services
{
    public partial class Sys_ImageLibraryService : ServiceBase<Sys_ImageLibrary, ISys_ImageLibraryRepository>, ISys_ImageLibraryService, IDependency
    {
        public Sys_ImageLibraryService(ISys_ImageLibraryRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static ISys_ImageLibraryService Instance
        {
           get { return AutofacContainerModule.GetService<ISys_ImageLibraryService>(); }
        }
    }
}


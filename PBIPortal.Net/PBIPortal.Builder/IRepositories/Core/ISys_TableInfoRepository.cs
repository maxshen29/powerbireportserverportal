using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Extensions.AutofacManager;
namespace DairyStar.Builder.IRepositories
{
    public partial interface ISys_TableInfoRepository : IDependency,IRepository<Sys_TableInfo>
    {
    }
}


/*
*所有关于PBI_Group_User类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBI.APP.IServices
{
    public partial interface IPBI_Group_UserService
    {
        Task<WebResponseContent> InsertUser(List<PBI_Group_User> listObject);
    }
 }

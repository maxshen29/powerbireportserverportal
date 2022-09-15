/*
*所有关于PBI_Catalog_Right_User类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PBI.APP.IServices
{
    public partial interface IPBI_Catalog_Right_UserService
    {

        Task<WebResponseContent> InsertUserRight(List<PBI_Catalog_Right_User> listObjects);

    }
 }

/*
*所有关于PBI_CatalogItems_Role_Group类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PBI.APP.IServices
{
    public partial interface IPBI_CatalogItems_Role_GroupService
    {
        Task<WebResponseContent> InsertGroupRoleRight(List<PBI_CatalogItems_Role_Group> listObject);
    }
 }

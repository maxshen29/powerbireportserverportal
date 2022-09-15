/*
*所有关于PBI_SSO_Group类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using PBIPortal.Entity.DomainModels.PBI;
using System.Threading.Tasks;

namespace PBI.APP.IServices
{
    public partial interface IPBI_SSO_GroupService
    {
        Task<WebResponseContent> GetPBICatalogItems(PBIItem pBIItem);

        Task<WebResponseContent> AddGroup(PBI_SSO_Group pBI_SSO_Group);
    }
 }

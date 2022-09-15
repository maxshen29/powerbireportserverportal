/*
*所有关于PBI_SSO_DEPT类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.IServices
{
    public partial interface IPBI_SSO_DEPTService
    {
        Task<WebResponseContent> GetPBICatalogItems(PBIItem pBIItem);
        Task<WebResponseContent> GetALLDept();
    }
 }

/*
*所有关于PBI_CustormCatalogItems类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PBI.APP.IServices
{
    public partial interface IPBI_CustormCatalogItemsService
    {
        Task<WebResponseContent> AddCustormReport(PBI_CustormCatalogItems pBIItem);
        Task<WebResponseContent> GetPBICatalog(int pBIItem);
        Task<WebResponseContent> UpdateCustormReport(PBI_CustormCatalogItems pBIItem);
        Task<WebResponseContent> GetALLRightCatalogPBI();
    }
  
}

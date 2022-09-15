/*
*所有关于PBI_Catalog类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PBI.APP.IServices 
{ 
    public partial interface IPBI_CatalogService
    {

        Task<WebResponseContent> GetPBICatalog(int pBIItem);
        Task<WebResponseContent> AddCustormCatalog(PBI_Catalog pBIItem);
        Task<WebResponseContent> UpdatePBICatalog(PBI_Catalog pBIItem);
        Task<WebResponseContent> GetALLCatalogbyUser();


    }
        
 }

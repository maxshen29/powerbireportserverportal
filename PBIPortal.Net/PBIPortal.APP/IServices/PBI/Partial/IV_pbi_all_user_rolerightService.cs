/*
*所有关于V_pbi_all_user_roleright类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.IServices
{
    public partial interface IV_pbi_all_user_rolerightService
    {
        Task<WebResponseContent> SetRolesALLReportsUserRight();
        Task<WebResponseContent> SetReportRolePBI(PBIItem pBIItem);
    }
 }

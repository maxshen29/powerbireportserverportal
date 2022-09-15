/*
*所有关于PBI_Group_Right类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PBI.APP.IServices
{
    public partial interface IPBI_Group_RightService
    {
        Task<WebResponseContent> InsertGroupRight(List<PBI_Group_Right> listObject);
    }
 }

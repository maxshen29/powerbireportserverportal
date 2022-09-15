/*
*所有关于V_group_indexpath类的业务代码接口应在此处编写
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PBI.APP.IServices 
{
    public partial interface IV_group_indexpathService
    {

        Task<WebResponseContent> GeIndexPath();
    }
 }

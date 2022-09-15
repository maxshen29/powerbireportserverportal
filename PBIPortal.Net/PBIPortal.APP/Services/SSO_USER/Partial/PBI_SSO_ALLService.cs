/*
 *所有关于PBI_SSO_ALL类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_SSO_ALLService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;
using System.Linq;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using PBIPortal.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PBIPortal.Core.Enums;
using PBIPortal.Entity.DomainModels.SSO_USER;
using System;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_ALLService
    {
        public  Task<WebResponseContent> GetALL()
        {

            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                // var result = repository.FindAsIQueryable(x => 1 == 1).Select(p => new Tree { id = p.DEPT_ID, label = p.DEPT_NAME, name = p.DEPT_NAME, pid = (int)p.PDEPT_ID });
                var result = repository.FindAsIQueryable(x => 1 == 1).Select(p => new Tree { id = p.USER_ID.ToString(), label = p.USER_LOGIN_NAME, name = p.USER_LOGIN_NAME, pid = p.DEPT_PT_ID.ToString(),icon= "ios-man" });

                return Task.FromResult(responseContent.OK(null, result));
            }
            catch (Exception ex)
            {
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError+ex.Message));
            }

        }


    }
}

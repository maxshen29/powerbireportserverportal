/*
 *所有关于V_group_indexpath类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*V_group_indexpathService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Core.ManageUser;
using System;
using PBIPortal.Core.Enums;
using Microsoft.Extensions.Logging;
using PBIPortal.Core.Services;

namespace PBI.APP.Services
{
    public partial class V_group_indexpathService
    {
        public Task<WebResponseContent> GeIndexPath()
        { 
            string msg = string.Empty;
            string username = UserContext.Current.UserName;
            object result;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
               
                //   var result = repository.FindAsIQueryable(x => x.Group_PID == pid);
                
                result = repository.FindAsIQueryable(x => x.USER_Login_Name == username).OrderBy(x=>x.Group_Priority).Take(1);     
                return Task.FromResult(responseContent.OK(null, result));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, username,"", msg);
            }
        }
    }
}

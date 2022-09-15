/*
 *所有关于PBI_CatalogItems_Role类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_CatalogItems_RoleService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System.Collections.Generic;
using System;
using PBIPortal.Core.Services;
using PBIPortal.Core.Enums;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogItems_RoleService
    {


        public Task<WebResponseContent> InsertRoleUserRight(List<PBI_CatalogItems_Role> listObject)
        {
            List<PBI_CatalogItems_Role> templist = new List<PBI_CatalogItems_Role>();
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                foreach (PBI_CatalogItems_Role t in listObject)
                {
                    if (!repository.Exists(x => x.PBI_ID == t.PBI_ID && x.PBI_RoleID == t.PBI_RoleID&&x.User_LoginName==t.User_LoginName))
                    {
                        t.CreateDate = DateTime.Now;
                        templist.Add(t);
                    }
                }
                repository.AddRange(templist, true);

                return Task.FromResult(responseContent.OK(null, null));
            }
            catch (Exception ex)
            {

                Logger.Info(LoggerType.Info, listObject.ToString(), responseContent.Message, ex.Message);
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }

        }
    }
}

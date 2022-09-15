/*
 *所有关于PBI_CustormCatalogItems类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_CustormCatalogItemsService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using PBIPortal.Core.ManageUser;

namespace PBI.APP.Services
{
    public partial class PBI_CustormCatalogItemsService
    {
        public Task<WebResponseContent> AddCustormReport(PBI_CustormCatalogItems pBIItem)
        {

            string msg = string.Empty;
            pBIItem.Path = pBIItem.Path.Trim();

            pBIItem.Name = pBIItem.Name.Trim();
            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                if (repository.Exists(x => x.Name == pBIItem.Name&& x.ParentCustormCatalogId==pBIItem.ParentCustormCatalogId))
                {
                    return Task.FromResult(responseContent.Error("报表名称不能重复！"));
                }
                if (repository.Exists(x => x.Path == pBIItem.Path && x.ParentCustormCatalogId == pBIItem.ParentCustormCatalogId))
                {
                    return Task.FromResult(responseContent.Error("报表路径不能重复！"));
                }
                repository.Add(pBIItem, true);
                return Task.FromResult(responseContent.OK(null, null));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, pBIItem.CreatedDate.ToString(), responseContent.Message, msg);
            }

        }


        public Task<WebResponseContent> UpdateCustormReport(PBI_CustormCatalogItems pBIItem)
        {

            string msg = string.Empty;
            pBIItem.Path = pBIItem.Path.Trim();

            pBIItem.Name = pBIItem.Name.Trim();
            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                if (repository.Exists(x => x.Name == pBIItem.Name && x.ParentCustormCatalogId == pBIItem.ParentCustormCatalogId&&x.id!=pBIItem.id))
                {
                    return Task.FromResult(responseContent.Error("报表名称不能重复！"));
                }
                if (repository.Exists(x => x.Path == pBIItem.Path && x.ParentCustormCatalogId == pBIItem.ParentCustormCatalogId && x.id != pBIItem.id))
                {
                    return Task.FromResult(responseContent.Error("报表路径不能重复！"));
                }
                repository.Update(pBIItem, true);
                return Task.FromResult(responseContent.OK(null, null));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, pBIItem.CreatedDate.ToString(), responseContent.Message, msg);
            }

        }


        public Task<WebResponseContent> GetPBICatalog(int pBIItem)
        {

            string msg = string.Empty;
          
            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                 

                var result = repository.FindAsIQueryable(x => x.ParentCustormCatalogId == pBIItem).OrderByDescending(x=>x.Sort);     
                return Task.FromResult(responseContent.OK(null, result));



            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {

                Logger.Info(LoggerType.Info, pBIItem.ToString(), responseContent.Message, msg);
            }

        }

        /// <summary>
        /// 获取用户具备目录访问报表列表
        /// </summary>
        /// <returns></returns>
        public Task<WebResponseContent> GetALLRightCatalogPBI()
        {

            string msg = string.Empty;
       
            string username = UserContext.Current.UserName.ToLower();
            List<PBI_CustormCatalogItems> pBI_CustormCatalogItems = new List<PBI_CustormCatalogItems>();

            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                string sqlforgroupuser = $"select b.* from [dbo].[PBI_Catalog_Right_Group] a,  [dbo].[PBI_CustormCatalogItems] b ,[PBI_Group_User] c where a.CatalogId=b.ParentCustormCatalogId and a.Group_Id = c.Group_ID and c.user_login_name='{username}'   order by Sort desc";
                pBI_CustormCatalogItems = repository.FromSql(sqlforgroupuser);
                
                string sqlforuser = $"select b.* from [dbo].[PBI_Catalog_Right_User] a,  [dbo].[PBI_CustormCatalogItems] b  where a.CatalogId=b.ParentCustormCatalogId and a.user_login_name='{username}'   order by Sort desc";
                pBI_CustormCatalogItems.AddRange(repository.FromSql(sqlforuser));

                return Task.FromResult(responseContent.OK(null, pBI_CustormCatalogItems));
                 

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {

                Logger.Info(LoggerType.Info, "",responseContent.Message, msg);
            }

        }









    }
}

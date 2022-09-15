/*
 *所有关于PBI_Catalog类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_CatalogService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using Microsoft.Extensions.Logging;
using PBIPortal.Core.Services;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using PBIPortal.Core.ManageUser;
using PBIPortal.Core.EFDbContext;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogService
    {
        public List<PBI_Catalog> ALLCatalogs = new List<PBI_Catalog>();
        /// <summary>
        /// 获取所有权限目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PBI_Catalog> GetPBI_Catalogs(List<int> id)
        {

            string msg = string.Empty; 
            List<PBI_Catalog> pBI_Catalogs = new List<PBI_Catalog>();

        

            foreach (int i in id)
            {
                List<PBI_CustormCatalogItems> T = repository.Find<PBI_CustormCatalogItems>(x => x.id == i);
             
                if (T.Count > 0)
                { 
                    GetPBI_Catalog((int)T[0].ParentCustormCatalogId);
                    pBI_Catalogs.AddRange(ALLCatalogs);
                  //  ALLCatalogs.Clear();
                }
            
            }
          //  Logger.Info(LoggerType.Info, pBI_Catalogs.Count.ToString(), pBI_Catalogs.Count.ToString(), msg); ;
            return pBI_Catalogs;
          


        }
        /// <summary>
        /// 回归获取目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void GetPBI_Catalog(int id)
        {
           
            List<PBI_Catalog> tmp= repository.Find(x => x.CatalogId == id);
            if (tmp.Count > 0)
            {
                bool t = false;
                foreach (PBI_Catalog pBI_Catalog in ALLCatalogs)
                {
                    if (pBI_Catalog.CatalogId == id)
                    {
                        t = true;
                    }
                
                }
                 
                if(!t)
                {
                    ALLCatalogs.AddRange(tmp);
                }

                    

                    if (tmp[0].ParentCatalogId != 0)
                    {
                        GetPBI_Catalog((int)tmp[0].ParentCatalogId);

                    }
              }
            

        }


            /// <summary>
            /// 获取有权限的部门
            /// </summary>
            /// <returns></returns>
        public Task<WebResponseContent> GetALLCatalogbyUser()
        {

           
            string msg = string.Empty;
            string username = UserContext.Current.UserName;
            List<PBI_Catalog> pBI_Catalogs = new List<PBI_Catalog>();
            List<V_GetALLUserRightbyRptid> v_GetALLUserRightbyRptids = new List<V_GetALLUserRightbyRptid>();

            WebResponseContent responseContent = new WebResponseContent();
            try
            {

             
                //   var result = repository.FindAsIQueryable(x => x.Group_PID == pid);
                if (username.ToLower() == "admin" || username.ToLower() == "pbiadmin")
                {
                    var result = repository.Find(x => true).OrderByDescending(x => x.Sort);
                    return Task.FromResult(responseContent.OK(null, result));
                }
                else
                {
                    v_GetALLUserRightbyRptids = repository.Find<V_GetALLUserRightbyRptid>(x => x.USER_Login_Name == username);
                }

                List<int> tempint = new List<int>();
                foreach (V_GetALLUserRightbyRptid  tmprpt in v_GetALLUserRightbyRptids)
                {
                    bool t = false;

                  

                    foreach (int i in tempint)
                    {
                        if (i == (int)tmprpt.PBI_ID)
                        {
                            t = true;
                        }
                    }
                    if (!t)
                    {
                        tempint.Add((int)tmprpt.PBI_ID);
                    }

                }


                ///获取目录权限(获取直接指定权限的目录) 报表
                string sqlforgroupuser = $"select b.* from [dbo].[PBI_Catalog_Right_Group] a,  [dbo].[PBI_CustormCatalogItems] b ,[PBI_Group_User] c where a.CatalogId=b.ParentCustormCatalogId and a.Group_Id = c.Group_ID and c.user_login_name='{username}'   order by Sort desc";
                var  pBI_CustormCatalogItems = repository.FromSql<PBI_CustormCatalogItems>(sqlforgroupuser);

                string sqlforuser = $"select b.* from [dbo].[PBI_Catalog_Right_User] a,  [dbo].[PBI_CustormCatalogItems] b  where a.CatalogId=b.ParentCustormCatalogId and a.user_login_name='{username}'   order by Sort desc";
                pBI_CustormCatalogItems.AddRange(repository.FromSql<PBI_CustormCatalogItems>(sqlforuser));

                foreach (PBI_CustormCatalogItems obj in pBI_CustormCatalogItems)
                {

                    bool t = false;
                    foreach (int i in tempint)
                    {
                        if (i == (int)obj.id)
                        {
                            t = true;
                        }
                    }
                    if (!t)
                    {
                        tempint.Add((int)obj.id);
                    }

                }



                //获取按照报表获取目录
                pBI_Catalogs = GetPBI_Catalogs(tempint);


                string sqlstruser = $"select b.* from [PBI_Catalog_Right_User] a, [dbo].[PBI_Catalog] b where a.CatalogId=b.CatalogId and a.USER_LOGIN_NAME='{username.ToLower()}'   order by Sort desc";

                ///获取目录权限(获取直接指定权限的目录)  
                ///
                var userCatalogs = repository.FromSql(sqlstruser);

                string sqlstrgroup = $"select b.* from[PBI_Catalog_Right_Group] a, [dbo].[PBI_Catalog] b,[PBI_Group_User] c where c.Group_ID=a.Group_Id and  a.CatalogId=b.CatalogId and c.USER_LOGIN_NAME='{username.ToLower()}'   order by Sort desc";

                var userGroupCatalogs = repository.FromSql(sqlstrgroup);


                pBI_Catalogs.AddRange(userCatalogs);
                pBI_Catalogs.AddRange(userGroupCatalogs);

                // responseContent.Data = pBI_Catalogs;

                return Task.FromResult(responseContent.OK(null, pBI_Catalogs));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, username, pBI_Catalogs.Count.ToString(), msg);
            }

        }

            /// <summary>
            /// 添加目录
            /// </summary>
            /// <param name="pBIItem"></param>
            /// <returns></returns>
        public Task<WebResponseContent> AddCustormCatalog(PBI_Catalog pBIItem)
        {

            string msg = string.Empty;

            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                if (repository.Exists(x => x.Name == pBIItem.Name && x.ParentCatalogId == pBIItem.ParentCatalogId))
                {
                    return Task.FromResult(responseContent.Error("目录名称不能重复！"));
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
                Logger.Info(LoggerType.Info, pBIItem.Name, pBIItem.Description, msg);
            }

        }
        //删除目录
        public override WebResponseContent Del(object[] keys, bool delList = true)
        {

            string msg = string.Empty;
            bool delit = false;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                foreach (object key in keys)
                {
                    int pid = Convert.ToInt32(key);
                    if (repository.Exists(x => x.ParentCatalogId == pid))
                    {
                        delit = true;
                        return responseContent.Error("目录不为空不能删除！");
                    }
                    else
                    {
                       
                        if (repository.Exists(x => x.CatalogId == pid))
                        {
                            repository.Delete(repository.FindFirst(x => x.CatalogId == pid), true);
                            Logger.Info(LoggerType.Info, key.ToString(), "删除报表目录", "删除成功");
                            return responseContent.OK("删除报表目录成功",null);
                        }
                     
                        return  responseContent.OK("删除报表目录成功", null);
                    }
                                       
                }
                if (delit)
                {
                    return responseContent.Error("目录不为空不能删除！");
                }
                else
                {
                    return responseContent.OK("删除报表目录成功", null);
                }
              

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return responseContent.Error(ResponseType.ServerError);
            }
            finally
            {
                Logger.Info(LoggerType.Info, keys.ToString(), responseContent.Message, msg);
              
            }


        }

        /// <summary>
        /// 获取子目录
        /// </summary>
        /// <param name="pBIItem"></param>
        /// <returns></returns>
        public Task<WebResponseContent> GetPBICatalog(int pBIItem)
        {

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {                
                var result = repository.FindAsIQueryable(x => x.ParentCatalogId == pBIItem).OrderByDescending(x=>x.Sort);
                return Task.FromResult(responseContent.OK(null, result));
                 

 
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info,pBIItem.ToString(), responseContent.Message, msg);
            }

        }

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="pBIItem"></param>
        /// <returns></returns>
        public Task<WebResponseContent> UpdatePBICatalog(PBI_Catalog pBIItem)
        {

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                if (repository.Exists(x => x.Name == pBIItem.Name && x.ParentCatalogId == pBIItem.ParentCatalogId&&x.CatalogId!=pBIItem.CatalogId))
                {
                    return Task.FromResult(responseContent.Error("目录名称不能重复！"));
                }

               var result = repository.Update(pBIItem, true);
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
    }

}

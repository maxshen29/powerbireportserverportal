/*
 *所有关于V_pbi_all_user_roleright类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*V_pbi_all_user_rolerightService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Core.Services;
using PBIPortal.Core.Enums;
using System;
using PBIPortal.Entity.DomainModels.PBI;
using PBIPortal.Core.ManageUser;
using Newtonsoft.Json;

namespace PBI.APP.Services
{
    public partial class V_pbi_all_user_rolerightService
    {


        public Task<WebResponseContent> SetReportRolePBI(PBIItem pBIItem)
        {
            string msg = "";


            WebResponseContent responseContent = new WebResponseContent();

            // Logger.Info(LoggerType.Info, pBIItem.ToString(), responseContent.Message, msg);
            try
            {
                List<string> allReportsPath = new List<string>();
                string putdata = "";

               // var tUserReports = repository.Find(x => true).GroupBy(x => x.PBI_Path).Select(t => new { PBI_Path = t.Key }).ToList();

             //   tUserReports = tUserReports.Distinct().ToList();

               
                    // List<PBI_CatalogItems_Role> tempRoles = repository.Find<PBI_CatalogItems_Role>(x => x.PBI_Path == t.PBI_Path);
                    var tempusers = repository.Find(x => x.PBI_Path == pBIItem.Path).GroupBy(x => x.User_LoginName).Select(t => new { User_LoginName = t.Key });


                    PBI_CatalogItems_Role_Group pBI_CatalogItems_Role_Group = new PBI_CatalogItems_Role_Group();
                    PBI_Group_User pBI_Group_User = new PBI_Group_User();
                    List<PBIDataModelRole> pBIDataModelRoles = new List<PBIDataModelRole>();
                    List<PBIDataModelRoleAssignments> allPBIDataModelRoleAssignments = new List<PBIDataModelRoleAssignments>();


                    foreach (var tu in tempusers)
                    {
                        var tempRoles = repository.Find(x => x.PBI_Path == pBIItem.Path && x.User_LoginName == tu.User_LoginName);
                        PBIDataModelRoleAssignments pBIDataModelRoleAssignments = new PBIDataModelRoleAssignments();
                        pBIDataModelRoleAssignments.GroupUserName = tu.User_LoginName;
                        List<Guid> tguids = new List<Guid>();
                        foreach (V_pbi_all_user_roleright tr in tempRoles)
                        {
                            Guid tmpguid = new Guid(tr.PBI_RoleID.ToString());
                            if (tguids.IndexOf(tmpguid) == -1)
                            {
                                tguids.Add(tmpguid);
                            }


                        }

                        pBIDataModelRoleAssignments.DataModelRoles = tguids.ToArray();
                        allPBIDataModelRoleAssignments.Add(pBIDataModelRoleAssignments);


                    }




                    string rptpath = pBIItem.Path;
                    if (rptpath.Substring(0, 1) != "/")
                    {
                        rptpath = "/" + rptpath;


                    }

                    string apiPath = "PowerBIReports(path='" + rptpath + "')/";
                    UserInfo userInfo = UserContext.Current.UserInfo;

                    API.GetPutData getPutData = new API.GetPutData();
                    getPutData.Init(apiPath);
                    getPutData.SetUserInfo(userInfo.UserName, userInfo.Token);

                    putdata = JsonConvert.SerializeObject(allPBIDataModelRoleAssignments);

                    msg = msg + "|" + getPutData.PutJson("DataModelRoleAssignments", putdata);

                    return Task.FromResult(responseContent.OK("ok", msg));
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }
            finally
            {

                Logger.Info(LoggerType.Info, "", responseContent.Message, msg);
            }



        }

        /// <summary>
        /// 设置报表权限
        /// </summary>  
        /// <param name="pBIItem"></param>
        /// <returns></returns>
        /// 
        public Task<WebResponseContent> SetRolesALLReportsUserRight()
        {
            string msg = "";          


            WebResponseContent responseContent = new WebResponseContent();

            // Logger.Info(LoggerType.Info, pBIItem.ToString(), responseContent.Message, msg);
            try
            {
                List<string> allReportsPath = new List<string>();
                string putdata = "";

                var tUserReports = repository.Find(x => true).GroupBy(x => x.PBI_Path).Select(t => new { PBI_Path = t.Key }).ToList();             
 
                tUserReports = tUserReports.Distinct().ToList();

                foreach (var t in tUserReports)
                {
                    // List<PBI_CatalogItems_Role> tempRoles = repository.Find<PBI_CatalogItems_Role>(x => x.PBI_Path == t.PBI_Path);
                    var tempusers = repository.Find(x => x.PBI_Path == t.PBI_Path).GroupBy(x => x.User_LoginName).Select(t => new { User_LoginName = t.Key });


                    PBI_CatalogItems_Role_Group pBI_CatalogItems_Role_Group = new PBI_CatalogItems_Role_Group();
                    PBI_Group_User pBI_Group_User = new PBI_Group_User();
                    List<PBIDataModelRole> pBIDataModelRoles = new List<PBIDataModelRole>();
                    List<PBIDataModelRoleAssignments> allPBIDataModelRoleAssignments = new List<PBIDataModelRoleAssignments>();


                    foreach (var tu in tempusers)
                    {
                        var  tempRoles = repository.Find(x => x.PBI_Path == t.PBI_Path && x.User_LoginName == tu.User_LoginName);
                        PBIDataModelRoleAssignments pBIDataModelRoleAssignments = new PBIDataModelRoleAssignments();
                        pBIDataModelRoleAssignments.GroupUserName = tu.User_LoginName;
                        List<Guid> tguids = new List<Guid>();
                        foreach (V_pbi_all_user_roleright tr in tempRoles)
                        {
                            Guid tmpguid = new Guid(tr.PBI_RoleID.ToString());
                            if (tguids.IndexOf(tmpguid) == -1)
                            {
                                tguids.Add(tmpguid);
                            }


                        }

                        pBIDataModelRoleAssignments.DataModelRoles = tguids.ToArray();
                        allPBIDataModelRoleAssignments.Add(pBIDataModelRoleAssignments);
                         

                    }




                    string rptpath = t.PBI_Path;
                    if (rptpath.Substring(0, 1) != "/")
                    {
                        rptpath = "/" + rptpath;


                    }

                    string apiPath = "PowerBIReports(path='" + rptpath + "')/";
                    UserInfo userInfo = UserContext.Current.UserInfo;

                    API.GetPutData getPutData = new API.GetPutData();
                    getPutData.Init(apiPath);
                    getPutData.SetUserInfo(userInfo.UserName, userInfo.Token);

                    putdata = JsonConvert.SerializeObject(allPBIDataModelRoleAssignments);

                    msg =msg+"|"+ getPutData.PutJson("DataModelRoleAssignments", putdata);
                     

                }




                return Task.FromResult(responseContent.OK("ok", msg));
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }
            finally
            {

                Logger.Info(LoggerType.Info, "", responseContent.Message, msg);
            }

        }
    }
}

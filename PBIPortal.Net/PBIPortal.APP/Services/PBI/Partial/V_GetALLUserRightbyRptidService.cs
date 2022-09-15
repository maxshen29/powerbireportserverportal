/*
 *所有关于V_GetALLUserRightbyRptid类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*V_GetALLUserRightbyRptidService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Core.Services;
using System;
using PBIPortal.Core.Enums;
using System.Collections.Generic;
using PBIPortal.Core.ManageUser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Services
{
    public partial class V_GetALLUserRightbyRptidService
    {
        /// <summary>
        /// 获取用户可访问报表权限
        /// </summary>
        /// <returns></returns>
        public Task<WebResponseContent> GeAllRptRight()
        {
            string msg = string.Empty;
            string username = UserContext.Current.UserName;
          
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                object result;
                //   var result = repository.FindAsIQueryable(x => x.Group_PID == pid);
                if (username.ToLower() == "admin" || username.ToLower() == "pbiadmin")
                {
                    result = repository.FindAsIQueryable(x => true);
                }
                else
                {
                    result = repository.FindAsIQueryable(x => x.USER_Login_Name== username.ToLower());
                }


          


                return Task.FromResult(responseContent.OK(null, result));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, username, responseContent.Message, msg);
            }
        }
        /// <summary>
        /// 获取报表rolename
        /// </summary>
        /// <param name="pBIItem"></param>
        /// <returns></returns>
      
        public Task<WebResponseContent> GetReportDataModelRole(PBIItem pBIItem)
        {
            string msg = "";
            string tempstr = "";
            WebResponseContent responseContent = new WebResponseContent();
            List<V_GetALLUserRightbyRptid> v_GetALLUserRightbyRptids = new List<V_GetALLUserRightbyRptid>();
            try
            {
                 
             
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

                 tempstr = getPutData.GetResult("DataModelRoles");
                JObject joClient = (JObject)JsonConvert.DeserializeObject(tempstr);
                responseContent.Data = joClient["value"];

                return Task.FromResult(responseContent.OK("ok", responseContent.Data));
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }
            finally
            {
                Logger.Info(LoggerType.Info, tempstr, responseContent.Message, msg);
            }
         }




        /// <summary>
        /// 设置报表权限
        /// </summary>
        /// <param name="pBIItem"></param>
        /// <returns></returns>
        public Task<WebResponseContent> SetReportRight(int pBIItem)
        {
            string msg = "";
            WebResponseContent responseContent = new WebResponseContent();
            List<V_GetALLUserRightbyRptid>  v_GetALLUserRightbyRptids = new List<V_GetALLUserRightbyRptid>();
            string putdata = "" ;

            // Logger.Info(LoggerType.Info, pBIItem.ToString(), responseContent.Message, msg);
            try
            {
                List<PBIRoles> roles = new List<PBIRoles>();
                PBIRoles Roles = new PBIRoles();
                Roles.Name = "\u6d4f\u89c8\u8005";
                Roles.Description = "\u53ef\u4ee5\u67e5\u770b\u6587\u4ef6\u5939\u3001\u62a5\u8868\u548c\u8ba2\u9605\u62a5\u8868\u3002";
                roles.Add(Roles);

                PBIPolicie pBIPolicie = new PBIPolicie();
                List<Policie> Policies = new List<Policie>();

                v_GetALLUserRightbyRptids = repository.Find(x => x.PBI_ID == pBIItem);


                List<object> list = new List<object>();
                if (v_GetALLUserRightbyRptids.Count == 0)
                {
                    return Task.FromResult(responseContent.Error("报表用户权限为空，请选择用户！"));

                }
                foreach (V_GetALLUserRightbyRptid vuser in v_GetALLUserRightbyRptids)
                {
                    Policie policie = new Policie();
                    policie.Roles = roles;
                    policie.GroupUserName = vuser.USER_Login_Name;
                    Policies.Add(policie); 
                }

           


                pBIPolicie.InheritParentPolicy = false;
                pBIPolicie.Policies = Policies;


                string rptpath = v_GetALLUserRightbyRptids[0].Path;
                if (rptpath.Substring(0, 1) != "/")
                {
                    rptpath = "/" + rptpath;


                }

                string apiPath = "PowerBIReports(path='" + rptpath + "')/";
                UserInfo userInfo = UserContext.Current.UserInfo;

                API.GetPutData getPutData = new API.GetPutData();
                getPutData.Init(apiPath);
                getPutData.SetUserInfo(userInfo.UserName, userInfo.Token);

                putdata = JsonConvert.SerializeObject(pBIPolicie);

                string tempstr = getPutData.PutJson("Policies", putdata);
              //  JObject joClient = (JObject)JsonConvert.DeserializeObject(tempstr);
              //  responseContent.Data = joClient["value"];




                return Task.FromResult(responseContent.OK("ok", tempstr));
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }
            finally
            {

                Logger.Info(LoggerType.Info, putdata, responseContent.Message, msg);
            }

        }





    }
}


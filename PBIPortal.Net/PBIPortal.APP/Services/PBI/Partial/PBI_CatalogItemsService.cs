/*
 *所有关于PBI_CatalogItems类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_CatalogItemsService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;
using System.Linq;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using PBIPortal.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PBIPortal.Core.Enums;
using System;
using System.Threading.Tasks;
using PBIPortal.Core.Services;
using PBIPortal.Core.ManageUser;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Services
{
    public partial class PBI_CatalogItemsService
    {
        public  Task<WebResponseContent> GetPBICatalogItems(PBIItem pBIItem)
        {
         
            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                string apiPath = "Folders(path='" + pBIItem.Path + "')/";
                UserInfo userInfo = UserContext.Current.UserInfo;

                API.GetPutData getPutData = new API.GetPutData();
                getPutData.Init(apiPath);
                getPutData.SetUserInfo(userInfo.UserName,userInfo.Token);

                string tempstr = getPutData.GetResult("CatalogItems");
                JObject joClient = (JObject)JsonConvert.DeserializeObject(tempstr);
                responseContent.Data = joClient["value"];               
             
                

            return  Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));
        }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info, pBIItem.Path, responseContent.Message, msg);
            }
            
        }
    }
    
}

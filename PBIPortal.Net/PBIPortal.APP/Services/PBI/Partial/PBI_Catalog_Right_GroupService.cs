/*
 *所有关于PBI_Catalog_Right_Group类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_Catalog_Right_GroupService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
    public partial class PBI_Catalog_Right_GroupService
    {
        public Task<WebResponseContent> InsertGroupRight(List<PBI_Catalog_Right_Group> listObjects)
        {
            List<PBI_Catalog_Right_Group> templist = new List<PBI_Catalog_Right_Group>();
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                foreach (PBI_Catalog_Right_Group listObject in listObjects)
                {
                    if (!repository.Exists(x => x.CatalogId == listObject.CatalogId && x.Group_Id == listObject.Group_Id))
                    {
                        templist.Add(listObject);
                    }
                }
                repository.AddRange(templist, true);

                return Task.FromResult(responseContent.OK(null, null));
            }
            catch (Exception ex)
            {

                Logger.Info(LoggerType.Info, "", responseContent.Message, ex.Message);
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }

        }
    }
}

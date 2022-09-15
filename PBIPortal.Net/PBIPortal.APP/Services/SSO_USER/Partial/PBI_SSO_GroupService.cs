/*
 *所有关于PBI_SSO_Group类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_SSO_GroupService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Entity.DomainModels.PBI;
using System;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Configuration;

using PBIPortal.Core.ManageUser;
using PBIPortal.Core.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_GroupService
    {

        private static string[] auditFields = new string[] { "auditid", "auditstatus", "auditor", "auditdate", "auditreason" };
        public Task<WebResponseContent> GetPBICatalogItems(PBIItem pBIItem)
        {


            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                int pid = Convert.ToInt32(pBIItem.ID);
                //   var result = repository.FindAsIQueryable(x => x.Group_PID == pid);
                var result = repository.FindAsIQueryable(x => true);


                return Task.FromResult(responseContent.OK(null, result));
               
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


        public Task<WebResponseContent> AddGroup(PBI_SSO_Group pBI_SSO_Group)
        {

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                if (repository.Exists(x => x.Group_Name == pBI_SSO_Group.Group_Name))
                {
                    return Task.FromResult(responseContent.Error("用户组名称重复！"));
                }

                pBI_SSO_Group.Group_PID = 0;
                repository.Add(pBI_SSO_Group, true);

                return Task.FromResult(responseContent.OK(null, null));

            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Info,"", responseContent.Message, msg);
            }

        }


        public override WebResponseContent Add(SaveModel saveModel)
        {
            WebResponseContent responseData = new WebResponseContent();
          
            base.AddOnExecuting = (PBI_SSO_Group group, object obj) =>
            {
                group.Group_Name = group.Group_Name.Trim();
                group.Group_PID = 0;
              
                if (repository.Exists(x => x.Group_Name == group.Group_Name))
                    return responseData.Error("组名重复");             
                //设置默认头像
                return responseData.OK();
            };

            base.AddOnExecuted = (PBI_SSO_Group group, object list) =>
            {
                return responseData.OK("添加组成功");
            };
            return base.Add(saveModel); ;
        }

       

        public override WebResponseContent Import(List<Microsoft.AspNetCore.Http.IFormFile> files)
        {




            WebResponseContent responseData = new WebResponseContent();

            if (files == null || files.Count == 0)
                return new WebResponseContent { Status = true, Message = "请选择上传的文件" };
            Microsoft.AspNetCore.Http.IFormFile formFile = files[0];
            string dicPath = $"Upload/{DateTime.Now.ToString("yyyMMdd")}/{typeof(PBI_SSO_Group).Name}/".MapPath();
            if (!Directory.Exists(dicPath)) Directory.CreateDirectory(dicPath);
            dicPath = $"{dicPath}{Guid.NewGuid().ToString()}_{formFile.FileName}";
            // dicPath = dicPath.Replace(':', '-');
            using (var stream = new FileStream(dicPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            try
            {
                responseData = EPPlusHelper.ReadToDataTable<PBI_SSO_Group>(dicPath, GetIgnoreTemplate());
            }
            catch (Exception ex)
            {
                responseData.Error("未能处理导入的文件,请检查导入的文件是否正确");
                Logger.Error($"表{typeof(PBI_SSO_Group).GetEntityTableCnName()}导入失败{ex.Message + ex.InnerException?.Message}");
            }
            if (!responseData.Status) return responseData;
            List<PBI_SSO_Group> pBI_SSO_Groups = new List<PBI_SSO_Group>();
            bool inputErro = false;
            foreach (PBI_SSO_Group  pBI_SSO_Group in responseData.Data as List<PBI_SSO_Group>)
            {
                if (repository.Exists(x => x.Group_Name == pBI_SSO_Group.Group_Name))
                {
                    //  return responseData.Error("上传失败,同一部门下部门名不能相同！");
                    inputErro = true;
                    Logger.Info(LoggerType.Error, pBI_SSO_Group.Group_Name, pBI_SSO_Group.Group_Name, "用户组名称相同，不能添加。");

                }
                else
                {

                    bool dpuser = false;
                    foreach (PBI_SSO_Group temp in pBI_SSO_Groups)
                    {

                        if (temp.Group_Name == pBI_SSO_Group.Group_Name)
                        {
                            dpuser = true;
                            inputErro = true;
                        }
                    }

                    if (dpuser)
                    {
                        Logger.Info(LoggerType.Error, pBI_SSO_Group.Group_Name, pBI_SSO_Group.Group_Name, "用户组名称重复，只添加一个。");
                    }
                    else
                    {
                        pBI_SSO_Group.Group_PID = 0;
                        pBI_SSO_Groups.Add(pBI_SSO_Group);

                    }



                }

            }

            repository.AddRange(pBI_SSO_Groups, true);

            if (inputErro)
            {
                return new WebResponseContent { Status = true, Message = "文件上传成功，有错误信息，详情请查看日志！！" };
            }
            else
            {
                return new WebResponseContent { Status = true, Message = "文件上传成功！" };
            }
        }

        private List<string> GetIgnoreTemplate()
        {
            //忽略创建人、修改人、审核等字段
            List<string> ignoreTemplate = UserIgnoreFields.ToList();
            ignoreTemplate.AddRange(auditFields.ToList());
            return ignoreTemplate;
        }
        private static string[] _createFields { get; set; }
        private static string[] CreateFields
        {
            get
            {
                if (_createFields != null) return _createFields;
                _createFields = AppSetting.CreateMember.GetType().GetProperties()
                    .Select(x => x.GetValue(AppSetting.CreateMember)?.ToString())
                    .Where(w => !string.IsNullOrEmpty(w)).ToArray();
                return _createFields;
            }
        }

        private static string[] _modifyFields { get; set; }
        private static string[] ModifyFields
        {
            get
            {
                if (_modifyFields != null) return _modifyFields;
                _modifyFields = AppSetting.ModifyMember.GetType().GetProperties()
                    .Select(x => x.GetValue(AppSetting.ModifyMember)?.ToString())
                    .Where(w => !string.IsNullOrEmpty(w)).ToArray();
                return _modifyFields;
            }
        }
        private static string[] _userIgnoreFields { get; set; }

        private static string[] UserIgnoreFields
        {
            get
            {
                if (_userIgnoreFields != null) return _userIgnoreFields;
                List<string> fields = new List<string>();
                fields.AddRange(CreateFields);
                fields.AddRange(ModifyFields);
                // fields.AddRange();
                _userIgnoreFields = fields.ToArray();
                return _userIgnoreFields;
            }
        }
    }
}

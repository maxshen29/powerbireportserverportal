/*
 *所有关于PBI_Group_User类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_Group_UserService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Core.Enums;
using PBIPortal.Core.Services;
using PBIPortal.Entity.DomainModels.SSO_USER;
using System.IO;
using PBIPortal.Core.Configuration;
using PBIPortal.Core.DBManager;

namespace PBI.APP.Services
{
    public partial class PBI_Group_UserService
    {
        private static string[] auditFields = new string[] { "auditid", "auditstatus", "auditor", "auditdate", "auditreason" };
        public Task<WebResponseContent> InsertUser(List<PBI_Group_User> listObject)
        {
            List<PBI_Group_User> templist = new List<PBI_Group_User>();
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                foreach (PBI_Group_User pBI_Group_User in listObject)
                {
                    if (!repository.Exists(x => x.USER_Login_Name == pBI_Group_User.USER_Login_Name&&x.Group_ID==pBI_Group_User.Group_ID))
                    {
                        templist.Add(pBI_Group_User);                       
                    }
                }
                repository.AddRange(templist, true);

                return Task.FromResult(responseContent.OK(null,null));
            }
            catch (Exception ex)
            {
                
                Logger.Info(LoggerType.Info,listObject.ToString(), responseContent.Message, ex.Message);
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }

        }


        public override  WebResponseContent Import(List<Microsoft.AspNetCore.Http.IFormFile> files)
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
                responseData = EPPlusHelper.ReadToDataTable<PBI_Group_User>(dicPath, GetIgnoreTemplate());
            }
            catch (Exception ex)
            {
                responseData.Error("未能处理导入的文件,请检查导入的文件是否正确");
                Logger.Error($"表{typeof(PBI_Group_User).GetEntityTableCnName()}导入失败{ex.Message + ex.InnerException?.Message}");
            }
            if (!responseData.Status) return responseData;
            List<PBI_Group_User> pBI_Group_Users = new List<PBI_Group_User>();

            bool inputErro = false;
            foreach (PBI_Group_User pBI_Group_User in responseData.Data as List<PBI_Group_User>)
            {
                if (repository.Exists(x => x.Group_Name == pBI_Group_User.Group_Name&&x.USER_Login_Name==pBI_Group_User.USER_Login_Name))
                {
                    //  return responseData.Error("上传失败,同一部门下部门名不能相同！");
                    inputErro = true;
                    Logger.Info(LoggerType.Error, pBI_Group_User.Group_Name, pBI_Group_User.USER_Login_Name, "同一个用户组重复导入同，不能添加。");

                }
                else
                {

                    bool dpuser = false;
                    foreach (PBI_Group_User temp in pBI_Group_Users)
                    {

                        if (temp.Group_Name == pBI_Group_User.Group_Name&&temp.USER_Login_Name==pBI_Group_User.USER_Login_Name&&temp.USER_TrueName==pBI_Group_User.USER_TrueName)
                        {
                            dpuser = true;
                            inputErro = true;
                        }
                    }

                    if (dpuser)
                    {
                        Logger.Info(LoggerType.Error, pBI_Group_User.Group_Name, pBI_Group_User.USER_TrueName, "信息重复，只添加一个。");
                    }
                    else
                    {



                        int tempgoroupid = GetGroupID(pBI_Group_User.Group_Name.Trim());
                        int tempuserid = GetUserID(pBI_Group_User.USER_Login_Name.Trim());

                        if (tempgoroupid == -1|| tempuserid == -1)
                        {
                            Logger.Info(LoggerType.Error, pBI_Group_User.Group_Name, pBI_Group_User.USER_Login_Name, "找不到相应的组或者用用户名，请查日志，只添加一个。");
                        }
                        else
                        {

                            pBI_Group_User.Group_ID = tempgoroupid;
                            pBI_Group_User.USER_ID = tempuserid;
                            pBI_Group_Users.Add(pBI_Group_User);

                        }


                    }



                }

            }

            repository.AddRange(pBI_Group_Users, true);

            if (inputErro)
            {
                return new WebResponseContent { Status = true, Message = "文件上传成功，有错误信息，详情请查看日志！！" };
            }
            else
            {
                return new WebResponseContent { Status = true, Message = "文件上传成功！" };
            }
        }

        public  int GetGroupID(string Group_Name)
        {
            int result = -1;
             List<PBI_SSO_Group> pBI_SSO_Groups=  repository.Find<PBI_SSO_Group>(x => x.Group_Name == Group_Name);
            Logger.Info(LoggerType.Error, Group_Name, pBI_SSO_Groups.Count.ToString(), "");
            if (pBI_SSO_Groups.Count > 0)
            {
                result= pBI_SSO_Groups[0].Group_Id;
            }
            return result;
        
        }


        public int GetUserID(string User_Login_Name)
        {
            int result = -1;
            List<PBI_SSO_USER> pBI_SSO_users = repository.Find<PBI_SSO_USER>(x => x.USER_LOGIN_NAME == User_Login_Name);
            Logger.Info(LoggerType.Error, User_Login_Name, pBI_SSO_users.Count.ToString(), "");
            if (pBI_SSO_users.Count > 0)
            {
                result = pBI_SSO_users[0].USER_ID;
            }
            return result;

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

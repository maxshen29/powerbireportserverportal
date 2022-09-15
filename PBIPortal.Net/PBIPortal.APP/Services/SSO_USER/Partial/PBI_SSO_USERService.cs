/*
 *所有关于PBI_SSO_USER类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_SSO_USERService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using PBIPortal.Entity.DomainModels.SSO_USER;
using PBIPortal.Core.Enums;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using PBIPortal.Core.Services;
using System.IO;
using PBIPortal.Core.Configuration;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_USERService
    {
        private static string[] auditFields = new string[] { "auditid", "auditstatus", "auditor", "auditdate", "auditreason" };
        public Task<WebResponseContent> GetALLUser()
        {

            WebResponseContent responseContent = new WebResponseContent();
            try
            {

                // var result = repository.FindAsIQueryable(x => 1 == 1).Select(p => new Tree { id = p.DEPT_ID, label = p.DEPT_NAME, name = p.DEPT_NAME, pid = (int)p.PDEPT_ID });
                var result = repository.FindAsIQueryable(x => true).Select(p => new Tree { id = p.USER_ID.ToString(), label = p.USER_LOGIN_NAME, name = p.USER_LOGIN_NAME, pid = p.ODEPT_CODE, icon = "ios-man" });

                return Task.FromResult(responseContent.OK(null, result));
            }
            catch (Exception ex)
            {
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }

        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <returns></returns>
        public Task<WebResponseContent> ResetUserPwd(ResetPwd resetPwd)
        {

            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                var T = repository.FindFirst(x => x.USER_LOGIN_NAME == resetPwd.USER_LOGIN_NAME.Trim());
                if (repository.Exists<Sys_User>(x => x.UserName == resetPwd.USER_LOGIN_NAME.Trim()))
                {

                    var t = repository.Find<Sys_User>(x => x.UserName == resetPwd.USER_LOGIN_NAME);
                    t[0].UserPwd = resetPwd.UserPwd.EncryptDES(AppSetting.Secret.User);
                    repository.Update<Sys_User>(t[0], true);

                }
                else
                {
                    List<Sys_User> sys_Users = new List<Sys_User>(); //添加SYS_users
                    var t = repository.Find<Sys_User>(x => true).OrderByDescending(x => x.User_Id).Take(1);
                    int tid = t.First(x => true).User_Id;

                    tid = tid + 1;
                    Sys_User user = new Sys_User();
                    user.UserName = T.USER_LOGIN_NAME;
                    user.Role_Id = 3;
                    user.RoleName = "普通用户";
                    user.CreateDate = DateTime.Now;
                    user.HeadImageUrl = "";
                    user.UserTrueName = T.USER_TrueName;
                    user.UserPwd = resetPwd.UserPwd.EncryptDES(AppSetting.Secret.User);
                    user.Enable = 1;
                    string token = JwtHelper.IssueJwt(new UserInfo()
                    {
                        User_Id = tid,
                        UserName = user.UserName,
                        Role_Id = user.Role_Id,
                        UserTrueName = user.UserTrueName

                    });
                    user.Token = token;
                    sys_Users.Add(user);


                    repository.AddRange<Sys_User>(sys_Users, true);

                }
                 

                return Task.FromResult(responseContent.OK("重置成功", "重置成功"));
            }
            catch (Exception ex)
            {
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError + ex.Message));
            }

        }


        public override WebResponseContent Add(SaveModel saveModel)
        {
            WebResponseContent responseData = new WebResponseContent();
            base.AddOnExecute = (SaveModel userModel) =>
            {
                string USER_LOGIN_NAME = userModel.MainData["USER_LOGIN_NAME"].ToString();
                if (repository.Exists<Sys_User>(x=>x.UserName==USER_LOGIN_NAME))
                { 
                    return responseData.Error("用户名重复不能创建！");
                
                }
                if (repository.Exists(x => x.USER_LOGIN_NAME == USER_LOGIN_NAME))
                {
                    return responseData.Error("用户名重复不能创建！");

                }
                return responseData.OK();
            };


            ///生成8位数密码

            string pwd = "";
                 //在AddOnExecuting之前已经对提交的数据做过验证是否为空
                 base.AddOnExecuting = (PBI_SSO_USER user, object obj) =>
            {
                user.USER_LOGIN_NAME = user.USER_LOGIN_NAME.Trim();
                if (repository.Exists(x => x.USER_LOGIN_NAME == user.USER_LOGIN_NAME))
                    return responseData.Error("用户名已经被注册");

                ///添加user表
                 List<Sys_User> sys_Users = new List<Sys_User>(); //添加SYS_users
                var t = repository.Find<Sys_User>(x => true).OrderByDescending(x => x.User_Id).Take(1);
                int tid = t.First(x => true).User_Id;
                Sys_User sys_user = new Sys_User();
                 pwd = user.USER_IDCODE.Substring(10, 8);
                if (!repository.Exists<Sys_User>(x => x.UserName == user.USER_LOGIN_NAME))
                {
                    tid = tid + 1;
                    sys_user.UserName = user.USER_LOGIN_NAME;
                    sys_user.Role_Id = 3;
                    sys_user.RoleName = "普通用户";
                    sys_user.CreateDate = DateTime.Now;
                    sys_user.HeadImageUrl = "";
                    sys_user.UserTrueName = user.USER_TrueName;
                    sys_user.UserPwd = user.USER_IDCODE.Substring(10, 8).EncryptDES(AppSetting.Secret.User);
                    
                    sys_user.Enable = 1;
                    string token = JwtHelper.IssueJwt(new UserInfo()
                    {
                        User_Id = tid,
                        UserName = sys_user.UserName,
                        Role_Id = sys_user.Role_Id,
                        UserTrueName = sys_user.UserTrueName

                    });
                    sys_user.Token = token;
                    sys_Users.Add(sys_user);
                    repository.AddRange<Sys_User>(sys_Users, true);
                   

                }





                //设置默认头像
                return responseData.OK();
            };

            base.AddOnExecuted = (PBI_SSO_USER user, object list) =>
            {
                return responseData.OK($"用户新建成功.帐号{user.USER_LOGIN_NAME}密码{pwd}");
            };
            return base.Add(saveModel); ;


        }



        public override WebResponseContent Import(List<Microsoft.AspNetCore.Http.IFormFile> files)
        {


            

            WebResponseContent responseData = new WebResponseContent();

            if (files == null || files.Count == 0)
                return new WebResponseContent { Status = true, Message = "请选择上传的文件" };
            Microsoft.AspNetCore.Http.IFormFile formFile = files[0];
            string dicPath = $"Upload/{DateTime.Now.ToString("yyyMMdd")}/{typeof(PBI_SSO_USER).Name}/".MapPath();
            if (!Directory.Exists(dicPath)) Directory.CreateDirectory(dicPath);
            dicPath = $"{dicPath}{Guid.NewGuid().ToString()}_{formFile.FileName}";
            // dicPath = dicPath.Replace(':', '-');
            using (var stream = new FileStream(dicPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            try
            {
                responseData = EPPlusHelper.ReadToDataTable<PBI_SSO_USER>(dicPath, GetIgnoreTemplate());
            }
            catch (Exception ex)
            {
                responseData.Error("未能处理导入的文件,请检查导入的文件是否正确");
                Logger.Error($"表{typeof(PBI_SSO_USER).GetEntityTableCnName()}导入失败{ex.Message + ex.InnerException?.Message}");
            }
            if (!responseData.Status) return responseData;
            List<PBI_SSO_USER> pBI_SSO_USERs = new List<PBI_SSO_USER>();
           

            bool inputErro = false;      
 
            foreach (PBI_SSO_USER  pBI_SSO_USER in responseData.Data as List<PBI_SSO_USER>)
            {
                 

                if (repository.Exists(x => x.USER_LOGIN_NAME == pBI_SSO_USER.USER_LOGIN_NAME))
                {
                    inputErro = true;
                    Logger.Info(LoggerType.Error, pBI_SSO_USER.USER_LOGIN_NAME, pBI_SSO_USER.USER_TrueName, "系统已经有此登录，不能添加。");
                }
                else           
                {
                    bool dpuser = false;
                    foreach (PBI_SSO_USER temp_user in pBI_SSO_USERs)
                    {

                        if (temp_user.USER_LOGIN_NAME == pBI_SSO_USER.USER_LOGIN_NAME)
                        {
                            dpuser = true;
                            inputErro = true;
                        }
                    }

                    if (dpuser)
                    {
                        Logger.Info(LoggerType.Error, pBI_SSO_USER.USER_LOGIN_NAME, pBI_SSO_USER.USER_TrueName, "登录名重复，只添加一个。");
                    }
                    else
                    {
                        pBI_SSO_USERs.Add(pBI_SSO_USER);
                    }

                }


                //  return responseData.Error("上传失败,同一部门下部门名不能相同！"); 
            }
            repository.AddRange(pBI_SSO_USERs, true);
            List<Sys_User> sys_Users = new List<Sys_User>(); //添加SYS_users
            var t = repository.Find<Sys_User>(x => true).OrderByDescending(x => x.User_Id).Take(1);
            int tid = t.First(x => true).User_Id;


            foreach (PBI_SSO_USER T in pBI_SSO_USERs)
            {
               

                if (!repository.Exists<Sys_User>(x => x.UserName == T.USER_LOGIN_NAME))
                {
                    tid = tid + 1;
                    Sys_User user = new Sys_User();
                    user.UserName =T.USER_LOGIN_NAME;
                    user.Role_Id = 3;
                    user.RoleName = "普通用户";
                    user.CreateDate = DateTime.Now;
                    user.HeadImageUrl = "";
                    user.UserTrueName = T.USER_TrueName;
                    user.UserPwd = T.USER_IDCODE.Substring(10, 8).EncryptDES(AppSetting.Secret.User);
                    user.Enable = 1;
                    string token = JwtHelper.IssueJwt(new UserInfo()
                    {
                        User_Id = tid,
                        UserName = user.UserName,
                        Role_Id = user.Role_Id,
                        UserTrueName = user.UserTrueName

                    });
                    user.Token = token;
                    sys_Users.Add(user);               
                     
                }
             
            }

            repository.AddRange<Sys_User>(sys_Users, true);

            if (inputErro)
            {

                return new WebResponseContent { Status = true, Message = "文件上传成功，有重复登录名，请查看日志。" };
            }
            else
            {

                return new WebResponseContent { Status = true, Message = "文件上传成功" };

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

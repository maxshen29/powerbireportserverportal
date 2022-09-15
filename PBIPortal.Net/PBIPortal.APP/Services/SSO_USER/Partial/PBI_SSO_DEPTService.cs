/*
 *所有关于PBI_SSO_DEPT类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*PBI_SSO_DEPTService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System;
using PBIPortal.Entity.DomainModels.SSO_USER;
using PBIPortal.Entity.DomainModels.PBI;
using PBIPortal.Core.Services;
using System.Collections.Generic;
using System.IO;
using PBIPortal.Core.Configuration;

namespace PBI.APP.Services
{
    public partial class PBI_SSO_DEPTService
    {
        private static string[] auditFields = new string[] { "auditid", "auditstatus", "auditor", "auditdate", "auditreason" };

        public Task<WebResponseContent> GetPBICatalogItems(PBIItem pBIItem)
        {

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                string pid = pBIItem.ID;
                var result = repository.FindAsIQueryable(x => x.DEPT_PCODE == pid);
               // var result = repository.FindAsIQueryable(x => true);



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

        public  Task<WebResponseContent> GetALLDept()
        {
          
            WebResponseContent responseContent = new WebResponseContent();
            try
             {

                // var result = repository.FindAsIQueryable(x => 1 == 1).Select(p => new Tree { id = p.DEPT_ID, label = p.DEPT_NAME, name = p.DEPT_NAME, pid = (int)p.PDEPT_ID }); 
                var result = repository.FindAsIQueryable(x => 1 == 1).Select(p => new Tree { id = p.DEPT_CODE, label = p.DEPT_NAME, name = p.DEPT_NAME, pid = p.DEPT_PCODE.ToString(),icon= "ios-folder-outline" });

                return  Task.FromResult(responseContent.OK(null, result));
            }
            catch (Exception ex)
            {
                //string msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError+ex.Message));
            }

        }



        public override WebResponseContent Import(List<Microsoft.AspNetCore.Http.IFormFile> files)
        {




            WebResponseContent responseData = new WebResponseContent();

            if (files == null || files.Count == 0)
                return new WebResponseContent { Status = true, Message = "请选择上传的文件" };
            Microsoft.AspNetCore.Http.IFormFile formFile = files[0];
            string dicPath = $"Upload/{DateTime.Now.ToString("yyyMMdd")}/{typeof(PBI_SSO_DEPT).Name}/".MapPath();
            if (!Directory.Exists(dicPath)) Directory.CreateDirectory(dicPath);
            dicPath = $"{dicPath}{Guid.NewGuid().ToString()}_{formFile.FileName}";
            // dicPath = dicPath.Replace(':', '-');
            using (var stream = new FileStream(dicPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            try
            {
                responseData = EPPlusHelper.ReadToDataTable<PBI_SSO_DEPT>(dicPath, GetIgnoreTemplate());
            }
            catch (Exception ex)
            {
                responseData.Error("未能处理导入的文件,请检查导入的文件是否正确");
                Logger.Error($"表{typeof(PBI_SSO_DEPT).GetEntityTableCnName()}导入失败{ex.Message + ex.InnerException?.Message}");
            }
            if (!responseData.Status) return responseData;
            List<PBI_SSO_DEPT> pBI_SSO_DEPTs = new List<PBI_SSO_DEPT>();
            bool inputErro = false;
            foreach (PBI_SSO_DEPT pBI_SSO_DEPT in responseData.Data as List<PBI_SSO_DEPT>)
            {
                if (repository.Exists(x => x.DEPT_NAME == pBI_SSO_DEPT.DEPT_NAME && x.DEPT_PCODE == pBI_SSO_DEPT.DEPT_PCODE))
                {
                    //  return responseData.Error("上传失败,同一部门下部门名不能相同！");
                    inputErro = true;
                    Logger.Info(LoggerType.Error,pBI_SSO_DEPT.DEPT_NAME, pBI_SSO_DEPT.DEPT_NAME,"部门名称相同，不能添加。");

                }
                else               
                {

                    bool dpuser = false;
                    foreach (PBI_SSO_DEPT temp_user in pBI_SSO_DEPTs)
                    {

                        if (temp_user.DEPT_NAME == pBI_SSO_DEPT.DEPT_NAME)
                        {
                            dpuser = true;
                            inputErro = true;
                        }
                    }

                    if (dpuser)
                    {
                        Logger.Info(LoggerType.Error, pBI_SSO_DEPT.DEPT_NAME, pBI_SSO_DEPT.DEPT_NAME, "部门名称重复，只添加一个。");
                    }
                    else
                    {
                        pBI_SSO_DEPTs.Add(pBI_SSO_DEPT);
                     
                    }


                    
                }

            }

            repository.AddRange(pBI_SSO_DEPTs, true);

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


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Core.DBManager;
using PBIPortal.Core.EFDbContext;
using PBIPortal.Core.Filters;
using PBIPortal.Core.Infrastructure;
using PBIPortal.Core.ObjectActionValidator;
using PBIPortal.Entity.AttributeManager;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;
using PBIPortal.Entity.DomainModels.System;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    [Route("api/User")]
    public partial class Sys_UserController
    {
        [HttpPost, HttpGet, Route("login"), AllowAnonymous]
        [ObjectModelValidatorFilter(ValidatorModel.Login)]
        public async Task<IActionResult> Login([FromBody]LoginInfo loginInfo)
        {
            return Json(await Service.Login(loginInfo));
        }

        [HttpPost, Route("replaceToken"), AllowAnonymous]
        public async Task<IActionResult> ReplaceToken()
        {
            return Json(await Service.ReplaceToken());
        }

        [HttpPost, Route("modifyPwd")]
        [ApiActionPermission]
        //通过ObjectGeneralValidatorFilter校验参数，不再需要if esle判断OldPwd与NewPwd参数
        [ObjectGeneralValidatorFilter(ValidatorGeneral.OldPwd, ValidatorGeneral.NewPwd)]
        public async Task<IActionResult> ModifyPwd(string oldPwd, string newPwd)
        {
            return Json(await Service.ModifyPwd(oldPwd, newPwd));
        }

        [HttpPost, Route("getCurrentUserInfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            return Json(await Service.GetCurrentUserInfo());
        }


        [HttpPost, Route("PBILogin"), AllowAnonymous]
        public async Task<IActionResult> PBILogin([FromBody]PostData postData)
        {
            return Json(await Service.PBILogin(postData));
        }


        [HttpGet, HttpPost, Route("SSOLogin"), AllowAnonymous]
        public async Task<IActionResult> SSOLogin(string sign)
        {
            return Json(await Service.SSOLogin(sign));
        }

        [HttpGet, HttpPost, Route("UserCodeLogin"), AllowAnonymous]
        public async Task<IActionResult> UserCodeLogin(string clientId, string usercode, string src)
        {
            return Json(await Service.UserCodeLogin(clientId, usercode, src));
        }

        [ HttpPost, Route("ModifyUserInfo")]
        public async Task<IActionResult> ModifyUserInfo([FromBody]PBIuserinfo sys_User)
        {
            return Json(await Service.ModifyUserInfo(sys_User));
        }


        [HttpGet, HttpPost, Route("PortalLogin"), AllowAnonymous]
        public async Task<IActionResult> PortalLogin(string token, string src)
        {
            return Json(await Service.PortalLogin(token, src));
        }



        


    }
}

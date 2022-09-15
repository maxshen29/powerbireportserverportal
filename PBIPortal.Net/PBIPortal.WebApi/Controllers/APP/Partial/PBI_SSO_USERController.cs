/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_SSO_USER",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Controllers
{
    public partial class PBI_SSO_USERController
    {
        [HttpPost, Route("GetALLUser")]
        public async Task<IActionResult> GetALLUser()
        {
            return Json(await Service.GetALLUser()); 
        }


        [HttpPost, Route("ResetUserPwd")]
        public async Task<IActionResult> ResetUserPwd([FromBody]ResetPwd resetPwd)
        {
            return Json(await Service.ResetUserPwd(resetPwd));
        }

         

    }
}

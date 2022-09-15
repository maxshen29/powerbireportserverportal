/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_SSO_ALL",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Core.Utilities;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Controllers
{
    public partial class PBI_SSO_ALLController
    {
        [HttpPost, Route("GetALL")]
        public async Task<IActionResult> GetALL()
        {

            return Json(await Service.GetALL());
        }
    }
}

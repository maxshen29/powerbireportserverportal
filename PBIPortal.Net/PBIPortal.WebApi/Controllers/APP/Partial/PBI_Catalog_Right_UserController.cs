/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_Catalog_Right_User",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Controllers
{
    public partial class PBI_Catalog_Right_UserController
    {

        [HttpPost, Route("InsertUserRight")]
        public async Task<IActionResult> InsertUserRight([FromBody] List<PBI_Catalog_Right_User> listObjects)
        {
            return Json(await Service.InsertUserRight(listObjects));
        }
    }
}

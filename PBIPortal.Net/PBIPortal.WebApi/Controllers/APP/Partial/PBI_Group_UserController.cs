/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_Group_User",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Controllers
{
    public partial class PBI_Group_UserController
    {

        [HttpPost, Route("InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody]List<PBI_Group_User> listObject)
        {
          
            return Json(await Service.InsertUser(listObject));
        }
    }
}

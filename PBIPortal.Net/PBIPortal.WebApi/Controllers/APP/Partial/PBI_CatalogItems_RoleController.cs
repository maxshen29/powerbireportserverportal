/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_CatalogItems_Role",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Controllers
{
    public partial class PBI_CatalogItems_RoleController
    {

        [HttpPost, Route("InsertRoleUserRight")]
        public async Task<IActionResult> InsertRoleUserRight([FromBody]List<PBI_CatalogItems_Role> listObject)
        {
            return Json(await Service.InsertRoleUserRight(listObject));
        }
      
    }
}

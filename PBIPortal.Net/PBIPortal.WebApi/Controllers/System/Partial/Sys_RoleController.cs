using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.AttributeManager;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.IServices;
using PBIPortal.System.Services;

namespace PBIPortal.System.Controllers
{
    [Route("api/role")]
    public partial class Sys_RoleController   
    {
        [HttpPost, Route("getCurrentTreePermission")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetCurrentTreePermission()
        {
            return Json(await Service.GetCurrentTreePermission());
        }

        [HttpPost, Route("getUserTreePermission")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetUserTreePermission(int roleId)
        {
            return Json(await Service.GetUserTreePermission(roleId));
        }

        [HttpPost, Route("savePermission")]
        [ApiActionPermission(ActionPermissionOptions.Update)]
        public async Task<IActionResult> SavePermission([FromBody] List<UserPermissions> userPermissions, int roleId)
        {
            return Json(await Service.SavePermission(userPermissions, roleId));
        }
    }
}



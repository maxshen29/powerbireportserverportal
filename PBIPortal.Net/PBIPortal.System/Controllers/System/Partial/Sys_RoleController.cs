using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Extensions;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.Services;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_RoleController
    {
        [ActionPermission("Sys_Role", ActionPermissionOptions.Search)]

        public async Task<IActionResult> TreeManager()
        {
            ViewData["TreeList"] = (await Service.GetCurrentTreePermission()).Serialize();

            //ViewData["TreeList"] = new

            //{
            //    RoleData = Service.GetRoleTreeList(roleId),
            //    MenuData = Service.GetCurrentTreePermission(roleId), //Sys_MenuService.Instance.GetMenu()
            //}.Serialize();
            return View("~/Views/System/System/Sys_RoleTree.cshtml");
        }


        [ActionPermission("Sys_Role", ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetRoleMenuAuth(int role_Id)
        {
            return Json(await Service.GetUserTreePermission(role_Id));
        }
        [ActionPermission("Sys_Role", ActionPermissionOptions.Update)]
        public IActionResult SaveRoleAuth(int role_Id)
        {
            return Json(Service.SavePermission(null, role_Id));
        }
     
    }
}


﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Extensions;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_MenuController
    {
        [ActionPermission("Sys_Menu", ActionPermissionOptions.Search)]
        public override ActionResult Manager()
        {
            ViewData["TreeList"] = Service.GetMenu().Result.Serialize();
            return View("~/Views/System/System/Sys_MenuTree.cshtml");
        }

        [ActionPermission("Sys_Menu", "1", ActionPermissionOptions.Search)]
        public async Task<ActionResult> GetDaetail(int menuId)
        {
            return Json(await Service.GetTreeItem(menuId));
        }
        [ActionPermission("Sys_Menu", ActionPermissionOptions.Add)]
        public async Task<ActionResult> SaveEdit(Sys_Menu menu)
        {
            return Json(await Service.Save(menu));
        }

    }
}

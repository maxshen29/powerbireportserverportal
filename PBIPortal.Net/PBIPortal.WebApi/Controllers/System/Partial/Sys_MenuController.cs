using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_MenuController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("getTreeMenu")]
        //2019.10.24屏蔽用户查询自己权限菜单
       // [ApiActionPermission("Sys_Menu", ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetTreeMenu()
        {
            return Json(await _service.GetCurrentMenuActionList());
        }
        [HttpPost, Route("getMenu")]
        [ApiActionPermission("Sys_Menu", ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetMenu()
        {
            return Json(await _service.GetMenu());
        }

        [HttpPost, Route("getTreeItem")]
        [ApiActionPermission("Sys_Menu", "1", ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetTreeItem(int menuId)
        {
            return Json(await _service.GetTreeItem(menuId));
        }

        //[ActionPermission("Sys_Menu", "1", ActionPermissionOptions.Add)]
        //只有角色ID为1的才能进行保存操作
        [HttpPost, Route("save"), ApiActionPermission(1)]
        public async Task<ActionResult> Save([FromBody]Sys_Menu menu)
        {
            return Json(await _service.Save(menu));
        }
    }
}

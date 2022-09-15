/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("V_group_indexpath",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;

namespace PBI.APP.Controllers
{
    public partial class V_group_indexpathController
    {
        [HttpPost, Route("GeIndexPath")]
        public async Task<IActionResult> GeIndexPath()
        {
            return Json(await Service.GeIndexPath());
        }
       
    }
}

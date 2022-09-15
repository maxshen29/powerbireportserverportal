/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("V_pbi_all_user_roleright",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Controllers
{
    public partial class V_pbi_all_user_rolerightController
    {
        [HttpPost, Route("SetRolesALLReportsUserRight")]
        public async Task<IActionResult> SetRolesALLReportsUserRight()
        {
            return Json(await Service.SetRolesALLReportsUserRight());
        }

        [HttpPost, Route("SetReportRolePBI")]
        public async Task<IActionResult> SetReportRolePBI([FromBody]PBIItem pBIItem)
        {
            return Json(await Service.SetReportRolePBI(pBIItem));
        }
     
    }
}

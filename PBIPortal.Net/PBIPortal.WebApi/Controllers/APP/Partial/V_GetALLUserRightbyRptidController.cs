/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("V_GetALLUserRightbyRptid",Enums.ActionPermissionOptions.Search)]
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
    public partial class V_GetALLUserRightbyRptidController
    {

        [HttpPost, Route("SetReportRight")]
        public async Task<IActionResult> SetReportRight([FromBody]PBIItem pBIItem)
        {

            return Json(await Service.SetReportRight(Convert.ToInt32(pBIItem.ID)));
        }

        [HttpPost, Route("GeAllRptRight")]
        public async Task<IActionResult> GeAllRptRight()
        {
            return Json(await Service.GeAllRptRight());
        }



        [HttpPost, Route("GetReportDataModelRole")]
        public async Task<IActionResult> GetReportDataModelRole([FromBody]PBIItem pBIItem)
        {
            return Json(await Service.GetReportDataModelRole(pBIItem));
        }


       
         


    }
}

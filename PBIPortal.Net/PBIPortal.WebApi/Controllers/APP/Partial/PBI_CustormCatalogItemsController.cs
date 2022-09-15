/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_CustormCatalogItems",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Controllers
{
    public partial class PBI_CustormCatalogItemsController
    {
        [HttpPost, Route("AddCustormReport")]
        public async Task<IActionResult> GetPBICatalogItems([FromBody]PBI_CustormCatalogItems pBIItem)
        {
            return Json(await Service.AddCustormReport(pBIItem));


               

        }


        [HttpPost, Route("GetPBICatalog")]
        public async Task<IActionResult> GetPBICatalog([FromBody]PBIItem pBIItem)
        {
            return Json(await Service.GetPBICatalog(Convert.ToInt16(pBIItem.ID)));
        }

        [HttpPost, Route("UpdateCustormReport")]
        public async Task<IActionResult> UpdateCustormReport([FromBody]PBI_CustormCatalogItems pBIItem)
        {
            return Json(await Service.UpdateCustormReport(pBIItem));
        }

         [HttpPost, Route("GetALLRightCatalogPBI")]
        public async Task<IActionResult> GetALLRightCatalogPBI()
        {
            return Json(await Service.GetALLRightCatalogPBI());
        }
      
    }
}

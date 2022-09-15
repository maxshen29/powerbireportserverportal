/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_Catalog",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Controllers
{
    public partial class PBI_CatalogController
    {

        [HttpPost, Route("GetPBICatalog")]
        public async Task<IActionResult> GetPBICatalog([FromBody]PBIItem pBIItem)
        {
            return Json(await Service.GetPBICatalog(Convert.ToInt16(pBIItem.ID)));
        }

        [HttpPost, Route("AddCustormCatalog")]
        public async Task<IActionResult> GetPBIAddCustormCatalogCatalog([FromBody]PBI_Catalog pBIItem)
        {
            return Json(await Service.AddCustormCatalog(pBIItem));
        }

        [HttpPost, Route("UpdatePBICatalog")]
        public async Task<IActionResult> UpdatePBICatalog([FromBody]PBI_Catalog pBIItem)
        {
            return Json(await Service.UpdatePBICatalog(pBIItem));
        }



        [HttpPost, Route("GetALLCatalogbyUser")]
        public async Task<IActionResult> GetALLCatalogbyUser()
        {
            return Json(await Service.GetALLCatalogbyUser());
        }

 



    }
}

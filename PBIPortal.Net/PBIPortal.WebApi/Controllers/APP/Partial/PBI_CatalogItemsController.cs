/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("PBI_CatalogItems",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;

namespace PBI.APP.Controllers
{
    public partial class PBI_CatalogItemsController
    {
        [HttpPost, Route("GetPBICatalogItems")]
        public async Task<IActionResult> GetPBICatalogItems([FromBody]PBIItem pBIItem)
        {
            return Json(await Service.GetPBICatalogItems(pBIItem));

        }
    }
}

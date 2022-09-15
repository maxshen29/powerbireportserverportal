/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_CatalogController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_Catalog")]
    [PermissionTable(Name = "PBI_Catalog")]
    public partial class PBI_CatalogController : ApiBaseController<IPBI_CatalogService>
    {
        public PBI_CatalogController(IPBI_CatalogService service)
        : base(service)
        {
        }
    }
}


/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_CatalogItems_RoleController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_CatalogItems_Role")]
    [PermissionTable(Name = "PBI_CatalogItems_Role")]
    public partial class PBI_CatalogItems_RoleController : ApiBaseController<IPBI_CatalogItems_RoleService>
    {
        public PBI_CatalogItems_RoleController(IPBI_CatalogItems_RoleService service)
        : base(service)
        {
        }
    }
}


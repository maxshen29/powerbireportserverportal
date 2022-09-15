/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_CatalogItems_Role_GroupController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_CatalogItems_Role_Group")]
    [PermissionTable(Name = "PBI_CatalogItems_Role_Group")]
    public partial class PBI_CatalogItems_Role_GroupController : ApiBaseController<IPBI_CatalogItems_Role_GroupService>
    {
        public PBI_CatalogItems_Role_GroupController(IPBI_CatalogItems_Role_GroupService service)
        : base(service)
        {
        }
    }
}


/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_Catalog_Right_GroupController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_Catalog_Right_Group")]
    [PermissionTable(Name = "PBI_Catalog_Right_Group")]
    public partial class PBI_Catalog_Right_GroupController : ApiBaseController<IPBI_Catalog_Right_GroupService>
    {
        public PBI_Catalog_Right_GroupController(IPBI_Catalog_Right_GroupService service)
        : base(service)
        {
        }
    }
}


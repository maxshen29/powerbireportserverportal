/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_Group_UserController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_Group_User")]
    [PermissionTable(Name = "PBI_Group_User")]
    public partial class PBI_Group_UserController : ApiBaseController<IPBI_Group_UserService>
    {
        public PBI_Group_UserController(IPBI_Group_UserService service)
        : base(service)
        {
        }
    }
}


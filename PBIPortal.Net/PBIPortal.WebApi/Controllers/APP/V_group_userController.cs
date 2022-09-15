/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹V_group_userController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/V_group_user")]
    [PermissionTable(Name = "V_group_user")]
    public partial class V_group_userController : ApiBaseController<IV_group_userService>
    {
        public V_group_userController(IV_group_userService service)
        : base(service)
        {
        }
    }
}


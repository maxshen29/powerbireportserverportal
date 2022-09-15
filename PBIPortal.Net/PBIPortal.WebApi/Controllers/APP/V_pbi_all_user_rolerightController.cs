/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹V_pbi_all_user_rolerightController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/V_pbi_all_user_roleright")]
    [PermissionTable(Name = "V_pbi_all_user_roleright")]
    public partial class V_pbi_all_user_rolerightController : ApiBaseController<IV_pbi_all_user_rolerightService>
    {
        public V_pbi_all_user_rolerightController(IV_pbi_all_user_rolerightService service)
        : base(service)
        {
        }
    }
}


/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹V_Group_Report_RightController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/V_Group_Report_Right")]
    [PermissionTable(Name = "V_Group_Report_Right")]
    public partial class V_Group_Report_RightController : ApiBaseController<IV_Group_Report_RightService>
    {
        public V_Group_Report_RightController(IV_Group_Report_RightService service)
        : base(service)
        {
        }
    }
}


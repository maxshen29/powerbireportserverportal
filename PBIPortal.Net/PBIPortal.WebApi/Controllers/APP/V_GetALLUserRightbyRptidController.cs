/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹V_GetALLUserRightbyRptidController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/V_GetALLUserRightbyRptid")]
    [PermissionTable(Name = "V_GetALLUserRightbyRptid")]
    public partial class V_GetALLUserRightbyRptidController : ApiBaseController<IV_GetALLUserRightbyRptidService>
    {
        public V_GetALLUserRightbyRptidController(IV_GetALLUserRightbyRptidService service)
        : base(service)
        {
        }
    }
}


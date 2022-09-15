/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹V_group_indexpathController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/V_group_indexpath")]
    [PermissionTable(Name = "V_group_indexpath")]
    public partial class V_group_indexpathController : ApiBaseController<IV_group_indexpathService>
    {
        public V_group_indexpathController(IV_group_indexpathService service)
        : base(service)
        {
        }
    }
}


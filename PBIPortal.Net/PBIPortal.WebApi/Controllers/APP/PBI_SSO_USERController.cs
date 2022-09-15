/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹PBI_SSO_USERController编写
 */
using Microsoft.AspNetCore.Mvc;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.AttributeManager;
using PBI.APP.IServices;
namespace PBI.APP.Controllers
{
    [Route("api/PBI_SSO_USER")]
    [PermissionTable(Name = "PBI_SSO_USER")]
    public partial class PBI_SSO_USERController : ApiBaseController<IPBI_SSO_USERService>
    {
        public PBI_SSO_USERController(IPBI_SSO_USERService service)
        : base(service)
        {
        }
    }
}


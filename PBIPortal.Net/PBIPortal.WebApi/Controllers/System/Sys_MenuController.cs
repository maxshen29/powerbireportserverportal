using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    [Route("api/menu")]
    [ApiController, JWTAuthorize()]
    public partial class Sys_MenuController : ApiBaseController<ISys_MenuService>
    {
        private ISys_MenuService _service { get; set; }
        public Sys_MenuController(ISys_MenuService service) :
            base("System", "System", "Sys_Menu", service)
        {
            _service = service;
        } 
    }
}

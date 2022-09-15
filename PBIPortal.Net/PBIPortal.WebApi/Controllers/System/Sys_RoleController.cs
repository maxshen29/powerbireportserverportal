using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Filters;
using PBIPortal.Entity.AttributeManager;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    [Route("api/Sys_Role")]
    [PermissionTable(Name = "Sys_Role")]
    public partial class Sys_RoleController : ApiBaseController<ISys_RoleService>
    {
        public Sys_RoleController(ISys_RoleService service)
        : base("System", "System", "Sys_Role", service)
        {

        }
    }
}



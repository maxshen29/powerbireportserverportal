using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Entity.DomainModels;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    [Route("api/Sys_Log")]
    public partial class Sys_LogController : ApiBaseController<ISys_LogService>
    {
        public Sys_LogController(ISys_LogService service)
        : base("System", "System", "Sys_Log", service)
        { 
        }
    }
}

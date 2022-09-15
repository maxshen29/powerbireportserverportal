using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.System.IServices;
using PBIPortal.Core.Controllers.Basic;
using Microsoft.AspNetCore.Mvc;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_LogController : WebBaseController<ISys_LogService>
    {
        public Sys_LogController(ISys_LogService service)
        : base("System","System","Sys_Log", service)
        {
        }
    }
}


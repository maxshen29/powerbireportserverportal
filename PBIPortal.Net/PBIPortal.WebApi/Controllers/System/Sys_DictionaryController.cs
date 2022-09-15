using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PBIPortal.Core.Controllers.Basic;
using PBIPortal.Core.Extensions;
using PBIPortal.Core.Filters;
using PBIPortal.System.IServices;

namespace PBIPortal.System.Controllers
{
    [Route("api/Sys_Dictionary")]
    public partial class Sys_DictionaryController : ApiBaseController<ISys_DictionaryService>
    {
        public Sys_DictionaryController(ISys_DictionaryService service)
        : base("System", "System", "Sys_Dictionary", service)
        {
        }
    }
}

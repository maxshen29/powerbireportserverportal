﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.System.IServices;
using PBIPortal.Core.Controllers.Basic;
using Microsoft.AspNetCore.Mvc;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_MenuController : WebBaseController<ISys_MenuService>
    {
        public Sys_MenuController(ISys_MenuService service)
        : base("System","System","Sys_Menu", service)
        {
        }
        //public override ActionResult Manager()
        //{
        //    return base.Manager();
        //}
    }
}


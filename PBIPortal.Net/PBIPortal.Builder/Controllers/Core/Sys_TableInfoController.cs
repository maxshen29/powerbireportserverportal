﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyStar.Builder.IServices;
using PBIPortal.Core.Controllers.Basic;
using Microsoft.AspNetCore.Mvc;

namespace PBIPortal.Builder.Controllers
{
    public partial class Sys_TableInfoController : WebBaseController<ISys_TableInfoService>
    {
        public Sys_TableInfoController(ISys_TableInfoService service)
        : base("Builder","Core","Sys_TableInfo", service)
        {
        }
    }
}


﻿/*
 *Author：jxx
 *Contact：283591387@qq.com
 *Date：2018-07-01
 * 此代码由框架生成，请勿随意更改
 */
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.System.IServices;
using PBIPortal.Core.Controllers.Basic;
using Microsoft.AspNetCore.Mvc;

namespace PBIPortal.System.Controllers
{
    public partial class Sys_UserController : WebBaseController<ISys_UserService>
    {
        public Sys_UserController(ISys_UserService service)
        : base("System","System","Sys_User", service)
        {
        }
    }
}


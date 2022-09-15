using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PBIPortal.Core.Filters
{
    public class JWTAuthorizeAttribute : AuthorizeAttribute
    {
        public JWTAuthorizeAttribute() : base()
        {

        }
    }
}

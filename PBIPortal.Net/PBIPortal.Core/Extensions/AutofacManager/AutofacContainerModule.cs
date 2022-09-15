using PBIPortal.Core.Extensions;
using System;
using PBIPortal.Core.Configuration;

namespace PBIPortal.Core.Extensions.AutofacManager
{
    public class AutofacContainerModule
    {
        public static TService GetService<TService>() where TService:class
        {
            return typeof(TService).GetService() as TService;
        }
    }
}

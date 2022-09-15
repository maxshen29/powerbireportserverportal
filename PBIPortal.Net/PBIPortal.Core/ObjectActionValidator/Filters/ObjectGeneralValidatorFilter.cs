﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using PBIPortal.Core.Infrastructure;
using PBIPortal.Core.ObjectActionValidator;

namespace PBIPortal.Core.ObjectActionValidator
{
    //General
    public class ObjectGeneralValidatorFilter :Attribute, IFilterMetadata
    {
        //
        /// <summary>
        /// 对方法参数进行校验
        /// </summary>
        /// <param name="methodParams"></param>
        public ObjectGeneralValidatorFilter([NotNull]params ValidatorGeneral[]  validators)
        {
                MethodsParameters = validators.GetGeneralOption().ToArray();
        }
        public GeneralOptions[] MethodsParameters { get; }
    }
}

/*
 *所有关于V_group_user类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*V_group_userService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;
using System.Linq;
using PBIPortal.Core.Utilities;
using System.Linq.Expressions;
using PBIPortal.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PBI.APP.Services
{
    public partial class V_group_userService
    {
       
    }
}

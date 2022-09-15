using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Extensions.AutofacManager;
using PBIPortal.Entity.DomainModels;
using System.Linq;
using PBIPortal.Core.Extensions;
using System.Collections.Generic;
using PBIPortal.Core.Enums;

namespace PBIPortal.System.Services
{
    public partial class Sys_DictionaryListService
    {

        public override PageGridData<Sys_DictionaryList> GetPageData(PageDataOptions pageData)
        {
            base.OrderByExpression = x => new Dictionary<object, QueryOrderBy>() { {
                    x.OrderNo,QueryOrderBy.Desc
                },
                {
                    x.DicList_ID,QueryOrderBy.Asc
                }
            };
            return base.GetPageData(pageData);
        }
    }
}


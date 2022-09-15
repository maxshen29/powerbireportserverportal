﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PBIPortal.Core.BaseProvider;
using PBIPortal.Entity.DomainModels;

namespace PBIPortal.System.IServices
{
    public partial interface ISys_DictionaryService
    {
        /// <summary>
        /// 代码生成器获取所有字典项(超级管理权限)
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetBuilderDictionary();
        Task<object> GetVueDictionary(string[] dicNos);
        object GetTableDictionary(Dictionary<string, object[]> keyData);
        Task<object> GetSearchDictionary(string dicNo, string value);

        /// <summary>
        /// 表单设置为远程查询，重置或第一次添加表单时，获取字典的key、value
        /// </summary>
        /// <param name="dicNo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<object> GetRemoteDefaultKeyValue(string dicNo, string key);
    }
}


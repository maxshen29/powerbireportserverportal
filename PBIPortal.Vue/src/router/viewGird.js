let viewgird = [
   {
        path: '/Sys_Log',
        name: 'sys_Log',
        component: () => import('@/views/system/Sys_Log.vue')
    }
    ,{
        path: '/Sys_User',
        name: 'Sys_User',
        component: () => import('@/views/system/Sys_User.vue')
    }    ,{
        path: '/Sys_Dictionary',
        name: 'Sys_Dictionary',
        component: () => import('@/views/system/Sys_Dictionary.vue')
    }    ,{
        path: '/Sys_Role',
        name: 'Sys_Role',
        component: () => import('@/views/system/Sys_Role.vue')
    } 
    ,{
        path: '/Sys_DictionaryList',
        name: 'Sys_DictionaryList',
        component: () => import('@/views/system/Sys_DictionaryList.vue')
    } 
    ,{
        path: '/SellOrder',
        name: 'SellOrder',
        component: () => import('@/views/order/SellOrder.vue')
    }    ,{
        path: '/App_Appointment',
        name: 'App_Appointment',
        component: () => import('@/views/order/App_Appointment.vue')
    }    ,{
        path: '/App_TransactionAvgPrice',
        name: 'App_TransactionAvgPrice',
        component: () => import('@/views/appmanager/App_TransactionAvgPrice.vue')
    }    ,{
        path: '/App_Expert',
        name: 'App_Expert',
        component: () => import('@/views/appmanager/App_Expert.vue')
    }    ,{
        path: '/App_Transaction',
        name: 'App_Transaction',
        component: () => import('@/views/appmanager/App_Transaction.vue')
    }    ,{
        path: '/App_ReportPrice',
        name: 'App_ReportPrice',
        component: () => import('@/views/appmanager/App_ReportPrice.vue')
    }    ,{
        path: '/App_News',
        name: 'App_News',
        component: () => import('@/views/appmanager/App_News.vue')
    }    ,{
        path: '/PBI_SSO_ALL',
        name: 'PBI_SSO_ALL',
        component: () => import('@/views/app/PBI_SSO_ALL.vue')
    }    ,{
        path: '/PBI_SSO_DEPT',
        name: 'PBI_SSO_DEPT',
        component: () => import('@/views/app/PBI_SSO_DEPT.vue')
    }    ,{
        path: '/PBI_SSO_Group',
        name: 'PBI_SSO_Group',
        component: () => import('@/views/app/PBI_SSO_Group.vue')
    }    ,{
        path: '/PBI_User_Right',
        name: 'PBI_User_Right',
        component: () => import('@/views/app/PBI_User_Right.vue')
    }    ,{
        path: '/PBI_Group_Right',
        name: 'PBI_Group_Right',
        component: () => import('@/views/app/PBI_Group_Right.vue')
    }    ,{
        path: '/PBI_Group_User',
        name: 'PBI_Group_User',
        component: () => import('@/views/app/PBI_Group_User.vue')
    }    ,{
        path: '/PBI_CatalogItems',
        name: 'PBI_CatalogItems',
        component: () => import('@/views/app/PBI_CatalogItems.vue')
    }    ,{
        path: '/V_group_user',
        name: 'V_group_user',
        component: () => import('@/views/app/V_group_user.vue')
    }    ,{
        path: '/PBI_SSO_USER',
        name: 'PBI_SSO_USER',
        component: () => import('@/views/app/PBI_SSO_USER.vue')
    }    ,{
        path: '/PBI_Catalog',
        name: 'PBI_Catalog',
        component: () => import('@/views/app/PBI_Catalog.vue')
    }    ,{
        path: '/PBI_CustormCatalogItems',
        name: 'PBI_CustormCatalogItems',
        component: () => import('@/views/app/PBI_CustormCatalogItems.vue')
    }    ,{
        path: '/V_Group_Report_Right',
        name: 'V_Group_Report_Right',
        component: () => import('@/views/app/V_Group_Report_Right.vue')
    }    ,{
        path: '/V_User_Report_Right',
        name: 'V_User_Report_Right',
        component: () => import('@/views/app/V_User_Report_Right.vue')
    }    ,{
        path: '/V_GetALLUserRightbyRptid',
        name: 'V_GetALLUserRightbyRptid',
        component: () => import('@/views/app/V_GetALLUserRightbyRptid.vue')
    }    ,{
        path: '/V_group_indexpath',
        name: 'V_group_indexpath',
        component: () => import('@/views/app/V_group_indexpath.vue')
    }    ,{
        path: '/PBI_CatalogItems_Role',
        name: 'PBI_CatalogItems_Role',
        component: () => import('@/views/app/PBI_CatalogItems_Role.vue')
    }    ,{
        path: '/PBI_CatalogItems_Role_Group',
        name: 'PBI_CatalogItems_Role_Group',
        component: () => import('@/views/app/PBI_CatalogItems_Role_Group.vue')
    }    ,{
        path: '/V_pbi_all_user_roleright',
        name: 'V_pbi_all_user_roleright',
        component: () => import('@/views/app/V_pbi_all_user_roleright.vue')
    }    ,{
        path: '/PBI_Catalog_Right_Group',
        name: 'PBI_Catalog_Right_Group',
        component: () => import('@/views/app/PBI_Catalog_Right_Group.vue')
    }    ,{
        path: '/PBI_Catalog_Right_User',
        name: 'PBI_Catalog_Right_User',
        component: () => import('@/views/app/PBI_Catalog_Right_User.vue')
    }]
export default viewgird
let pbiinportal = [
    {

        path: '/PBIDeptUser',
        name: 'PBIDeptUser',
        component: () => import('@/views/PBI/PBIDeptUser.vue')
    }

    ,
    {
        path: '/PBICustormCatalogItems',
        name: 'PBICustormCatalogItems',
        component: () => import('@/views/PBI/PBICustormCatalogItems.vue')
    }
    ,
    {
        path: '/groupAdmin',
        name: 'groupAdmin',
        component: () => import('@/views/PBI/groupAdmin.vue')
    },
    {
        path: '/PBIReportRight',
        name: 'PBIReportRight',
        component: () => import('@/views/PBI/PBIReportRight.vue')
    }
    ,
    {
        path: '/ReportRoleManage',
        name: 'ReportRoleManage',
        component: () => import('@/views/PBI/ReportRoleManage.vue')
    },
    {
        path: '/PBICatalogRight',
        name: 'PBICatalogRight',
        component: () => import('@/views/PBI/PBICatalogRight.vue')
    }
   

]
export default pbiinportal;
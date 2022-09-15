let pbi = [
    {
        path: '/PBICustormPortal',
        name: 'PBICustormPortal',
        component: () => import('@/views/PBI/PBICustormPortal.vue')
    },
    {
        path: '/PBIPortal',
        name: 'PBIPortal',
        component: () => import('@/views/PBI/PBIPortal.vue')
    }
    ,
    {
        path: '/perinfo',
        name: 'perinfo',
        component: () => import('@/views/PBI/UserInfo.vue')
    }

    ,
    {
        path: '/ReportPortal',
        name: 'ReportPortal',
        component: () => import('@/views/PBI/ReportPortal.vue'),
        meta: {
            "X-Frame-Options": "SAMEORIGIN"
        }
    },
    {
        path: '/ReportINPortal',
        name: 'ReportINPortal',
        component: () => import('@/views/PBI/ReportINPortal.vue'),
        meta: {
            "X-Frame-Options": "SAMEORIGIN"
        }
    }

    ,
    {
        path: '/temp',
        name: 'temp',
        component: () => import('@/views/PBI/temp.vue'),
        meta: {
            anonymous: true,
            title: "接口登录"
        }
    }
    ,
    {
        path: '/temp1',
        name: 'temp1',
        component: () => import('@/views/PBI/temp1.vue'),
        meta: {
            anonymous: true,
            title: "接口登录"
        }
    },
    {
        path: '/temp2',
        name: 'temp2',
        component: () => import('@/views/PBI/temp2.vue'),
        meta: {
            anonymous: true,
            title: "接口登录"
        }
    },
    {
        path: '/temp3',
        name: 'temp4',
        component: () => import('@/views/PBI/temp3.vue'),
        meta: {
            anonymous: true,
            title: "接口登录"
        }
    },
    {
        path: '/testlogin',
        name: 'testlogin',
        component: () => import('@/views/PBI/testlogin.vue'),
        meta: {
            anonymous: true,
            title: "接口登录"
        }
    }

]
export default pbi;
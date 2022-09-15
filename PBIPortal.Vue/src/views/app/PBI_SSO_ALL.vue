<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_SSO_ALL.js此处编写
 -->
<template>
    <div>
        <view-grid :columns="columns"
                   :detail="detail"
                   :editFormFileds="editFormFileds"
                   :editFormOptions="editFormOptions"
                   :searchFormFileds="searchFormFileds"
                   :searchFormOptions="searchFormOptions"
                   :table="table"
                   :extend="extend">
        </view-grid>
    </div>
</template>

<script>
    import extend from "@/extension/app/PBI_SSO_ALL.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'USER_ID',
                    footer: "Foots",
                    cnName: 'SSO所有用户',
                    name: 'PBI_SSO_ALL',
                    url: "/PBI_SSO_ALL/",
                    sortName: "USER_ID"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'USER_ID',title:'USER_ID',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'USER_LOGIN_NAME',title:'USER_LOGIN_NAME',type:'string',width:120,align:'left',sortable:true},
                       {field:'DEPT_CODE',title:'DEPT_CODE',type:'string',width:120,align:'left'},
                       {field:'USER_SORT',title:'USER_SORT',type:'int',width:90,align:'left'},
                       {field:'USER_POST_CODE',title:'USER_POST_CODE',type:'string',width:120,align:'left'},
                       {field:'USER_POST',title:'USER_POST',type:'string',width:120,align:'left'},
                       {field:'DEPT_NAME',title:'DEPT_NAME',type:'string',width:120,align:'left'},
                       {field:'DEPT_LEVEL',title:'DEPT_LEVEL',type:'byte',width:90,align:'left'},
                       {field:'DEPT_SORT',title:'DEPT_SORT',type:'int',width:90,align:'left'},
                       {field:'TDEPT_CODE',title:'TDEPT_CODE',type:'string',width:90,align:'left'},
                       {field:'ODEPT_CODE',title:'ODEPT_CODE',type:'string',width:120,align:'left'},
                       {field:'CODE_PATH',title:'CODE_PATH',type:'string',width:220,align:'left'},
                       {field:'DEPT_FLAG',title:'DEPT_FLAG',type:'byte',width:90,align:'left'},
                       {field:'DEPT_PT_ID',title:'DEPT_PT_ID',type:'string',width:90,align:'left'},
                       {field:'USER_LEVEL',title:'USER_LEVEL',type:'string',width:120,align:'left'},
                       {field:'USER_LEVEL_NAME',title:'USER_LEVEL_NAME',type:'string',width:120,align:'left'},
                       {field:'USER_POST_NAME',title:'USER_POST_NAME',type:'string',width:120,align:'left'},
                       {field:'ORGI_DEPT_CODE',title:'ORGI_DEPT_CODE',type:'string',width:120,align:'left'},
                       {field:'DEPT_PCODE',title:'DEPT_PCODE',type:'string',width:120,align:'left'}],
                detail: {
                    cnName:"#detailCnName",
                    columns: [],
                    sortName: "",
                    key:""
                }
            };
        }
    };
    export default vueParam;
</script>

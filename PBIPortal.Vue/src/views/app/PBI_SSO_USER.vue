<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_SSO_USER.js此处编写
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
    import extend from "@/extension/app/PBI_SSO_USER.js";
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
                    name: 'PBI_SSO_USER',
                    url: "/PBI_SSO_USER/",
                    sortName: "USER_ID"
                },
                extend: extend,
                editFormFileds: {"ODEPT_CODE":"","USER_LOGIN_NAME":"","USER_TrueName":"","USER_IDCODE":""},
                editFormOptions: [[{"title":"部门ID","required":true,"field":"ODEPT_CODE","type":"text"}],
                              [{"title":"用户登录名","required":true,"field":"USER_LOGIN_NAME","type":"text"}],
                              [{"title":"用户显示名","required":true,"field":"USER_TrueName","type":"text"}],
                              [{"title":"身份证号码","required":true,"field":"USER_IDCODE","type":"text"}]],
                searchFormFileds: {"USER_LOGIN_NAME":"","ODEPT_CODE":"","USER_TrueName":"","USER_IDCODE":""},
                searchFormOptions: [[{"title":"用户登录名","field":"USER_LOGIN_NAME","type":"like"},{"title":"部门ID","field":"ODEPT_CODE","type":"like"},{"title":"用户显示名","field":"USER_TrueName","type":"like"},{"title":"身份证号码","field":"USER_IDCODE","type":"text"}]],
                columns: [{field:'USER_ID',title:'用户ID',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'USER_LOGIN_NAME',title:'用户登录名',type:'string',width:120,require:true,align:'left',sortable:true},
                       {field:'ODEPT_CODE',title:'部门ID',type:'string',width:120,require:true,align:'left'},
                       {field:'USER_TrueName',title:'用户显示名',type:'string',width:120,require:true,align:'left'},
                       {field:'USER_IDCODE',title:'身份证号码',type:'string ',width:90,require:true,align:'left'}],
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

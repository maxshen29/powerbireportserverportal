<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_pbi_all_user_roleright.js此处编写
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
    import extend from "@/extension/app/V_pbi_all_user_roleright.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'id',
                    footer: "Foots",
                    cnName: '角色权限视图',
                    name: 'V_pbi_all_user_roleright',
                    url: "/V_pbi_all_user_roleright/",
                    sortName: "id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {"PBI_Name":"","PBI_Path":"","User_LoginName":"","PBI_RoleName":"","PBI_RoleID":""},
                searchFormOptions: [[{"title":"登录名","field":"User_LoginName"},{"title":"报表路径","field":"PBI_Path"},{"title":"报表名称","field":"PBI_Name"}],[{"title":"报表角色名称","field":"PBI_RoleName"},{"title":"报表角色ID","field":"PBI_RoleID"}]],
                columns: [{field:'PBI_ID',title:'报表ID',type:'int',width:90,align:'left',sortable:true},
                       {field:'PBI_Name',title:'报表名称',type:'string',width:220,align:'left'},
                       {field:'PBI_Path',title:'报表路径',type:'string',width:220,align:'left'},
                       {field:'User_LoginName',title:'登录名',type:'string',width:220,align:'left'},
                       {field:'PBI_RoleName',title:'报表角色名称',type:'string',width:220,align:'left'},
                       {field:'PBI_RoleID',title:'报表角色ID',type:'guid',width:90,align:'left'},
                       {field:'id',title:'ID',type:'long',width:90,require:true,align:'left'}],
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

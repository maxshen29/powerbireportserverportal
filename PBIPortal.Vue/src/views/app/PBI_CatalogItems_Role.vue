<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_CatalogItems_Role.js此处编写
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
    import extend from "@/extension/app/PBI_CatalogItems_Role.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'ID',
                    footer: "Foots",
                    cnName: '报表行角色管理',
                    name: 'PBI_CatalogItems_Role',
                    url: "/PBI_CatalogItems_Role/",
                    sortName: "ID"
                },
                extend: extend,
                editFormFileds: {"PBI_ID":"","PBI_Name":"","PBI_Path":"","User_LoginName":"","PBI_RoleName":"","PBI_RoleID":""},
                editFormOptions: [[{"title":"报表ID","field":"PBI_ID","type":"number"},
                               {"title":"报表名称","field":"PBI_Name"}],
                              [{"title":"报表路径","field":"PBI_Path"}],
                              [{"title":"用户登录名","field":"User_LoginName"}],
                              [{"title":"报表角色名称","field":"PBI_RoleName"}],
                              [{"title":"角色ID","field":"PBI_RoleID"}]],
                searchFormFileds: {"PBI_Name":"","PBI_Path":"","User_LoginName":"","PBI_RoleName":"","PBI_RoleID":"","PBI_ID":""},
                searchFormOptions: [[{"title":"报表名称","field":"PBI_Name"},{"title":"报表路径","field":"PBI_Path"},{"title":"用户登录名","field":"User_LoginName"}],[{"title":"报表角色名称","field":"PBI_RoleName"},{"title":"角色ID","field":"PBI_RoleID"},{"title":"报表ID","field":"PBI_ID","type":"number"}]],
                columns: [{field:'ID',title:'ID',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'PBI_ID',title:'报表ID',type:'int',width:80,align:'left',sortable:true},
                       {field:'PBI_Name',title:'报表名称',type:'string',width:220,align:'left'},
                       {field:'PBI_Path',title:'报表路径',type:'string',width:220,align:'left'},
                       {field:'User_LoginName',title:'用户登录名',type:'string',width:220,align:'left'},
                       {field:'PBI_RoleName',title:'报表角色名称',type:'string',width:220,align:'left'},
                       {field:'PBI_RoleID',title:'角色ID',type:'guid',width:220,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,hidden:true,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:130,hidden:true,align:'left'},
                       {field:'Modifier',title:'Modifier',type:'string',width:130,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'ModifyDate',type:'datetime',width:90,hidden:true,align:'left',sortable:true},
                       {field:'ModifyID',title:'ModifyID',type:'int',width:80,hidden:true,align:'left'}],
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

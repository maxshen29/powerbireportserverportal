<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_Catalog_Right_User.js此处编写
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
    import extend from "@/extension/app/PBI_Catalog_Right_User.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'Id',
                    footer: "Foots",
                    cnName: 'PBI目录用户权限',
                    name: 'PBI_Catalog_Right_User',
                    url: "/PBI_Catalog_Right_User/",
                    sortName: "Id"
                },
                extend: extend,
                editFormFileds: {"CatalogId":"","USER_LOGIN_NAME":"","USER_TrueName":"","User_Id":"","CreatedDate":""},
                editFormOptions: [[{"title":"","field":"CatalogId","type":"number"}],
                              [{"title":"","field":"USER_LOGIN_NAME"}],
                              [{"title":"","field":"USER_TrueName"}],
                              [{"title":"","field":"User_Id","type":"number"}],
                              [{"title":"","field":"CreatedDate"}]],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Id',title:'Id',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'CatalogId',title:'CatalogId',type:'int',width:90,align:'left',sortable:true},
                       {field:'USER_LOGIN_NAME',title:'USER_LOGIN_NAME',type:'string',width:90,align:'left'},
                       {field:'USER_TrueName',title:'USER_TrueName',type:'string',width:90,align:'left'},
                       {field:'User_Id',title:'User_Id',type:'int',width:90,align:'left'},
                       {field:'ModifiedBy',title:'ModifiedBy',type:'string',width:90,align:'left'},
                       {field:'ModifiedDate',title:'ModifiedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreatedBy',title:'CreatedBy',type:'string',width:90,align:'left'},
                       {field:'CreatedDate',title:'CreatedDate',type:'datetime',width:90,align:'left',sortable:true}],
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

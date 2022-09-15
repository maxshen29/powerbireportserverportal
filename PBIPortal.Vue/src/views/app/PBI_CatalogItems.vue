<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_CatalogItems.js此处编写
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
    import extend from "@/extension/app/PBI_CatalogItems.js";
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
                    cnName: 'PBI_CatalogItems',
                    name: 'PBI_CatalogItems',
                    url: "/PBI_CatalogItems/",
                    sortName: "ID"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Id',title:'Id',type:'guid',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Name',title:'Name',type:'string',width:120,align:'left',sortable:true},
                       {field:'Description',title:'Description',type:'string',width:120,align:'left'},
                       {field:'Path',title:'Path',type:'string',width:90,align:'left'},
                       {field:'Type',title:'Type',type:'string',width:120,align:'left'},
                       {field:'Hidden',title:'Hidden',type:'string',width:90,align:'left'},
                       {field:'Size',title:'Size',type:'int',width:90,align:'left'},
                       {field:'ModifiedBy',title:'ModifiedBy',type:'string',width:90,align:'left'},
                       {field:'ModifiedDate',title:'ModifiedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreatedBy',title:'CreatedBy',type:'string',width:90,align:'left'},
                       {field:'CreatedDate',title:'CreatedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'ParentFolderId',title:'ParentFolderId',type:'guid',width:90,align:'left'},
                       {field:'IsFavorite',title:'IsFavorite',type:'string',width:90,align:'left'},
                       {field:'Roles',title:'Roles',type:'string',width:90,align:'left'},
                       {field:'ContentType',title:'ContentType',type:'string',width:220,align:'left'},
                       {field:'Content',title:'Content',type:'string',width:220,align:'left'}],
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

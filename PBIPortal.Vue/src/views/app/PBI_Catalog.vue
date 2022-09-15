<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_Catalog.js此处编写
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
    import extend from "@/extension/app/PBI_Catalog.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'CatalogId',
                    footer: "Foots",
                    cnName: 'PBI自定义目录管理',
                    name: 'PBI_Catalog',
                    url: "/PBI_Catalog/",
                    sortName: "sort"
                },
                extend: extend,
                editFormFileds: {"Name":"","Description":"","ParentCatalogId":""},
                editFormOptions: [[{"title":"","field":"Name"},
                               {"title":"","field":"Description"},
                               {"title":"","field":"ParentCatalogId","type":"number"}]],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Name',title:'Name',type:'string',width:120,align:'left',sortable:true},
                       {field:'Description',title:'Description',type:'string',width:120,align:'left'},
                       {field:'ModifiedBy',title:'ModifiedBy',type:'string',width:90,align:'left'},
                       {field:'ModifiedDate',title:'ModifiedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreatedBy',title:'CreatedBy',type:'string',width:90,align:'left'},
                       {field:'CreatedDate',title:'CreatedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CatalogId',title:'CatalogId',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'ParentCatalogId',title:'ParentCatalogId',type:'int',width:90,align:'left'},
                       {field:'Sort',title:'Sort',type:'int',width:80,align:'left'}],
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

<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_Catalog_Right_Group.js此处编写
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
    import extend from "@/extension/app/PBI_Catalog_Right_Group.js";
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
                    cnName: '目录组权限表',
                    name: 'PBI_Catalog_Right_Group',
                    url: "/PBI_Catalog_Right_Group/",
                    sortName: "Id"
                },
                extend: extend,
                editFormFileds: {"CatalogId":"","Group_Name":"","Group_Id":"","CreatedDate":""},
                editFormOptions: [[{"title":"PBI目录ID","field":"CatalogId","type":"number"}],
                              [{"title":"组名称","field":"Group_Name"}],
                              [{"title":"组ID","field":"Group_Id","type":"number"}],
                              [{"title":"创建时间","field":"CreatedDate"}]],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Id',title:'id',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'CatalogId',title:'PBI目录ID',type:'int',width:90,align:'left',sortable:true},
                       {field:'Group_Name',title:'组名称',type:'string',width:180,align:'left'},
                       {field:'Group_Id',title:'组ID',type:'int',width:90,align:'left'},
                       {field:'ModifiedBy',title:'ModifiedBy',type:'string',width:90,align:'left'},
                       {field:'ModifiedDate',title:'ModifiedDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreatedBy',title:'CreatedBy',type:'string',width:90,align:'left'},
                       {field:'CreatedDate',title:'创建时间',type:'datetime',width:90,align:'left',sortable:true}],
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

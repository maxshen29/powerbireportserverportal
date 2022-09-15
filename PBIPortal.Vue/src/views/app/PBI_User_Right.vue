<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_User_Right.js此处编写
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
    import extend from "@/extension/app/PBI_User_Right.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'R_Id',
                    footer: "Foots",
                    cnName: '用户权限',
                    name: 'PBI_User_Right',
                    url: "/PBI_User_Right/",
                    sortName: "R_Id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'R_Id',title:'R_Id',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'PBI_ID',title:'PBI_ID',type:'int',width:90,align:'left',sortable:true},
                       {field:'USER_ID',title:'USER_ID',type:'int',width:90,align:'left'},
                       {field:'Right',title:'Right',type:'int',width:90,align:'left'},
                       {field:'Path',title:'Path',type:'string',width:90,align:'left'},
                       {field:'CreateDate',title:'CreateDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,hidden:true,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:130,align:'left'},
                       {field:'Modifier',title:'Modifier',type:'string',width:130,align:'left'},
                       {field:'ModifyDate',title:'ModifyDate',type:'datetime',width:90,align:'left',sortable:true},
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

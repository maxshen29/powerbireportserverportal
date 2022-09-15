<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_group_indexpath.js此处编写
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
    import extend from "@/extension/app/V_group_indexpath.js";
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
                    cnName: 'PBI组首页地址',
                    name: 'V_group_indexpath',
                    url: "/V_group_indexpath/",
                    sortName: "id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Group_ID',title:'Group_ID',type:'int',width:90,align:'left',sortable:true},
                       {field:'USER_Login_Name',title:'USER_Login_Name',type:'string',width:180,align:'left'},
                       {field:'PBI_Index_Path',title:'PBI_Index_Path',type:'string',width:220,align:'left'},
                       {field:'Group_Priority',title:'Group_Priority',type:'int',width:90,align:'left'},
                       {field:'id',title:'id',type:'long',width:80,require:true,align:'left'},
                       {field:'rpt_type',title:'rpt_type',type:'string',width:90,align:'left'}],
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

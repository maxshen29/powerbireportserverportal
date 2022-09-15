<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_Group_Report_Right.js此处编写
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
    import extend from "@/extension/app/V_Group_Report_Right.js";
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
                    cnName: '组权限视图',
                    name: 'V_Group_Report_Right',
                    url: "/V_Group_Report_Right/",
                    sortName: "R_Id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'Group_Id',title:'Group_Id',type:'int',width:90,require:true,align:'left',sortable:true},
                       {field:'Group_Name',title:'Group_Name',type:'string',width:220,align:'left'},
                       {field:'Name',title:'Name',type:'string',width:120,align:'left'},
                       {field:'PBI_ID',title:'PBI_ID',type:'int',width:80,align:'left'},
                       {field:'R_Id',title:'R_Id',type:'int',width:80,require:true,align:'left'}],
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

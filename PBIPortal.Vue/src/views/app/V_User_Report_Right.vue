<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_User_Report_Right.js此处编写
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
    import extend from "@/extension/app/V_User_Report_Right.js";
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
                    cnName: '用户权限表视图',
                    name: 'V_User_Report_Right',
                    url: "/V_User_Report_Right/",
                    sortName: "R_Id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'PBI_ID',title:'PBI_ID',type:'int',width:90,align:'left',sortable:true},
                       {field:'R_Id',title:'R_Id',type:'int',width:90,require:true,align:'left'},
                       {field:'USER_TrueName',title:'USER_TrueName',type:'string',width:120,align:'left'},
                       {field:'USER_LOGIN_NAME',title:'USER_LOGIN_NAME',type:'string',width:120,require:true,align:'left'}],
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

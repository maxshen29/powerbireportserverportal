<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_GetALLUserRightbyRptid.js此处编写
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
    import extend from "@/extension/app/V_GetALLUserRightbyRptid.js";
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
                    cnName: '报表权限视图',
                    name: 'V_GetALLUserRightbyRptid',
                    url: "/V_GetALLUserRightbyRptid/",
                    sortName: "id"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {"USER_Login_Name":"","USER_ID":"","ODEPT_CODE":"","USER_TrueName":"","PBI_ID":"","Path":""},
                searchFormOptions: [[{"title":"用户登录名","field":"USER_Login_Name","type":"like"},{"title":"用户ID","field":"USER_ID","type":"number"},{"title":"用户显示名","field":"USER_TrueName","type":"like"}],[{"title":"部门ID","field":"ODEPT_CODE","type":"like"},{"title":"报表ID","field":"PBI_ID","type":"number"},{"title":"报表路径","field":"Path","type":"like"}]],
                columns: [{field:'USER_Login_Name',title:'用户登录名',type:'string',width:220,align:'left',sortable:true},
                       {field:'USER_TrueName',title:'用户显示名',type:'string',width:120,align:'left'},
                       {field:'USER_ID',title:'用户ID',type:'int',width:90,require:true,align:'left'},
                       {field:'ODEPT_CODE',title:'部门ID',type:'string',width:120,align:'left'},
                       {field:'PBI_ID',title:'报表ID',type:'int',width:90,align:'left'},
                       {field:'Path',title:'报表路径',type:'string',width:90,align:'left'},
                       {field:'id',title:'id',type:'long',width:80,require:true,align:'left'}],
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

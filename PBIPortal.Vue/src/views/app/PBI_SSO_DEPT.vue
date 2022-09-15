<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_SSO_DEPT.js此处编写
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
    import extend from "@/extension/app/PBI_SSO_DEPT.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'DEPT_ID',
                    footer: "Foots",
                    cnName: '部门表',
                    name: 'PBI_SSO_DEPT',
                    url: "/PBI_SSO_DEPT/",
                    sortName: "DEPT_ID"
                },
                extend: extend,
                editFormFileds: {"DEPT_CODE":"","DEPT_NAME":"","DEPT_LEVEL":"","DEPT_PCODE":""},
                editFormOptions: [[{"title":"部门代码","required":true,"field":"DEPT_CODE","type":"text"}],
                              [{"title":"部门名称","required":true,"field":"DEPT_NAME","type":"text"}],
                              [{"title":"部门层级","required":true,"field":"DEPT_LEVEL","type":"text"}],
                              [{"title":"父部门ID","required":true,"field":"DEPT_PCODE","type":"text"}]],
                searchFormFileds: {"DEPT_CODE":"","DEPT_NAME":"","DEPT_LEVEL":"","DEPT_PCODE":""},
                searchFormOptions: [[{"title":"部门名称","field":"DEPT_NAME","type":"like"},{"title":"部门代码","field":"DEPT_CODE","type":"like"},{"title":"部门层级","field":"DEPT_LEVEL","type":"number"},{"title":"父部门ID","field":"DEPT_PCODE","type":"like"}]],
                columns: [{field:'DEPT_ID',title:'部门id',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'DEPT_CODE',title:'部门代码',type:'string',width:120,require:true,align:'left',sortable:true},
                       {field:'DEPT_NAME',title:'部门名称',type:'string',width:120,require:true,align:'left'},
                       {field:'DEPT_LEVEL',title:'部门层级',type:'byte',width:90,require:true,align:'left'},
                       {field:'DEPT_PCODE',title:'父部门ID',type:'string',width:120,require:true,align:'left'}],
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

<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_Group_User.js此处编写
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
    import extend from "@/extension/app/PBI_Group_User.js";
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
                    cnName: '组和用户',
                    name: 'PBI_Group_User',
                    url: "/PBI_Group_User/",
                    sortName: "R_Id"
                },
                extend: extend,
                editFormFileds: {"USER_TrueName":"","USER_Login_Name":"","Group_Name":""},
                editFormOptions: [[{"title":"用户显示名","field":"USER_TrueName","type":"text"}],
                              [{"title":"登录名","field":"USER_Login_Name","type":"text"}],
                              [{"title":"组名称","field":"Group_Name","type":"text"}]],
                searchFormFileds: {"USER_TrueName":"","USER_Login_Name":"","Group_Name":""},
                searchFormOptions: [[{"title":"用户显示名","field":"USER_TrueName","type":"text"},{"title":"登录名","field":"USER_Login_Name","type":"text"},{"title":"组名称","field":"Group_Name","type":"text"}]],
                columns: [{field:'R_Id',title:'ID',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Group_ID',title:'部门ID',type:'int',width:90,hidden:true,align:'left'},
                       {field:'Group_Name',title:'组名称',type:'string',width:90,align:'left',sortable:true},
                       {field:'USER_ID',title:'用户ID',type:'int',width:90,hidden:true,align:'left'},
                       {field:'USER_TrueName',title:'用户显示名',type:'string',width:90,align:'left'},
                       {field:'USER_Login_Name',title:'登录名',type:'string',width:90,align:'left'},
                       {field:'CreateDate',title:'创建日期',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,hidden:true,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:130,hidden:true,align:'left'},
                       {field:'Modifier',title:'Modifier',type:'string',width:130,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'ModifyDate',type:'datetime',width:90,hidden:true,align:'left',sortable:true},
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

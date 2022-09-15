<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/V_group_user.js此处编写
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
    import extend from "@/extension/app/V_group_user.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'vid',
                    footer: "Foots",
                    cnName: '组和用户视图',
                    name: 'V_group_user',
                    url: "/V_group_user/",
                    sortName: "type"
                },
                extend: extend,
                editFormFileds: {},
                editFormOptions: [],
                searchFormFileds: {},
                searchFormOptions: [],
                columns: [{field:'id',title:'id',type:'int',width:90,align:'left',sortable:true},
                       {field:'name',title:'name',type:'string',width:220,align:'left'},
                       {field:'truename',title:'truename',type:'string',width:220,align:'left'},
                       {field:'pid',title:'pid',type:'int',width:90,align:'left'},
                       {field:'CreateDate',title:'CreateDate',type:'datetime',width:90,align:'left',sortable:true},
                       {field:'type',title:'type',type:'string',width:90,require:true,align:'left'},
                       {field:'vid',title:'vid',type:'int',width:90,require:true,align:'left'}],
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

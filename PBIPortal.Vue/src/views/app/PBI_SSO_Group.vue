<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/app/PBI_SSO_Group.js此处编写
 -->
<template>
  <div>
    <view-grid
      :columns="columns"
      :detail="detail"
      :editFormFileds="editFormFileds"
      :editFormOptions="editFormOptions"
      :searchFormFileds="searchFormFileds"
      :searchFormOptions="searchFormOptions"
      :table="table"
      :extend="extend"
    >
    </view-grid>
  </div>
</template>

<script>
import extend from "@/extension/app/PBI_SSO_Group.js";
import ViewGrid from "@/components/basic/ViewGrid.vue";
var vueParam = {
  components: {
    ViewGrid,
  },
  data() {
    return {
      table: {
        key: "Group_Id",
        footer: "Foots",
        cnName: "用户组管理",
        name: "PBI_SSO_Group",
        url: "/PBI_SSO_Group/",
        sortName: "Group_Id",
      },
      extend: extend,
      editFormFileds: {
        Group_Name: "",
        Group_Priority: "",
        PBI_Index_Path: "",
        RPT_Type: "",
      },
      editFormOptions: [
        [
          {
            title: "群组名称",
            required: true,
            field: "Group_Name",
            type: "text",
          },
        ],
        [
          {
            title: "优先级",
            required: true,
            field: "Group_Priority",
            type: "number",
          },
        ],
        [{ title: "首页路径", field: "PBI_Index_Path", type: "text" }],
        [
          {
            dataKey: "报表类型",
            title: "首页报表类型",
            field: "RPT_Type",
            type: "select",
          },
        ],
      ],
      searchFormFileds: {
        Group_Name: "",
        Group_Priority: "",
        PBI_Index_Path: "",
        RPT_Type: "",
      },
      searchFormOptions: [
        [
          { title: "群组名称", field: "Group_Name", type: "like" },
          { title: "优先级", field: "Group_Priority", type: "like" },
          { title: "首页路径", field: "PBI_Index_Path", type: "like" },
          {
            dataKey: "报表类型",
            title: "首页报表类型",
            field: "RPT_Type",
            type: "text",
          },
        ],
      ],
      columns: [
        {
          field: "Group_Id",
          title: "群组ID",
          type: "int",
          width: 90,
          hidden: true,
          readonly: true,
          require: true,
          align: "left",
        },
        {
          field: "Group_Name",
          title: "群组名称",
          type: "string",
          width: 220,
          require: true,
          align: "left",
          sort: true,
        },
        {
          field: "Group_Level",
          title: "群组级别",
          type: "int",
          width: 90,
          hidden: true,
          align: "left",
        },
        {
          field: "Group_PID",
          title: "父ID（未用）",
          type: "int",
          width: 90,
          hidden: true,
          align: "left",
        },
        {
          field: "CreateDate",
          title: "创建日期",
          type: "datetime",
          width: 90,
          hidden: true,
          align: "left",
          sort: true,
        },
        {
          field: "CreateID",
          title: "CreateID",
          type: "int",
          width: 80,
          hidden: true,
          align: "left",
        },
        {
          field: "Creator",
          title: "Creator",
          type: "string",
          width: 130,
          hidden: true,
          align: "left",
        },
        {
          field: "Modifier",
          title: "Modifier",
          type: "string",
          width: 130,
          hidden: true,
          align: "left",
        },
        {
          field: "ModifyDate",
          title: "ModifyDate",
          type: "datetime",
          width: 90,
          hidden: true,
          align: "left",
          sort: true,
        },
        {
          field: "ModifyID",
          title: "ModifyID",
          type: "int",
          width: 80,
          hidden: true,
          align: "left",
        },
        {
          field: "Group_Priority",
          title: "优先级",
          type: "int",
          width: 80,
          require: true,
          align: "left",
        },
        {
          field: "PBI_Index_Path",
          title: "首页路径",
          type: "string",
          width: 220,
          align: "left",
        },
        {
          field: "RPT_Type",
          title: "首页报表类型",
          type: "string",
          bind: { key: "报表类型", data: [] },
          width: 90,
          align: "left",
        },
      ],
      detail: {
        cnName: "#detailCnName",
        columns: [],
        sortName: "",
        key: "",
      },
    };
  },
};
export default vueParam;
</script>

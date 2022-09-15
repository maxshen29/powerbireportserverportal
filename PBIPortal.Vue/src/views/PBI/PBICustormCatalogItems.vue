<template>
  <div class="menu-container">
    <div class="menu-left">
      <div class="menu-tree" style="height: 95%; margin-bottom: 100px">
        <el-input placeholder="搜索目录" v-model="filterTextCatalog"></el-input>
        <Icon type="md-add"></Icon>
        <span>报表目录</span>
        <span style="cursor: pointer" @click="addReportMainCatalog"
          >【添加主目录】</span
        >
        <el-tree
          class="filter-tree"
          :data="catalogdata"
          node-key="id"
          default-expand-all
          @node-click="appendCatalog"
          :filter-node-method="filterNodeCatalog"
          ref="treeCatalog"
        >
          <div class="action-group" slot-scope="{ node, data }">
            <div class="action-text">
              <Icon :type="data.icon"></Icon>
              {{ data.label }}
            </div>
          </div>
        </el-tree>
      </div>
    </div>

    <div class="menu-right">
      <div class="rightAll">
        <div class="head">
          <span class="project-name">Power BI 报表内容和目录管理</span>
        </div>
        <div class="align-center-vertical" id="ContentText">
          Power BI 报表内容和目录管理
        </div>
        <div class="contentTable" id="Content">
          <VolHeader
            icon="md-apps"
            :text="selectGroupName"
            style="margin-bottom: 20px"
          >
            <div
              style="
                margin-bottom: 10px;
                margin-top: 10px;
                margin-left: 10px;
                text-align: right;
              "
            >
              <Button type="info" ghost @click="addReport">添加报表</Button>
              <Button type="info" ghost @click="updateReport">修改报表</Button>
              <Button type="info" ghost @click="addReportCatalog"
                >添加子目录</Button
              >
              <Button type="info" ghost @click="UpdateReportCatalog"
                >修改目录</Button
              >
              <Button type="info" ghost @click="delReport">删除报表</Button>
              <Button type="info" ghost @click="delReportCatalog"
                >删除目录</Button
              >
            </div>
          </VolHeader>
          <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px">
            <vol-table
              ref="tableCatalogItems"
              :loadKey="true"
              :linkView="_linkViewCatalogItems"
              :columns="columnsCatalogItems"
              :pagination="paginationCatalogItems"
              :height="700"
              :url="urlCatalogItems"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeCatalogItems"
              @loadAfter="loadTableAfterCatalogItems"
            ></vol-table>
          </div>
          <div class="block pagination" v-if="!paginationHide">
            <el-pagination
              @size-change="handleSizeChange"
              @current-change="handleCurrentChange"
              :current-page="paginationCatalogItems.page"
              :page-sizes="[30, 60, 100, 300, 500]"
              :page-size="paginationCatalogItems.size"
              layout="total, sizes, prev, pager, next, jumper"
              :total="paginationCatalogItems.total"
            ></el-pagination>
          </div>
        </div>
      </div>
    </div>
    <vol-box
      :model.sync="viewModelReport"
      :height="300"
      :width="600"
      :title="addreportTile"
    >
      <VolForm
        ref="myformReport"
        :loadKey="loadKey"
        :formFileds="formFiledsReport"
        :formRules="formRulesReport"
      ></VolForm>
      <div slot="footer">
        <Button type="info" @click="addReports">确认</Button>
        <Button type="info" @click="viewModelReport = false">取消</Button>
      </div>
    </vol-box>

    <vol-box
      :model.sync="viewupdateModelReport"
      :height="300"
      :width="600"
      :title="updatereportTile"
    >
      <VolForm
        ref="myformupdateReport"
        :loadKey="loadKey"
        :formFileds="formFiledsUpdateReport"
        :formRules="formRulesUpdateReport"
      ></VolForm>
      <div slot="footer">
        <Button type="info" @click="updateReports">确认</Button>
        <Button type="info" @click="viewupdateModelReport = false">取消</Button>
      </div>
    </vol-box>

    <vol-box
      :model.sync="viewModelCatalog"
      :height="200"
      :width="600"
      :title="addCatalogTile"
    >
      <VolForm
        ref="myformCatalog"
        :loadKey="loadKey"
        :formFileds="formFiledsCatalog"
        :formRules="formRulesCatalog"
      ></VolForm>
      <div slot="footer">
        <Button type="info" @click="addCatalog">确认</Button>
        <Button type="info" @click="viewModelCatalog = false">取消</Button>
      </div>
    </vol-box>

    <vol-box
      :model.sync="viewUpdateModelCatalog"
      :height="200"
      :width="600"
      :title="addCatalogTile"
    >
      <VolForm
        ref="myformUpdateCatalog"
        :loadKey="loadKey"
        :formFileds="formFiledsCatalog"
        :formRules="formRulesCatalog"
      ></VolForm>
      <div slot="footer">
        <Button type="info" @click="UpdateCatalog">确认</Button>
        <Button type="info" @click="viewUpdateModelCatalog = false"
          >取消</Button
        >
      </div>
    </vol-box>
  </div>
</template>
<script>
import VolMenu from "@/components/basic/VolMenu.vue";
let imgUrl = require("@/assets/imgs/logo.png");
import VolTable from "@/components/basic/VolTable.vue";
import VolBox from "@/components/basic/VolBox.vue";
import VolForm from "@/components/basic/VolForm.vue";
import VolHeader from "@/components/basic/VolHeader.vue";
export default {
  components: { VolTable, VolBox, VolForm, VolHeader },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    },
    filterTextCatalog(val) {
      this.$refs.treeCatalog.filter(val);
    },
  },

  created() {
    this.getPowerBIFoder("/");
    //document.getElementById("ContentText").style.display = "none";
    // document.getElementById("Content").style.display = "block";

    // //console.log(this.logo);
  },
  data() {
    const tdata = [];
    const catalogdata = [];

    return {
      selectGroupName: "",
      loadKey: true,
      addreportTile: "",
      updatereportTile: "",
      formFiledsReport: {
        name: "",
        description: "",
        path: "",
        CreatedDate: this.getDateStr(),
        parentCustormCatalogId: this.pidCatalogItems,
        // reportid: "",
        type: "",
        hidden: "",
        size: 0,
        //  ParentFolderId: "",
        isFavorite: "",
        roles: "",
        contentType: "",
        content: "",
        sort: "",
      },
      formRulesReport: [
        [
          //两列的表单，formRules数据格式为:[[{},{}]]

          {
            title: "报表名称",
            required: true, //设置为必选项
            field: "name",
            type: "text",
          },
        ],
        [
          {
            title: "报表排序",
            required: true, //设置为必选项
            field: "sort",
            type: "text",
            placeholder: "排序数字越大越靠前,1最小。",
          },
        ],
        [
          {
            title: "报表路径",
            required: true, //设置为必选项
            field: "path",
            type: "text",
          },
        ],
        [
          {
            title: "报表类型",
            required: true, //设置为必选项
            field: "type",
            type: "select",

            data: [
              { key: "分页报表", value: "分页报表" },
              { key: "PowerBI报表", value: "PowerBI报表" },
            ],
          },
        ],
        [
          {
            title: "报表说明",
            required: true, //设置为必选项
            field: "description",
            type: "text",
          },
        ],
      ],

      formFiledsUpdateReport: {
        name: "",
        description: "",
        path: "",
        CreatedDate: this.getDateStr(),
        parentCustormCatalogId: this.pidCatalogItems,
        // reportid: "",
        type: "",
        hidden: "",
        size: 0,
        //  ParentFolderId: "",
        isFavorite: "",
        roles: "",
        contentType: "",
        content: "",
        sort: "",
      },
      formRulesUpdateReport: [
        [
          //两列的表单，formRules数据格式为:[[{},{}]]

          {
            title: "报表名称",
            required: true, //设置为必选项
            field: "name",
            type: "text",
          },
        ],
        [
          {
            title: "报表排序",
            required: true, //设置为必选项
            field: "sort",
            type: "number",
            placeholder: "排序数字越大越靠前,1最小。",
          },
        ],
        [
          {
            title: "报表路径",
            required: true, //设置为必选项
            field: "path",
            type: "text",
          },
        ],
        [
          {
            title: "报表类型",
            required: true, //设置为必选项
            field: "type",
            type: "select",
            data: [
              { key: "分页报表", value: "分页报表" },
              { key: "PowerBI报表", value: "PowerBI报表" },
            ],
          },
        ],

        [
          {
            title: "报表说明",
            required: true, //设置为必选项
            field: "description",
            type: "text",
          },
        ],
      ],

      addCatalogTile: "",
      formFiledsCatalog: {
        name: "",
        description: "",
        CreatedDate: this.getDateStr(),
        ParentCatalogId: this.pidCatalogItems,
        // reportid: "",
        type: "",
        sort: "",
        CatalogId: "",
      },
      formRulesCatalog: [
        [
          //两列的表单，formRules数据格式为:[[{},{}]]

          {
            title: "报表目录名称",
            required: true, //设置为必选项
            field: "name",
            type: "text",
          },
        ],
        [
          {
            title: "报表目录说明",
            required: true, //设置为必选项
            field: "description",
            type: "text",
          },
        ],
        [
          {
            title: "报表目录排序",
            required: true, //设置为必选项
            field: "sort",
            type: "number",
            placeholder: "排序数字越大越靠前,1最小。",
          },
        ],
      ],

      log: imgUrl,
      pid: 0,
      pidCatalog: 0,
      pidCatalogItems: 0,
      groupid: 0,
      date: "",
      selectId: 0,
      links: [{ text: "安全退出", path: "/login", id: -4 }],
      userName: "--",
      token: "--",
      reportUrl: "--",
      filterText: "",
      filterTextCatalog: "",
      fromdisplay: "display:none;",
      titledisplay: "display:block",
      tdata: JSON.parse(JSON.stringify(tdata)),
      catalogdata: JSON.parse(JSON.stringify(catalogdata)),
      logo: imgUrl,
      defaultProps: {
        children: "children",
        label: "label",
      },
      text: "",

      viewModelReport: false,
      viewModelCatalog: false,
      viewUpdateModelCatalog: false,
      viewupdateModelReport: false,

      urlCatalogItems: "api/PBI_CustormCatalogItems/getPageData", //后从加载数据的url

      paginationHide: {
        type: Boolean,
        default: false,
      },

      columnsCatalogItems: [
        //表配置
        {
          field: "Id", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "string", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left", //文字显示位置
        },
        {
          field: "Name",
          title: "报表名称",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Sort",
          title: "排序",
          type: "int",
          width: 20,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Path",
          title: "路径",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Type",
          title: "类型",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Description",
          title: "描述",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
      ],

      paginationCatalogItems: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30,
      },
    };
  },
  methods: {
    delReport() {
      let rows = this.$refs.tableCatalogItems.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      //console.log(rows);
      let delKeys = rows.map((x) => {
        return x.id;
      });
      // //console.log(delKeys);
      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_CustormCatalogItems/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, "正在删除数据....").then((x) => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.tableCatalogItems.load();
        return this.$message("删除完成");
      });
    },
    updateReport() {
      let rows = this.$refs.tableCatalogItems.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要修改的报表!");
      if (rows.length > 1) return this.$message.error("请选择一张报表!");

      ////console.log(rows);

      this.formFiledsUpdateReport.id = rows[0].id;
      this.formFiledsUpdateReport.name = rows[0].Name;
      this.formFiledsUpdateReport.description = rows[0].Description;
      this.formFiledsUpdateReport.path = rows[0].Path;
      this.formFiledsUpdateReport.size = "0";
      this.formFiledsUpdateReport.sort = rows[0].Sort;
      this.formFiledsUpdateReport.type = rows[0].Type;
      this.formFiledsUpdateReport.parentCustormCatalogId = this.pidCatalogItems;

      this.updatereportTile = "修改报表:" + this.formFiledsUpdateReport.name;

      //console.log(this.formFiledsUpdateReport);
      this.viewupdateModelReport = true;
    },

    addReport() {
      this.addreportTile = this.selectGroupName + "--添加报表";
      this.viewModelReport = true;
    },

    updateReports() {
      if (!this.$refs.myformupdateReport.validate()) {
        return;
      }
      this.formFiledsUpdateReport.parentCustormCatalogId = this.pidCatalogItems;
      //   this.formFiledsReport.createDate = this.getDateStr();
      //console.log(this.formFiledsUpdateReport);

      this.http
        .post(
          "/api/PBI_CustormCatalogItems/UpdateCustormReport",
          this.formFiledsUpdateReport,
          true
        )
        .then((x) => {
          if (!x.status) {
            this.$refs.myformupdateReport.reset();
            return this.$message(x.message);
          }
          this.$refs.myformupdateReport.reset();
          this.viewupdateModelReport = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();

          return this.$message("修改成功");
        });
    },
    addReports() {
      if (!this.$refs.myformReport.validate()) {
        return;
      }
      this.formFiledsReport.parentCustormCatalogId = this.pidCatalogItems;
      //   this.formFiledsReport.createDate = this.getDateStr();
      //console.log(this.formFiledsReport);

      this.http
        .post(
          "/api/PBI_CustormCatalogItems/AddCustormReport",
          this.formFiledsReport,
          true
        )
        .then((x) => {
          if (!x.status) {
            this.$refs.myformReport.reset();
            return this.$message(x.message);
          }
          this.$refs.myformReport.reset();
          this.viewModelReport = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();

          return this.$message("添加成功");
        });
    },
    addReportMainCatalog() {
      this.pidCatalogItems = 0;
      this.addCatalogTile = "添加主目录";
      this.$refs.myformCatalog.reset();
      this.viewModelCatalog = true;
    },
    addReportCatalog() {
      this.addCatalogTile = this.selectGroupName + "--添加子目录";
      this.$refs.myformCatalog.reset();
      this.viewModelCatalog = true;
    },

    UpdateReportCatalog() {
      this.addCatalogTile = "修改目录：" + this.selectGroupName;
      this.viewUpdateModelCatalog = true;
    },

    UpdateCatalog() {
      //  //console.log(this.pidCatalogItems);
      if (!this.$refs.myformUpdateCatalog.validate()) {
        return;
      }
      // this.formFiledsCatalog.ParentCatalogId = this.pidCatalogItems;
      //   this.formFiledsReport.createDate = this.getDateStr();
      ////console.log(this.formFiledsCatalog);

      this.http
        .post("/api/PBI_Catalog/UpdatePBICatalog", this.formFiledsCatalog, true)
        .then((x) => {
          if (!x.status) {
            // this.$refs.myformUpdateCatalog.reset();
            return this.$message(x.message);
          }
          // this.$refs.myformUpdateCatalog.reset();
          this.viewUpdateModelCatalog = false;

          this.getPowerBIFoder("/");
          this.loadCatalogItems();

          return this.$message("修改成功");
        });
    },

    addCatalog() {
      //  //console.log(this.pidCatalogItems);
      if (!this.$refs.myformCatalog.validate()) {
        return;
      }
      this.formFiledsCatalog.CatalogId = "";
      this.formFiledsCatalog.CreatedDate = "";
      this.formFiledsCatalog.ParentCatalogId = this.pidCatalogItems;
      //   this.formFiledsReport.createDate = this.getDateStr();

      let adddata = {
        name: this.formFiledsCatalog.name,
        description: this.formFiledsCatalog.description,
        CreatedDate: this.getDateStr(),
        ParentCatalogId: this.formFiledsCatalog.ParentCatalogId,
        // reportid: "",
        sort: this.formFiledsCatalog.sort,
      };
      //console.log(this.formFiledsCatalog);

      this.http
        .post("/api/PBI_Catalog/AddCustormCatalog", adddata, true)
        .then((x) => {
          if (!x.status) {
            this.$refs.myformCatalog.reset();
            return this.$message(x.message);
          }
          this.$refs.myformCatalog.reset();
          this.viewModelCatalog = false;

          this.getPowerBIFoder("/");
          this.loadCatalogItems();

          return this.$message("添加成功");
        });
    },
    delReportCatalog() {
      //   this.$refs.tableCatalogItems
      if (this.$refs.tableCatalogItems.rowData.length > 0) {
        return this.$message("目录不为空，不可删除！");
      } else {
        let delKeys = [this.pidCatalogItems];
        // //console.log(delKeys);
        if (!delKeys || delKeys.length == 0)
          return this.$message.error("没有获取要删除的行数据!");

        let url = "/api/PBI_Catalog/Del";
        // let url = this.getUrl(this.const.DEL);
        this.http.post(url, delKeys, "正在删除数据....").then((x) => {
          if (!x.status) return this.$message.error(x.message);
          this.$message.error(x.message);
          //删除后ssss
          //this.refresh();
          // this.$refs.tableCatalogItems.load();
          this.getPowerBIFoder("/");
          this.selectGroupName = "为选择目录，请选择目录！";
          return this.$message("删除完成");
        });
      }
    },

    getDateStr() {
      var date = new Date();
      //年
      var year = date.getFullYear();
      //月
      var month = date.getMonth() + 1;
      //日
      var day = date.getDate();
      //时
      var hh = date.getHours();
      //分
      var mm = date.getMinutes();
      //秒
      var ss = date.getSeconds();
      var rq = year + "-" + month + "-" + day + " " + hh + ":" + mm + ":" + ss;
      return rq;
    },
    addGroupUser() {
      let rows = this.$refs.tableUser.getSelected();

      if (rows.length == 0) {
        return this.$message.error("未选择用户");
      }
      //  this.text = "当前选中的行数据：" + JSON.stringify(rows);
      // this.viewModel = true;
      // return rows;

      let groupuser = [];

      rows.forEach((x) => {
        let tempuser = {
          USER_LOGIN_NAME: x.USER_LOGIN_NAME,
          USER_ID: x.USER_ID,
          USER_TrueName: x.USER_TrueName,
          Group_ID: this.pid,
          CreateDate: this.getDateStr(),
        };
        groupuser.push(tempuser);
      });
      //console.log(groupuser);
      this.http
        .post("/api/PBI_Group_User/InsertUser", groupuser, "....")
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
        });

      this.viewModel = false;
      this.$refs.table.load();
    },
    _linkViewCatalogItems(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeCatalogItems(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        {
          name: "ParentCustormCatalogId",
          value: this.pidCatalogItems,
          displayType: "int",
        },
      ];
      //  //console.log(param);
      // //console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterCatalogItems(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // //console.log(data);
      // //console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadCatalogItems() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      //console.log(this.pidCatalogItems);
      let where = {
        wheres: [
          {
            name: "ParentCustormCatalogId",
            value: this.pidCatalogItems,
            displayType: "int",
          },
        ],
      };
      this.$refs.tableCatalogItems.load(where);
    },

    load() {
      let where = {
        wheres: [{ ParentCustormCatalogId: this.pidCatalogItems }],
      };
      this.$refs.tableCatalogItems.load(where);
    },

    ///分页代码
    handleSizeChange(val) {
      this.paginations.rows = val;
      this.load();
    },
    handleCurrentChange(val) {
      this.paginations.page = val;
      this.load();
    },
    sortChange(sort) {
      this.paginations.sort = sort.prop;
      this.paginations.order = sort.order == "ascending" ? "asc" : "desc";
      this.load();
    },
    resetPage() {
      //重置查询分页
      this.paginations.rows = 30;
      this.paginations.page = 1;
    },
    selectionChange(selection) {
      // //console.log(selection);
      //选择行事件,只有单选才触发
      if (this.single && selection.length == 1) {
        this.$emit("rowChange", selection[0]); //
      }
      if (this.single && selection.length > 1) {
        this.$refs.table.toggleRowSelection(selection[0]);
      }
      // this.rowChange(selection[0]);
    },
    ///分页

    setListCheckUser: function (idx) {
      var check = this.list[idx].check;
      this.list[idx].check = check === true ? false : true;
    },
    filterNodeCatalog(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClick(data) {
      ////console.log(data);
    },

    filterNodeUser(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClickUser(data) {
      ////console.log(data);
    },

    showTime() {
      var week = new Array(
        "星期一",
        "星期二",
        "星期三",
        "星期四",
        "星期五",
        "星期六",
        "星期日"
      );
      var date = new Date();
      var year = date.getFullYear();
      var month = date.getMonth() + 1;
      var day = date.getDate();
      var hour = date.getHours();
      var minutes = date.getMinutes();
      var second = date.getSeconds();
      this.date =
        year +
        "." +
        (month < 10 ? "0" + month : month) +
        "." +
        day +
        "" +
        " " +
        hour +
        ":" +
        minutes +
        ":" +
        (second < 10 ? "0" + second : second) +
        " " +
        (week[date.getDay() - 1] || "");
    },

    appendCatalog(data) {
      //  //console.log(data);
      this.selectGroupName = data.label;
      document.getElementById("ContentText").style.display = "none";
      document.getElementById("Content").style.display = "block";
      var pathstr = data.path;

      this.formFiledsCatalog.CatalogId = data.id;
      this.formFiledsCatalog.name = data.label;
      this.formFiledsCatalog.description = data.description;
      this.formFiledsCatalog.sort = data.sort;
      this.formFiledsCatalog.ParentCatalogId = data.pid;

      //console.log(data.id);
      let treedata = [];
      let selectoption = [];
      this.http
        .post(
          "/api/PBI_Catalog/GetPBICatalog",
          {
            path: "",
            id: data.id,
          },
          "获取数据...."
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
          //    //console.log(result.data.length);
          let m = 0;
          if (result.data.length == 0) {
            tempoption = {
              text: "请选择部门",
              key: "",
              icon: "md-add",
            };
            this.groupoptions.push(tempoption);
          }
          result.data.forEach((x) => {
            let tempdata = [];

            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              icon: "ios-folder-outline",
              description: x.description,
              sort: x.sort,
              children: [],
            };

            let appchindrened = false;
            data.children.forEach((m) => {
              if (m.id == x.catalogId) {
                appchindrened = true;
              }
            });
            if (!appchindrened) {
              data.children.push(tempdata);
            }

            m++;
          });
        });

      this.pidCatalogItems = data.id;

      this.loadCatalogItems();

      //  //console.log(data);
    },
    getPowerBIFoder(path) {
      let catalogdatatemp = [];
      this.http
        .post(
          "/api/PBI_Catalog/GetPBICatalog",
          {
            path: "",
            id: 0,
          },
          "获取数据...."
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
          ////console.log(result.data);
          result.data.forEach((x) => {
            let tempdata = [];
            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              icon: "ios-folder-outline",
              description: x.description,
              sort: x.sort,
              children: [],
            };
            catalogdatatemp.push(tempdata);
          });
          //  //console.log(treedata);
          this.catalogdata = catalogdatatemp;
        });
    },
  },
};
</script>
<style lang="less" scoped>
.menu-container {
  display: flex;
  position: fixed;
  width: 99%;
  height: 100%;
  margin-right: 0px;
  border: 0px solid #eee;
  border-radius: 5px;

  .menu-left {
    width: 15%;
    border: 1px solid #eee;
    margin-bottom: 50px;
    .module-name {
      border-radius: 0px;
      /* height: 5%; */
      line-height: 21px;
      margin-bottom: 0px;
    }
  }
  .rightAll {
    border: 1px solid #eee;
    margin-right: 180px;
    padding: 5px;
  }
  .menu-right {
    flex: 1;
    border: 2px solid #eee;
    width: 85%;
    overflow: scroll;

    .contentTable {
      width: 100%;
      height: 100%;
      border: 3px;
      display: none;
    }
    .head {
      width: 100%;
      height: 80px;
      background-color: skyblue;
      font-size: x-large;
    }
    .content {
      //   position: absolute;
      width: 100%;
      height: 100%;
    }
    // padding: 0 100px;
    .form-content {
      border: 2px solid #eee;
      margin-top: 2px;
      width: 100%;
      padding: 25px;
      box-shadow: rgb(214, 214, 214) 0px 4px 21px;
    }
  }
}
.align-center-vertical {
  height: 600px;
  display: flex;
  align-items: center;
  justify-content: space-around;
  flex-direction: column;
  font-size: 50px;
}
.project-name {
  font-size: 50px;
  position: relative;
  color: white;
  font-weight: 600;
  margin-left: 9px;
}
.el-tree {
  width: 100%;
  overflow: scroll;
  overflow-x: hidden;
  height: 95%;
}

.el-tree > .el-tree-node {
  display: inline-block;
  min-width: 100%;
}
</style>
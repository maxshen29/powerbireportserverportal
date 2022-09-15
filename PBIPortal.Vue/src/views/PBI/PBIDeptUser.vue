<template>
  <div class="menu-container">
    <div class="menu-left">
      <div class="menu-tree" style="height:95%;margin-bottom:100px">
        <el-input placeholder="搜索部门" v-model="filterTextUser"></el-input>
        <Icon type="md-add"></Icon>
        <span>部门和用户管理</span>
        <el-tree
          class="filter-tree"
          :data="Userdata"
          node-key="id"
          default-expand-all
          @node-click="appendUser"
          :filter-node-method="filterNodeUser"
          ref="treeUser"
        >
          <div class="action-group" slot-scope="{node, data}">
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
          <span class="project-name">Power BI 部门和用户管理</span>
        </div>
        <div class="align-center-vertical" id="ContentText">Power BI部门和用户管理</div>
        <div class="contentTable" id="Content">
          <VolHeader icon="md-apps" :text="selectGroupName" style="margin-bottom: 20px;">
            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;text-align: right">
              <Button type="info" ghost @click="resetpwd">重置密码</Button>
            </div>
          </VolHeader>
          <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;">
            <vol-table
              ref="tableUser"
              :loadKey="true"
              :linkView="_linkViewUser"
              :columns="columnsUser"
              :pagination="paginationUser"
              :height="700"
              :url="urlUser"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeUser"
              @loadAfter="loadTableAfterUser"
            ></vol-table>
          </div>
          <div class="block pagination" v-if="!paginationHide">
            <el-pagination
              @size-change="handleSizeChange"
              @current-change="handleCurrentChange"
              :current-page="pagination.page"
              :page-sizes="[30, 60, 100,300,500]"
              :page-size="pagination.size"
              layout="total, sizes, prev, pager, next, jumper"
              :total="pagination.total"
            ></el-pagination>
          </div>

          <vol-box :model.sync="viewModel" :height="250" :width="600" :title="resetUserTile">
            <div style="padding:10px;20px;">
              <VolForm ref="pwd" :formRules="modifyOptions.data" :formFileds="modifyOptions.fileds"></VolForm>
              <Button type="info" icon="md-checkmark-circle" long @click="savePwd">保存</Button>
            </div>
            <div slot="footer">
              <span></span>
            </div>
          </vol-box>
        </div>
      </div>
    </div>
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
    filterTextUser(val) {
      this.$refs.treeUser.filter(val);
    }
  },

  created() {
    this.getPowerBIFoder("/");
    this.getPowerBIDept("/");

    // console.log(this.logo);
  },
  data() {
    const tdata = [];
    const Userdata = [];

    return {
      selectGroupName: "",
      loadKey: true,
      formFileds: {
        Group_Name: "",
        Group_Level: "1",
        Group_PID: "0",
        CreateDate: this.getDateStr()
      },
      formRules: [
        [
          //两列的表单，formRules数据格式为:[[{},{}]]

          {
            title: "用户组名称",
            required: true, //设置为必选项
            field: "Group_Name",
            type: "text"
          }
        ]
      ],
      modifyOptions: {
        fileds: { oldPwd: "", newPwd: "", newPwd1: "" },
        data: [
          [
            {
              type: "password",
              required: true,
              title: "新密码",
              field: "newPwd"
            }
          ],
          [
            {
              type: "password",
              required: true,
              title: "确认密码",
              field: "newPwd1"
            }
          ]
        ]
      },
      log: imgUrl,
      pid: 0,
      pidUser: 0,
      groupid: 0,
      date: "",
      selectId: 0,
      links: [{ text: "安全退出", path: "/login", id: -4 }],
      userName: "--",
      token: "--",
      reportUrl: "--",
      filterText: "",
      filterTextUser: "",
      fromdisplay: "display:none;",
      titledisplay: "display:block",
      tdata: JSON.parse(JSON.stringify(tdata)),
      Userdata: JSON.parse(JSON.stringify(Userdata)),
      logo: imgUrl,
      defaultProps: {
        children: "children",
        label: "label"
      },
      text: "",
      viewModel: false, //点击单元格时弹出框
      viewModelGroup: false,
      viewModelDelGroup: false,
      resetUserTile: "",
      resetUserName: "",

      url: "api/V_group_user/getPageData", //后从加载数据的url
      urlUser: "api/PBI_SSO_USER/getPageData", //后从加载数据的url
      urlGroup: "api/PBI_SSO_Group/getPageData", //后从加载数据的url

      paginationHide: {
        type: Boolean,
        default: false
      },

      columns: [
        //表配置
        {
          field: "vid", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "string", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left" //文字显示位置
        },
        {
          field: "name",
          title: "显示名",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "truename",
          title: "真实名",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "type",
          title: "类型",
          type: "string",
          width: 50,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "CreateDate",
          title: "创建日期",
          type: "datetime",
          width: 50,
          align: "left"
        }
      ],
      columnsUser: [
        //表配置
        {
          field: "id", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "string", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left" //文字显示位置
        },
        {
          field: "USER_LOGIN_NAME",
          title: "登录名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "USER_TrueName",
          title: "真实名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        }
      ],

      columnsGroup: [
        //表配置
        {
          field: "Group_Id", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "string", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left" //文字显示位置
        },
        {
          field: "Group_Name",
          title: "登录名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "CreateDate",
          title: "真实名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        }
      ],

      pagination: {
        sortName: "CreateDate", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",

        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
      },
      paginationUser: {
        sortName: "USER_LOGIN_NAME", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",

        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
      },
      paginationGroup: {
        sortName: "Group_ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",

        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
      }
    };
  },
  methods: {
    resetpwd() {
      this.viewModel = false;

      let rows = this.$refs.tableUser.getSelected();
      if (rows.length == 0)
        return this.$message.error("请选择要重置密码的用户！");
      if (rows.length > 1) return this.$message.error("请只选择有个用户！");
      // console.log(rows);
      let USER_LOGIN_NAME = rows[0].USER_LOGIN_NAME;
      this.resetUserTile = "重置：" + USER_LOGIN_NAME + "密码";
      this.resetUserName = USER_LOGIN_NAME;
      this.viewModel = true;
    },
    DelGroups() {
      this.viewModelDelGroup = false;

      let rows = this.$refs.tableGroup.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      // console.log(rows);
      let delKeys = rows.map(x => {
        return x.Group_Id;
      });

      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_SSO_Group/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, "正在删除数据....").then(x => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        // this.refresh();
        // this.$refs.tableGroup.load();
        this.getPowerBIFoder("/");
      });
    },
    savePwd() {
      if (
        this.modifyOptions.fileds.newPwd != this.modifyOptions.fileds.newPwd1
      ) {
        return this.$message.error("两次密码不一致");
      }
      let postdata = {
        USER_LOGIN_NAME: this.resetUserName,
        UserPwd: this.modifyOptions.fileds.newPwd
      };
      let url = "/api/PBI_SSO_USER/ResetUserPwd";

      this.http.post(url, postdata, true).then(x => {
        if (!x.status) {
          return this.$message.error(x.message);
        }
        this.viewModel = false;
        this.$Message.success(x.message);
        this.$refs.pwd.reset();
      });
    },
    delGroup() {
      this.loadGroup();
      this.viewModelDelGroup = true;
    },
    addGroups() {
      if (!this.$refs.myform.validate()) {
        return;
      }

      console.log(this.formFileds);
      // this.$message.error(JSON.stringify(this.formFileds));

      this.http
        .post("/api/PBI_SSO_Group/AddGroup", this.formFileds, true)
        .then(x => {
          if (!x.status) {
            return this.$message(x.message);
          }
          this.$refs.myform.reset();
          this.viewModelGroup = false;
          this.getPowerBIFoder("/");
          return this.$message("添加成功");
        });
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

      rows.forEach(x => {
        let tempuser = {
          USER_LOGIN_NAME: x.USER_LOGIN_NAME,
          USER_ID: x.USER_ID,
          USER_TrueName: x.USER_TrueName,
          Group_ID: this.pid,
          CreateDate: this.getDateStr()
        };
        groupuser.push(tempuser);
      });
      console.log(groupuser);
      this.http
        .post("/api/PBI_Group_User/InsertUser", groupuser, "....")
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
        });

      this.viewModel = false;
      this.$refs.table.load();
    },
    _linkViewUser(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeUser(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" }
      ];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterUser(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadUser() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      let where = {
        wheres: [
          { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" }
        ]
      };
      this.$refs.tableUser.load(where);
    },

    ///————————————————————————-
    /////---Group处理---
    ///————————————————————-
    _linkViewGroup(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeGroup(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [{ Group_PID: "0" }];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterGroup(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadGroup() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      let where = [{ Group_PID: "0" }];
      this.$refs.tableGroup.load(where);
    },

    ///————————————————————————-
    /////---Group处理---
    ///————————————————————-
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
      // console.log(selection);
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

    _linkView(row, column) {
      this.text =
        "点击单元格的弹出框，当前点击的行数据：" + JSON.stringify(row);
      this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBefore(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [{ pid: this.pid }];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfter(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    load() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      let where = [];
      this.$refs.table.load(where);
    },
    del() {
      let rows = this.$refs.table.getSelected();
      if (rows.length == 0) {
        return this.$message.error("请先选中行");
      }
      this.$refs.table.delRow();
      //此处可以接着写删除后台的代码
    },
    getRows() {
      let rows = this.$refs.table.getSelected();
      // console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请先选中行1");
      }
      this.text = "当前选中的行数据：" + JSON.stringify(rows);
      this.viewModel = true;
      return rows;
    },
    setListCheck: function(idx) {
      var check = this.list[idx].check;
      this.list[idx].check = check === true ? false : true;
    },

    getRowsUser() {
      let rows = this.$refs.tableUser.getSelected();
      // console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请先选中行1");
      }
      this.text = "当前选中的行数据：" + JSON.stringify(rows);
      this.viewModel = true;
      return rows;
    },

    delUser() {
      //删除数据
      let rows = this.$refs.table.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      // console.log(rows);
      let delKeys = rows.map(x => {
        return x.vid;
      });

      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_Group_User/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, "正在删除数据....").then(x => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.table.load();
      });
    },

    addUser() {
      if (this.selectGroupName == "") {
        this.$message.error("请选择左侧用户组");
        return;
      }
      this.viewModel = true;
    },

    addGroup() {
      this.viewModelGroup = true;
    },
    setListCheckUser: function(idx) {
      var check = this.list[idx].check;
      this.list[idx].check = check === true ? false : true;
    },
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClick(data) {
      //console.log(data);
    },

    filterNodeUser(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClickUser(data) {
      //console.log(data);
    },

    to(item) {
      if (item.path == "/login") {
        this.$store.commit("clearUserInfo", "");
      }
      this.$router.push({
        path: item.path
      });
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
    append_pid(data) {
      var pathstr = data.path;
      // console.log(data);
      let treedata = [];
      let selectoption = [];
      this.http
        .post(
          "/api/PBI_SSO_Group/GetPBICatalogItems",
          {
            path: pathstr,
            id: data.id
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          // console.log(result.data.length);
          let m = 0;
          if (result.data.length == 0) {
            tempoption = {
              text: "请选择组",
              key: "",
              icon: "md-add"
            };
            this.groupoptions.push(tempoption);
          }
          result.data.forEach(x => {
            let tempdata = [];
            let tempoption = [];
            tempdata = {
              id: x.group_Id,
              label: x.group_Name,
              pid: x.group_PID,
              icon: "ios-folder-outline",
              children: []
            };

            tempoption = {
              text: x.group_Name,
              key: x.group_Id,
              icon: "ios-folder-outline"
            };

            let appchindrened = false;
            data.children.forEach(m => {
              if (m.id == x.group_Id) {
                appchindrened = true;
              }
            });
            if (!appchindrened) {
              data.children.push(tempdata);
              this.groupoptions.push(tempoption);
            }

            m++;
          });
        });

      this.pid = data.id;
      this.load();

      //  console.log(data);
    },
    append(data) {
      this.pid = data.id;
      this.selectGroupName = data.label;

      document.getElementById("ContentText").style.display = "none";
      document.getElementById("Content").style.display = "block";
      this.load();

      //  console.log(data);
    },

    appendUser(data) {
      this.selectGroupName = data.label;
      document.getElementById("ContentText").style.display = "none";
      document.getElementById("Content").style.display = "block";
      var pathstr = data.path;

      //   console.log(data);
      let treedata = [];
      let selectoption = [];
      this.http
        .post(
          "/api/PBI_SSO_DEPT/GetPBICatalogItems",
          {
            path: pathstr,
            id: data.code
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          //   console.log(result.data.length);
          let m = 0;
          if (result.data.length == 0) {
            tempoption = {
              text: "请选择部门",
              key: "",
              icon: "md-add"
            };
            this.groupoptions.push(tempoption);
          }
          result.data.forEach(x => {
            let tempdata = [];

            tempdata = {
              id: x.depT_CODE,
              label: x.depT_NAME,
              code: x.depT_CODE,
              pcode: x.depT_PCODE,
              icon: "ios-folder-outline",
              children: []
            };

            let appchindrened = false;
            data.children.forEach(m => {
              if (m.id == x.depT_CODE) {
                appchindrened = true;
              }
            });
            if (!appchindrened) {
              data.children.push(tempdata);
            }

            m++;
          });
        });

      this.pidUser = data.code;
      this.loadUser();
      //  console.log(data);
    },
    getPowerBIFoder(path) {
      let userInfo = this.$store.getters.getUserInfo();
      this.userName = userInfo.userTrueName;
      let treedata = [];
      this.http
        .post(
          "/api/PBI_SSO_Group/GetPBICatalogItems",
          {
            path: path,
            id: "0"
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          //console.log(result.data);
          result.data.forEach(x => {
            let tempdata = [];
            tempdata = {
              id: x.group_Id,
              label: x.group_Name,
              pid: x.group_PID,
              icon: "ios-folder-outline",
              children: []
            };
            treedata.push(tempdata);
          });
          //  console.log(treedata);
          this.tdata = treedata;
        });
    },

    getPowerBIDept(path) {
      let treeUserdata = [];
      this.http
        .post(
          "/api/PBI_SSO_DEPT/GetPBICatalogItems",
          {
            path: path,
            id: "0"
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          // console.log(result.data);

          result.data.forEach(x => {
            let tempdata = [];
            tempdata = {
              id: x.depT_CODE,
              label: x.depT_NAME,
              code: x.depT_CODE,
              pcode: x.depT_PCODE,
              icon: "ios-folder-outline",
              children: []
            };
            treeUserdata.push(tempdata);
          });
          //  console.log(treedata);
          this.Userdata = treeUserdata;
        });
    }
  }
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
    width: 100%;
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
      // position: absolute;
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
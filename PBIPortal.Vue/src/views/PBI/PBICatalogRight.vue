<template>
  <div class="menu-container">
    <div class="menu-left">
      <div class="menu-tree" style="height:95%;margin-bottom:100px">
        <el-input placeholder="搜索目录" v-model="filterTextCatalog"></el-input>
        <Icon type="md-add"></Icon>
        <span>报表目录</span>
        <span style="cursor:pointer" @click="flashCatalog">【刷新目录】</span>
        <el-tree
          class="filter-tree"
          :data="catalogdata"
          node-key="id"
          default-expand-all
          @node-click="appendCatalog"
          :filter-node-method="filterNodeCatalog"
          ref="treeCatalog"
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
          <span class="project-name">Power BI 目录权限管理</span>
        </div>
        <div class="align-center-vertical" id="ContentText">Power BI 目录权限管理</div>
        <div class="contentTable" id="Content">
          <VolHeader icon="md-apps" :text="selectGroupName" style="margin-bottom: 20px;">
            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;text-align: right">
              <Button type="info" ghost @click="addReportRight">添加组权限</Button>
              <Button type="info" ghost @click="delReportRight">删除组权限</Button>
            </div>
          </VolHeader>
          <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;">
            <vol-table
              ref="tableCatalogItems"
              :loadKey="true"
              :linkView="_linkViewCatalogItems"
              :columns="columnsCatalogItems"
              :pagination="paginationCatalogItems"
              :height="300"
              :url="urlCatalogItems"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeCatalogItems"
              @loadAfter="loadTableAfterCatalogItems"
            ></vol-table>
          </div>
          <VolHeader icon="md-apps" :text="selectUserName" style="margin-bottom: 20px;">
            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;text-align: right">
              <Button type="info" ghost @click="addReportUserRight">添加用户权限</Button>
              <Button type="info" ghost @click="delReportUserRight">删除用户权限</Button>
            </div>
          </VolHeader>
          <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;">
            <vol-table
              ref="tableUser"
              :loadKey="true"
              :linkView="_linkViewUser"
              :columns="columnsUser"
              :pagination="paginationUser"
              :height="300"
              :url="urlUser"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeUser"
              @loadAfter="loadTableAfterUser"
            ></vol-table>
          </div>
        </div>
      </div>
    </div>
    <vol-box :model.sync="viewModelReport" :height="600" :width="600" :title="addreportTile">
      <vol-table
        ref="tableGroup"
        :loadKey="true"
        :linkView="_linkViewGroup"
        :columns="columnsGroup"
        :pagination="paginationGroup"
        :height="500"
        :url="urlGroup"
        :index="true"
        :paginationHide="false"
        @loadBefore="loadTableBeforeGroup"
        @loadAfter="loadTableAfterGroup"
      ></vol-table>

      <div slot="footer">
        <Button type="info" @click="addReports">确认</Button>
        <Button type="info" @click="viewModelReport=false">取消</Button>
      </div>
    </vol-box>

    <vol-box :model.sync="viewModel" :height="800" :width="600" :title="selectGroupName">
      <div style="height:700px">
        <div style="height:440px;" scrolling="yes">
          <div class="menu-tree" style="height:400px;  scrolling:yes">
            <el-input placeholder="搜索部门" v-model="filterTextUser"></el-input>
            <Icon type="md-add"></Icon>
            <span>部门</span>
            <el-tree
              class="filter-tree"
              :data="Userdata"
              height:300
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
        <div class="content">
          <vol-table
            ref="tableDeptUser"
            :loadKey="true"
            :linkView="_linkViewDeptUser"
            :columns="columnsDeptUser"
            :pagination="paginationDeptUser"
            :height="300"
            :url="urlDeptUser"
            :index="true"
            :paginationHide="false"
            @loadBefore="loadTableBeforeDeptUser"
            @loadAfter="loadTableAfterDeptUser"
          ></vol-table>
        </div>
      </div>

      <!--  <div slot="footer">这里可以自己扩展box，也可不用写 -->
      <div slot="footer">
        <Button type="info" @click="addUserRight">添加用户权限</Button>
        <Button type="info" @click="viewModel=false">取消</Button>
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
    filterTextUser(val) {
      this.$refs.treeUser.filter(val);
    }
  },

  created() {
    this.getPowerBIFoder("/");

    // console.log(this.logo);
  },
  data() {
    const tdata = [];
    const catalogdata = [];
    const Userdata = [];

    return {
      selectGroupName: "",
      selectUserName: "",
      loadKey: true,
      addreportTile: "",
      addCatalogTile: "",
      formFiledsCatalog: {
        name: "",
        description: "",
        CreatedDate: this.getDateStr(),
        ParentCatalogId: this.pidCatalogItems,
        // reportid: "",
        sort: 0
      },
      formRulesCatalog: [
        [
          //两列的表单，formRules数据格式为:[[{},{}]]

          {
            title: "报表目录名称",
            required: true, //设置为必选项
            field: "name",
            type: "text"
          }
        ],
        [
          {
            title: "报表目录说明",
            required: true, //设置为必选项
            field: "description",
            type: "text"
          }
        ]
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
      filterTextUser: "",
      fromdisplay: "display:none;",
      titledisplay: "display:block",
      tdata: JSON.parse(JSON.stringify(tdata)),
      catalogdata: JSON.parse(JSON.stringify(catalogdata)),
      Userdata: JSON.parse(JSON.stringify(Userdata)),
      logo: imgUrl,
      defaultProps: {
        children: "children",
        label: "label"
      },
      text: "",

      viewModelReport: false,
      viewModelCatalog: false,
      viewModel: false,

      urlCatalogItems: "api/PBI_Catalog_Right_Group/getPageData", //后从加载数据的url
      urlGroup: "api/PBI_SSO_Group/getPageData", //后从加载数据的url
      urlUser: "api/PBI_Catalog_Right_User/getPageData", //后从加载数据的url
      urlDeptUser: "api/PBI_SSO_USER/getPageData", //后从加载数据的url

      paginationHide: {
        type: Boolean,
        default: false
      },

      columnsDeptUser: [
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

      paginationDeptUser: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
      },
      columnsUser: [
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
          title: "用户名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        }
      ],

      paginationUser: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
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
          align: "left" //文字显示位置
        },
        {
          field: "Group_Name",
          title: "组名称",
          type: "string",
          width: 200,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        }
      ],

      paginationCatalogItems: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30
      },

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
          title: "组名称",
          type: "string",
          width: 200,
          hidden: false, //是否显示
          align: "left",
          sort: true //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        }
      ],
      paginationGroup: {
        sortName: "Group_Id", //从后加载数据分页时的排序字段
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
    ///添加用户表权限

    flashCatalog() {
      this.getPowerBIFoder("/");
    },

    _linkViewDeptUser(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeDeptUser(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" }
      ];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterDeptUser(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadDeptUser() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};

      let where = {
        wheres: [
          { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" }
        ]
      };
      this.$refs.tableDeptUser.load(where);
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
    },
    setReportRight() {
      // alert(this.pidCatalogItems);

      var postdata = {
        path: "",
        id: this.pidCatalogItems
      };
      this.http
        .post("/api/V_GetALLUserRightbyRptid/SetReportRight", postdata, true)
        .then(x => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
          return this.$message.success("报表服务器报表权限设置成功！");
        });
    },
    addUserRight() {
      let rows = this.$refs.tableDeptUser.getSelected();
      console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请选择需要添加的组！");
      }

      let postdata = [];
      rows.forEach(x => {
        let tempdata = {
          CatalogId: this.pidCatalogItems,
          USER_ID: x.USER_ID,
          USER_TrueName: x.USER_TrueName,
          USER_LOGIN_NAME: x.USER_LOGIN_NAME,
          CreateDate: this.getDateStr
        };
        postdata.push(tempdata);
      });

      this.http
        .post("/api/PBI_Catalog_Right_User/InsertUserRight", postdata, true)
        .then(x => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
          this.viewModel = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();
          this.loadUser();
          return this.$message.success("添加成功");
        });
    },
    appendUser(data) {
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
      this.loadDeptUser();
      //  console.log(data);
    },

    addReportUserRight() {
      this.getPowerBIDept("/");
      this.pidUser = "-1";
      this.viewModel = true;
      this.loadDeptUser();
    },
    delReportUserRight() {
      let rows = this.$refs.tableUser.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      console.log(rows);
      let delKeys = rows.map(x => {
        return x.Id;
      });
      // console.log(delKeys);
      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_Catalog_Right_User/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, "正在删除数据....").then(x => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.tableUser.load();
        return this.$message.success("删除完成");
      });
    },
    ///添加用户表权限
    delReportRight() {
      let rows = this.$refs.tableCatalogItems.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      console.log(rows);
      let delKeys = rows.map(x => {
        return x.Id;
      });
      // console.log(delKeys);
      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_Catalog_Right_Group/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, "正在删除数据....").then(x => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.tableCatalogItems.load();
        return this.$message.success("删除完成");
      });
    },
    addReportRight() {
      this.addreportTile = this.selectGroupName + "--添加组权限";
      this.viewModelReport = true;
      this.loadGroup();
    },
    addReports() {
      //console.log(this.formFiledsReport);

      let rows = this.$refs.tableGroup.getSelected();
      console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请选择需要添加的组！");
      }

      let postdata = [];
      rows.forEach(x => {
        let tempdata = {
          CatalogId: this.pidCatalogItems,
          Group_ID: x.Group_Id,
          Group_Name: x.Group_Name,
          CreateDate: this.getDateStr
        };
        postdata.push(tempdata);
      });

      this.http
        .post("/api/PBI_Catalog_Right_Group/InsertGroupRight", postdata, true)
        .then(x => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
          this.viewModelReport = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();
          return this.$message.success("添加成功");
        });
    },
    addReportCatalog() {
      this.addCatalogTile = this.selectGroupName + "--添加目录";
      this.viewModelCatalog = true;
    },
    addCatalog() {
      console.log(this.pidCatalogItems);
      if (!this.$refs.myformCatalog.validate()) {
        return;
      }
      this.formFiledsCatalog.ParentCatalogId = this.pidCatalogItems;
      //   this.formFiledsReport.createDate = this.getDateStr();
      // console.log(this.formFiledsReport);

      this.http
        .post(
          "/api/PBI_Catalog/AddCustormCatalog",
          this.formFiledsCatalog,
          true
        )
        .then(x => {
          if (!x.status) {
            this.$refs.myformCatalog.reset();
            return this.$message.success(x.message);
          }
          this.$refs.myformCatalog.reset();
          this.viewModelCatalog = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();

          return this.$message.success("添加成功");
        });
    },
    delReportCatalog() {
      //   this.$refs.tableCatalogItems
      if (this.$refs.tableCatalogItems.rowData.length > 0) {
        return this.$message.success("目录不为空，不可删除");
      } else {
        let delKeys = [this.pidCatalogItems];
        // console.log(delKeys);
        if (!delKeys || delKeys.length == 0)
          return this.$message.error("没有获取要删除的行数据!");

        let url = "/api/PBI_Catalog/Del";
        // let url = this.getUrl(this.const.DEL);
        this.http.post(url, delKeys, "正在删除数据....").then(x => {
          if (!x.status) return this.$message.error(x.message);
          this.$message.error(x.message);
          //删除后ssss
          //this.refresh();
          // this.$refs.tableCatalogItems.load();
          this.getPowerBIFoder("/");
          return this.$message.success("删除完成");
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
      this.$refs.tableUser.load();
    },
    _linkViewCatalogItems(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeCatalogItems(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "CatalogId", value: this.pidCatalogItems, displayType: "text" }
      ];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterCatalogItems(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadCatalogItems() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      console.log(this.pidCatalogItems);
      let where = {
        wheres: [
          {
            name: "CatalogId",
            value: this.pidCatalogItems,
            displayType: "text"
          }
        ]
      };
      this.$refs.tableCatalogItems.load(where);
    },

    //group
    _linkViewGroup(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeGroup(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [{ name: "Group_PID", value: 0, displayType: "int" }];
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
      // console.log(this.pidCatalogItems);
      let where = {
        wheres: [{ name: "Group_PID", value: 0, displayType: "int" }]
      };
      this.$refs.tableGroup.load(where);
    },

    //group

    //user
    _linkViewUser(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeUser(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "CatalogId", value: this.pidCatalogItems, displayType: "text" }
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
      // console.log(this.pidCatalogItems);
      let where = {
        wheres: [
          {
            name: "CatalogId",
            value: this.pidCatalogItems,
            displayType: "text"
          }
        ]
      };
      this.$refs.tableUser.load(where);
    },

    //user

    load() {
      let where = {
        wheres: [{ ParentCustormCatalogId: this.pidCatalogItems }]
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

    setListCheckUser: function(idx) {
      var check = this.list[idx].check;
      this.list[idx].check = check === true ? false : true;
    },
    filterNodeCatalog(value, data) {
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
      //  console.log(data);
      this.selectGroupName = data.label + "--组权限管理";
      this.selectUserName = data.label + "--用户权限管理";

      this.pidCatalogItems = data.id;

      document.getElementById("ContentText").style.display = "none";
      document.getElementById("Content").style.display = "block";

      var pathstr = data.path;

      //  console.log(data.id);
      let treedata = [];
      let selectoption = [];
      if (data.type == "folder") {
        this.http
          .post(
            "/api/PBI_Catalog/GetPBICatalog",
            {
              path: "",
              id: data.id
            },
            "获取数据...."
          )
          .then(result => {
            if (!result.status) return this.$Message.error(result.message);
            //    console.log(result.data.length);

            result.data.forEach(x => {
              let tempdata = [];

              tempdata = {
                id: x.catalogId,
                label: x.name,
                pid: x.parentCatalogId,
                type: "folder",
                icon: "ios-folder-outline",
                children: []
              };

              let appchindrened = false;
              data.children.forEach(m => {
                if (m.id == x.catalogId) {
                  appchindrened = true;
                }
              });
              if (!appchindrened) {
                data.children.push(tempdata);
              }
            });
          });

        //  this.getreport(data);
      }

      //console.log(data.type);

      this.loadCatalogItems();
      this.loadUser();
      //  console.log(data);
    },

    getreport(data) {
      this.http
        .post(
          "/api/PBI_CustormCatalogItems/GetPBICatalog",
          {
            path: "",
            id: data.id
          },
          "获取报表数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message); /*  */
          //    console.log(result.data.length);
          result.data.forEach(x => {
            let tempReportdata = [];
            let reportid = "p-" + x.id;

            tempReportdata = {
              id: reportid,
              label: x.name,
              pid: x.parentCustormCatalogId,
              type: "report",
              icon: "md-stats",
              children: []
            };

            let appreport = false;
            data.children.forEach(m => {
              if (m.id == reportid) {
                appreport = true;
              }
            });
            if (!appreport) {
              data.children.push(tempReportdata);
            }

            ///添加报表
          });
        });

      //  this.pidCatalogItems = data.id;
    },
    getPowerBIFoder(path) {
      let catalogdatatemp = [];
      this.http
        .post(
          "/api/PBI_Catalog/GetPBICatalog",
          {
            path: "",
            id: 0
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          //console.log(result.data);
          result.data.forEach(x => {
            let tempdata = [];
            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              type: "folder",
              icon: "ios-folder-outline",
              children: []
            };
            catalogdatatemp.push(tempdata);
          });
          //  console.log(treedata);
          this.catalogdata = catalogdatatemp;
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
      //  position: absolute;
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
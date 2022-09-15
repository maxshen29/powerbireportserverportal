 <template>
  <div class="menu-container">
    <div class="menu-left">
      <div class="menu-tree" style="height: 95%; margin-bottom: 100px">
        <el-input placeholder="搜索目录" v-model="filterTextCatalog"></el-input>
        <Icon type="md-add"></Icon>
        <span>报表目录</span>
        <span style="cursor: pointer" @click="flashCatalog">【刷新目录】</span>
        <span style="cursor: pointer" @click="setReportRole"
          >【设置所有报表行级权限】</span
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
          <span class="project-name">Power BI 报表角色管理</span>
        </div>
        <div class="align-center-vertical" id="ContentText">
          Power BI 报表角色管理
        </div>
        <div class="contentTable" id="Content">
          <VolHeader
            icon="md-apps"
            :text="selectRPTName"
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
              <Button type="info" ghost @click="addReportRole"
                >添加报表角色成员</Button
              >
              <Button type="info" ghost @click="delReportRole"
                >删除报表角色成员</Button
              >
              <Button type="info" ghost @click="SetReportRolePBI"
                >设置报表行级权限</Button
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
              :height="300"
              :url="urlCatalogItems"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeCatalogItems"
              @loadAfter="loadTableAfterCatalogItems"
            ></vol-table>
          </div>

          <VolHeader
            icon="md-apps"
            :text="selectRPTNameGroup"
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
              <Button type="info" ghost @click="addReportRoleGroup"
                >添加报表角色组成员</Button
              >
              <Button type="info" ghost @click="delReportRoleGroup"
                >删除报表角色组成员</Button
              >
            </div>
          </VolHeader>
          <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px">
            <vol-table
              ref="tableCatalogItemsGroup"
              :loadKey="true"
              :linkView="_linkViewCatalogItemsGroup"
              :columns="columnsCatalogItemsGroup"
              :pagination="paginationCatalogItemsGroup"
              :height="300"
              :url="urlCatalogItemsGroup"
              :index="true"
              :paginationHide="false"
              @loadBefore="loadTableBeforeCatalogItemsGroup"
              @loadAfter="loadTableAfterCatalogItemsGroup"
            ></vol-table>
          </div>
        </div>
      </div>
    </div>

    <vol-box
      :model.sync="viewModelReport"
      :height="600"
      :width="600"
      :title="addreportTile"
    >
      <vol-table
        ref="tableGroup"
        :loadKey="true"
        :linkView="_linkViewGroup"
        :columns="columnsGroup"
        :pagination="paginationGroup"
        :height="300"
        :url="urlGroup"
        :index="true"
        :paginationHide="false"
        @loadBefore="loadTableBeforeGroup"
        @loadAfter="loadTableAfterGroup"
      ></vol-table>

      <div>
        <br />
        <VolHeader icon="md-apps" text="选择需要加入的角色:"></VolHeader>
        <br />
        <el-checkbox-group v-model="checkedRoles" class="checkGroup">
          <el-checkbox
            v-for="item in PBIDataModelRole"
            :label="item.ModelRoleId"
            :key="item.ModelRoleId"
            >{{ item.ModelRoleName }}</el-checkbox
          >
        </el-checkbox-group>
      </div>
      <div slot="footer">
        <Button type="info" @click="addReportsGroupModelRole"
          >添加组权限</Button
        >
        <Button type="info" @click="viewModelReport = false">取消</Button>
      </div>
    </vol-box>

    <vol-box
      :model.sync="viewModel"
      :height="850"
      :width="600"
      :title="selectGroupName"
    >
      <div style="height: 700px">
        <div style="height: 400px" scrolling="yes">
          <div class="menu-tree" style="height: 360px; scrolling: yes">
            <el-input
              placeholder="搜索部门"
              v-model="filterTextUser"
            ></el-input>
            <Icon type="md-add"></Icon>
            <span>部门</span>
            <el-tree
              class="filter-tree"
              :data="Userdata"
              height:400
              node-key="id"
              default-expand-all
              @node-click="appendUser"
              :filter-node-method="filterNodeUser"
              ref="treeUser"
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
        <div class="content">
          <vol-table
            ref="tableDeptUser"
            :loadKey="true"
            :linkView="_linkViewDeptUser"
            :columns="columnsDeptUser"
            :pagination="paginationDeptUser"
            :height="280"
            :url="urlDeptUser"
            :index="true"
            :paginationHide="false"
            @loadBefore="loadTableBeforeDeptUser"
            @loadAfter="loadTableAfterDeptUser"
          ></vol-table>
        </div>
      </div>

      <div>
        <br />
        <VolHeader icon="md-apps" text="选择需要加入的角色:"></VolHeader>
        <br />
        <el-checkbox-group v-model="checkedRoles" class="checkGroup">
          <el-checkbox
            v-for="item in PBIDataModelRole"
            :label="item.ModelRoleId"
            :key="item.ModelRoleId"
            >{{ item.ModelRoleName }}</el-checkbox
          >
        </el-checkbox-group>
      </div>
      <!--  <div slot="footer">这里可以自己扩展box，也可不用写 -->
      <div slot="footer">
        <Button type="info" @click="addUserRoleRight">添加用户角色权限</Button>
        <Button type="info" @click="viewModel = false">取消</Button>
      </div>
    </vol-box>
  </div>
</template>
<script>
import VolHeader from "@/components/basic/VolHeader.vue";
import VolTable from "@/components/basic/VolTable.vue";
import VolBox from "@/components/basic/VolBox.vue";
export default {
  components: { VolTable, VolHeader, VolBox },
  watch: {
    filterTextCatalog(val) {
      this.$refs.treeCatalog.filter(val);
    },
  },
  filterTextUser(val) {
    this.$refs.treeUser.filter(val);
  },

  created() {
    this.getPowerBIFoder("/");

    // console.log(this.logo);
  },
  data() {
    const tdata = [];
    const catalogdata = [];
    const Userdata = [];
    const PBIDataModelRole = [];

    return {
      selectGroupName: "",
      selectUserName: "",
      loadKey: true,
      addreportTile: "",
      addCatalogTile: "",
      checkedRoles: [],
      selectReportData: [],

      catalogdata: JSON.parse(JSON.stringify(catalogdata)),
      PBIDataModelRole: JSON.parse(JSON.stringify(PBIDataModelRole)),
      Userdata: JSON.parse(JSON.stringify(Userdata)),
      PBIData: { path: "", id: "" },

      filterTextCatalog: "",
      selectRPTName: "",
      selectRPTNameGroup: "",
      pidCatalogItems: "",
      urlCatalogItems: "api/PBI_CatalogItems_Role/getPageData", //后从加载数据的url,

      columnsCatalogItems: [
        //表配置
        {
          field: "ID", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "number", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left", //文字显示位置
        },
        {
          field: "PBI_ID",
          title: "报表ID",
          type: "number",
          width: 30,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_Name",
          title: "报表名称",
          type: "string",
          width: 80,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_Path",
          title: "报表路径",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "User_LoginName",
          title: "用户登录名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_RoleName",
          title: "报表角色名称",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_RoleID",
          title: "报表角色ID",
          type: "GUID",
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

      urlCatalogItemsGroup: "api/PBI_CatalogItems_Role_Group/getPageData", //后从加载数据的url,

      columnsCatalogItemsGroup: [
        //表配置
        {
          field: "ID", //数据库表字段,必须和数据库一样，并且大小写一样
          title: "主键ID", //表头显示的名称
          type: "number", //数据类型
          isKey: true, //是否为主键字段
          width: 10, //表格宽度
          hidden: true, //是否显示
          readonly: true, //是否只读(功能未启用)
          require: true, //是否必填(功能未启用)
          align: "left", //文字显示位置
        },
        {
          field: "PBI_ID",
          title: "报表ID",
          type: "number",
          width: 30,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_Name",
          title: "报表名称",
          type: "string",
          width: 80,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_Path",
          title: "报表路径",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Group_Name",
          title: "组名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "Group_Id",
          title: "组ID",
          type: "string",
          width: 60,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_RoleName",
          title: "报表角色名称",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "PBI_RoleID",
          title: "报表角色ID",
          type: "GUID",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
      ],

      paginationCatalogItemsGroup: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30,
      },
      filterTextUser: "",
      viewModelCatalog: false,
      viewModelReport: false,
      viewModel: false,
      urlGroup: "api/PBI_SSO_Group/getPageData", //后从加载数据的url
      urlDeptUser: "api/PBI_SSO_USER/getPageData", //后从加载数据的url
      paginationHide: {
        type: Boolean,
        default: false,
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
          align: "left", //文字显示位置
        },
        {
          field: "USER_LOGIN_NAME",
          title: "登录名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "USER_TrueName",
          title: "真实名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
      ],

      paginationDeptUser: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30,
      },
      columnsUser: [
        //表配置
        {
          field: "R_Id", //数据库表字段,必须和数据库一样，并且大小写一样
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
          field: "USER_LOGIN_NAME",
          title: "登录名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
        {
          field: "USER_TrueName",
          title: "用户名",
          type: "string",
          width: 100,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
      ],

      paginationUser: {
        sortName: "ID", //从后加载数据分页时的排序字段
        sort: "",
        order: "desc",
        total: 0,
        size: 0,
        Wheres: [],
        page: 1,
        rows: 30,
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
          align: "left", //文字显示位置
        },
        {
          field: "Group_Name",
          title: "组名称",
          type: "string",
          width: 200,
          hidden: false, //是否显示
          align: "left",
          sort: true, //是否排序(目前第一个字段为排序字段，其他字段排序开发中)
        },
      ],
      paginationGroup: {
        sortName: "Group_Id", //从后加载数据分页时的排序字段
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
    ///

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
            id: data.code,
          },
          "获取数据...."
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
          //   console.log(result.data.length);
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
              id: x.depT_CODE,
              label: x.depT_NAME,
              code: x.depT_CODE,
              pcode: x.depT_PCODE,
              icon: "ios-folder-outline",
              children: [],
            };

            let appchindrened = false;
            data.children.forEach((m) => {
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
    ///
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

    addReportsGroupModelRole() {
      let rows = this.$refs.tableGroup.getSelected();
      // console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请选择需要添加的组！");
      }

      if (this.checkedRoles.length == 0) {
        return this.$message.error("请选择需要添加的角色！");
      }

      let postdata = [];
      let tempselectroles = [];
      this.PBIDataModelRole.forEach((y) => {
        this.checkedRoles.forEach((m) => {
          if (m == y.ModelRoleId) {
            tempselectroles.push(y);
          }
        });
      });
      tempselectroles.forEach((m) => {
        rows.forEach((x) => {
          let tempdata = {
            PBI_ID: this.pidCatalogItems,
            PBI_Name: this.selectReportData.label,
            PBI_Path: this.selectReportData.path,
            Group_Name: x.Group_Name,
            Group_ID: x.Group_Id,
            PBI_RoleName: m.ModelRoleName,
            PBI_RoleID: m.ModelRoleId,
            CreateDate: this.getDateStr,
          };
          postdata.push(tempdata);
        });
      });

      this.http
        .post(
          "/api/PBI_CatalogItems_Role_Group/InsertGroupRoleRight",
          postdata,
          true
        )
        .then((x) => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
          this.viewModelReport = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItemsGroup();
          this.checkedRoles = [];
          return this.$message.success("添加成功");
        });
    },
    ///role操作
    setReportRole() {
      this.http
        .post(
          "/api/V_pbi_all_user_roleright/SetRolesALLReportsUserRight",
          "",
          "设置报表行级权限控制！"
        )
        .then((x) => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
        });
    },

    SetReportRolePBI() {
      let url = "/api/V_pbi_all_user_roleright/SetReportRolePBI";
      this.http.post(url, this.PBIData, "设置报表行级权限控制！").then((x) => {
        if (!x.status) {
          return this.$message.success(x.message);
        }
      });
    },

    addUserRoleRight() {
      let rows = this.$refs.tableDeptUser.getSelected();
      // console.log(rows);
      if (rows.length == 0) {
        return this.$message.error("请选择需要添加的用户！");
      }

      if (this.checkedRoles.length == 0) {
        return this.$message.error("请选择需要添加的角色！");
      }

      let postdata = [];
      let tempselectroles = [];
      this.PBIDataModelRole.forEach((y) => {
        this.checkedRoles.forEach((m) => {
          if (m == y.ModelRoleId) {
            tempselectroles.push(y);
          }
        });
      });
      tempselectroles.forEach((m) => {
        rows.forEach((x) => {
          let tempdata = {
            PBI_ID: this.pidCatalogItems,
            PBI_Name: this.selectReportData.label,
            PBI_Path: this.selectReportData.path,
            User_LoginName: x.USER_LOGIN_NAME,
            PBI_RoleName: m.ModelRoleName,
            PBI_RoleID: m.ModelRoleId,
            CreateDate: this.getDateStr,
          };
          postdata.push(tempdata);
        });
      });

      this.http
        .post("/api/PBI_CatalogItems_Role/InsertRoleUserRight", postdata, true)
        .then((x) => {
          if (!x.status) {
            return this.$message.success(x.message);
          }
          this.viewModel = false;
          //this.getPowerBIFoder("/");
          this.loadCatalogItems();
          this.checkedRoles = [];
          return this.$message.success("添加成功");
        });
    },
    addReportRole() {
      this.selectGroupName = this.selectGroupName + "--添加用户权限";

      this.getPowerBIDept("/");
      this.pidUser = "-1";
      this.viewModel = true;
      this.loadDeptUser();
    },
    delReportRole() {
      let rows = this.$refs.tableCatalogItems.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      console.log(rows);
      let delKeys = rows.map((x) => {
        return x.ID;
      });
      // console.log(delKeys);
      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_CatalogItems_Role/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, false).then((x) => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.tableCatalogItems.load();
        return this.$message.success("删除完成");
      });
    },

    addReportRoleGroup() {
      this.addreportTile = this.selectGroupName + "--添加组权限";
      this.viewModelReport = true;
      this.loadGroup();
    },
    delReportRoleGroup() {
      let rows = this.$refs.tableCatalogItemsGroup.getSelected();
      if (rows.length == 0) return this.$message.error("请选择要删除的行!");
      console.log(rows);
      let delKeys = rows.map((x) => {
        return x.ID;
      });
      // console.log(delKeys);
      if (!delKeys || delKeys.length == 0)
        return this.$message.error("没有获取要删除的行数据!");

      let url = "/api/PBI_CatalogItems_Role_Group/Del";
      // let url = this.getUrl(this.const.DEL);
      this.http.post(url, delKeys, false).then((x) => {
        if (!x.status) return this.$message.error(x.message);
        this.$message.error(x.message);
        //删除后ssss
        //this.refresh();
        this.$refs.tableCatalogItemsGroup.load();
        return this.$message.success("删除完成");
      });
    },

    getPowerBIDept(path) {
      let treeUserdata = [];
      this.http
        .post(
          "/api/PBI_SSO_DEPT/GetPBICatalogItems",
          {
            path: path,
            id: "0",
          },
          "获取数据...."
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
          // console.log(result.data);

          result.data.forEach((x) => {
            let tempdata = [];
            tempdata = {
              id: x.depT_CODE,
              label: x.depT_NAME,
              code: x.depT_CODE,
              pcode: x.depT_PCODE,
              icon: "ios-folder-outline",
              children: [],
            };
            treeUserdata.push(tempdata);
          });
          //  console.log(treedata);
          this.Userdata = treeUserdata;
        });
    },

    ///role操作

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
        wheres: [{ name: "Group_PID", value: 0, displayType: "int" }],
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
        { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
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
          { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
        ],
      };
      this.$refs.tableUser.load(where);
    },

    //user

    _linkViewDeptUser(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeDeptUser(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" },
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
          { name: "ODEPT_CODE", value: this.pidUser, displayType: "text" },
        ],
      };
      this.$refs.tableDeptUser.load(where);
    },

    //role表格
    _linkViewCatalogItems(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeCatalogItems(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
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
      // console.log(this.pidCatalogItems);
      let where = {
        wheres: [
          { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
        ],
      };
      this.$refs.tableCatalogItems.load(where);
    },
    //role表格

    ///role group操作
    //role表格
    _linkViewCatalogItemsGroup(row, column) {
      //this.viewModel = true;
      //  this.$message.error(JSON.stringify(row));
    },
    loadTableBeforeCatalogItemsGroup(param, callBack) {
      //此处是从后台加数据前的处理，自己在此处自定义查询条件,查询数据格式自己定义或参考代码生成器查询页面请求的数据格式
      param.wheres = [
        { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
      ];
      //  console.log(param);
      // console.log("加载数据前" + param);
      callBack(true); //此处必须进行回调，返回处理结果，如果是false，则不会执行后台查询
    },
    loadTableAfterCatalogItemsGroup(data, callBack) {
      //此处是从后台加数据后，你可以在渲染表格前，预先处理返回的数据
      // console.log(data);
      // console.log("加载数据后" + data);
      callBack(true); //同上
    },
    loadCatalogItemsGroup() {
      //此处可以自定义查询条件,如果不调用的框架的查询，格式需要自己定义，
      //如果查询的是框架getPageData方法,需要指定格式,如:
      // let where={wheres:[{"name":"UserTrueName","value":"教兽",displayType:"text"}]};
      // console.log(this.pidCatalogItems);
      let where = {
        wheres: [
          { name: "PBI_ID", value: this.pidCatalogItems, displayType: "text" },
        ],
      };
      this.$refs.tableCatalogItemsGroup.load(where);
    },
    //role表格

    flashCatalog() {
      this.getPowerBIFoder("/");
    },
    appendCatalog(data) {
      //  console.log(data);

      if (data.type == "folder") {
        document.getElementById("ContentText").style.display = "flex";
        document.getElementById("Content").style.display = "none";
      } else {
        document.getElementById("ContentText").style.display = "none";
        document.getElementById("Content").style.display = "block";
      }

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
              id: data.id,
            },
            "获取数据...."
          )
          .then((result) => {
            if (!result.status) return this.$Message.error(result.message);
            //    console.log(result.data.length);

            result.data.forEach((x) => {
              let tempdata = [];

              tempdata = {
                id: x.catalogId,
                label: x.name,
                path: "",
                pid: x.parentCatalogId,
                type: "folder",
                icon: "ios-folder-outline",
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
            });
          });

        this.getreport(data);
      }

      //console.log(data.type);
      if (data.type == "report") {
        //console.log(data.id.length);

        this.pidCatalogItems = data.id.substring(2, data.id.length);
        this.selectRPTName = data.label + "----用户角色列表";
        this.selectRPTNameGroup = data.label + "----组角色列表";
        this.selectGroupName = data.label;
        this.selectReportData = data;
        this.GetReportDataModelRole(data);
        this.loadCatalogItemsGroup();
        this.loadCatalogItems();

        //     console.log(this.pidCatalogItems);

        this.PBIData.path = data.path;
        this.PBIData.id = this.pidCatalogItems;

        //  console.log(this.PBIDATA);
      }

      //  console.log(data);
    },

    filterNodeUser(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    filterNodeCatalog(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    GetReportDataModelRole(data) {
      this.http
        .post(
          "/api/V_GetALLUserRightbyRptid/GetReportDataModelRole",
          {
            path: data.path,
            id: data.id,
          },
          false
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */
          // console.log(result.data);
          this.PBIDataModelRole = result.data;
          this.checkedRoles = [];
        });
    },
    getreport(data) {
      this.http
        .post(
          "/api/PBI_CustormCatalogItems/GetPBICatalog",
          {
            path: data.path,
            id: data.id,
          },
          false
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */
          //    console.log(result.data.length);
          result.data.forEach((x) => {
            if (x.type == "PowerBI报表") {
              let tempReportdata = [];
              let reportid = "p-" + x.id;

              tempReportdata = {
                id: reportid,
                label: x.name,
                pid: x.parentCustormCatalogId,
                type: "report",
                path: x.path,
                icon: "md-stats",
                children: [],
              };

              let appreport = false;
              data.children.forEach((m) => {
                if (m.id == reportid) {
                  appreport = true;
                }
              });
              if (!appreport) {
                data.children.push(tempReportdata);
              }
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
            id: 0,
          },
          "获取数据...."
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message);
          //console.log(result.data);
          result.data.forEach((x) => {
            let tempdata = [];
            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              type: "folder",
              icon: "ios-folder-outline",
              children: [],
            };
            catalogdatatemp.push(tempdata);
          });
          //  console.log(treedata);
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
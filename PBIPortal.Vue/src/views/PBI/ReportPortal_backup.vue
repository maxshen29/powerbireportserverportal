 <template>
  <div class="all" id="alldiv">
    <div class="head">
      <div class="leftdiv">
        <span class="leftspan">
          <img :src="logourl" /> &nbsp;&nbsp;&nbsp;&nbsp; PowerBI数据分析平台
        </span>
      </div>
      <div class="rightdiv">
        <span class="rightspan">
          <img :src="headimgUrl" />
          {{ userName }}
        </span>
        &nbsp; &nbsp;
        <span @click="to" style="cursor: pointer; font-size: 12px"
          >安全退出</span
        >

        <span @click="openpersion" style="cursor: pointer; font-size: 12px"
          >个人信息</span
        >
        &nbsp; &nbsp;

        <button
          type="button"
          style="
            cursor: pointer;
            line-height: 40px;
            font-size: 14px;
            margin-top: 5px;
            width: 50px;
            height: 40px;
          "
          @click="getFullScreen"
        >
          全屏
        </button>
      </div>
    </div>
    <div class="content" id="content">
      <div class="demo-split">
        <Split v-model="split1" :max="0.75" :min="0.05">
          <div slot="left" class="demo-split-pane">
            <div class="contentleft" id="contentleft">
              <div class="menu-Search" id="menu-search">
                <el-input
                  placeholder="搜索文件夹和报表"
                  v-model="filterText"
                ></el-input>
              </div>

              <div class="eltreediv" id="eltreediv">
                <el-tree
                  class="filter-tree"
                  :data="tdata"
                  node-key="id"
                  default-expand-all
                  @node-click="appendCatalog"
                  :filter-node-method="filterNode"
                  ref="tree"
                >
                  <div class="action-group" slot-scope="{ node, data }">
                    <div class="action-text">
                      <Icon :type="data.icon"></Icon>
                      <span :title="data.label">{{ data.label }}</span>
                    </div>
                  </div>
                </el-tree>
              </div>
            </div>
          </div>
          <div slot="right" class="demo-split-pane">
            <div class="contentright" :style="pbiclass" id="contentright">
              <Tabs
                style="height: 100%"
                v-model="editableTabsValue"
                type="card"
                :before-leave="beforeLeave"
                class="my-tab-pane"
                @on-tab-remove="removeTab"
              >
                <TabPane
                  v-for="(item, index) in editableTabs"
                  :key="item.name"
                  :label="item.title"
                  :name="item.name"
                  :closable="item.close"
                  style="height: 100%"
                >
                  <div border="1" id="iframediv" class="iframediv">
                    <iframe
                      :name="item.iframename"
                      :id="item.iframeid"
                      v-bind:src="item.reportUrl"
                      width="100%"
                      height="100%"
                      style="margin: 0px 0px 0px 0px"
                      frameborder="0"
                      scrolling="no"
                    ></iframe>
                  </div>
                </TabPane>
              </Tabs>
            </div>
          </div>
        </Split>
      </div>
    </div>
  </div>
</template>
<script>
let logourl1 = require("@/assets/imgs/headimage/logo.png");
let headimgUrl1 = require("@/assets/imgs/headimage/user.png");
import VolMenu from "@/components/basic/VolMenu.vue";
let backUrl = require("@/assets/imgs/21.jpg");
export default {
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    },
    // resetheight: function () {
    //   this.$nextTick(function () {
    //     /////你需要调用的方法//页面加载完成后调用
    //     let right = document.getElementById("contentright");
    //     let contentheight = right.offsetHeight - 30 + "px";
    //     this.styleObj1 = {
    //       height: contentheight,
    //       overflow: "hidden",
    //     };
    //   });
    // },
  },

  mounted: function () {
    //此方法刷新页面时也会执行
    window.addEventListener("beforeunload", () => {
      this.$store.commit("clearUserInfo", "");
    });

    // window.onresize = () => {
    //   //this.resetheight();
    // };
  },

  created() {
    //console.log(this.styleObj1);
    // this.getPowerBIFoder("/");s
    this.userInfo = this.$store.getters.getUserInfo();
    this.userName = this.userInfo.userName;

    ///获取此用户的所有访问报表
    this.getallreport();
    ///获取用户首页
    this.getIndexPath();
    //获取所有访问目录
    this.getallPBI_Catalogs();
    // this.initpage();
    this.GetALLRightCatalogPBI();
  },
  data() {
    const tdata = [];
    const userInfo = [];
    const allrpt = [];
    const allcatalog = [];
    const iframeheight = 100;

    return {
      split1: 0.15,
      userInfo: [],
      allrpt: [],
      allcatalogrpt: [],
      iframeheight: 100,
      allcatalog: [],
      styleObj1: { height: '"' + iframeheight + '"px', overflow: "hidden" },
      //tabs
      editableTabsValue: "1",
      currentIndex: 1,
      currentIframeName: "iframe1",
      indexpath: "",
      editableTabs: [],

      tabIndex: 1,
      addIndex: 1,
      //tabls
      backUrl: backUrl,
      logourl: logourl1,
      headimgUrl: headimgUrl1,
      pbiclass: {
        backgroundImage: "url(" + backUrl + ")",
        backgroundRepeat: "no-repeat",
        backgroundSize: "100% 100%",
      },
      userName: "--",
      token: "--",
      reportUrl: "--",
      filterText: "",
      fromdisplay: "display:none;",
      titledisplay: "display:block",
      tdata: JSON.parse(JSON.stringify(tdata)),
      defaultProps: {
        children: "children",
        label: "label",
      },
    };
  },
  methods: {
    //重置iframe高度 未使用
    resetheight() {
      console.log("rest");
      /////你需要调用的方法//页面加载完成后调用
      let right = document.getElementById("contentright");
      let contentheight = right.offsetHeight - 30 + "px";
      this.styleObj1 = {
        height: contentheight,
        overflow: "hidden",
      };
    },
    ////全屏代码
    getFullScreen: function () {
      //console.log(this.currentIndex);
      if (this.currentIndex != "2") {
        this.currentIframeName = "ifame" + this.currentIndex;
        var el = document.getElementById(this.currentIframeName);
        var rfs =
          el.requestFullScreen ||
          el.webkitRequestFullScreen ||
          el.mozRequestFullScreen ||
          el.msRequestFullscreen;
        if (typeof rfs != "undefined" && rfs) {
          rfs.call(el);
        } else if (typeof window.ActiveXObject != "undefined") {
          var wscript = new ActiveXObject("WScript.Shell");
          if (wscript != null) {
            wscript.SendKeys("{F11}");
          }
        }
      }

      ////如果首页有路径也展示

      if (this.indexpath != "") {
        this.currentIframeName = "ifame" + this.currentIndex;
        var el = document.getElementById(this.currentIframeName);
        // console.log(el.src);
        //  if (el.src != "") {
        var rfs =
          el.requestFullScreen ||
          el.webkitRequestFullScreen ||
          el.mozRequestFullScreen ||
          el.msRequestFullscreen;
        if (typeof rfs != "undefined" && rfs) {
          rfs.call(el);
        } else if (typeof window.ActiveXObject != "undefined") {
          var wscript = new ActiveXObject("WScript.Shell");
          if (wscript != null) {
            wscript.SendKeys("{F11}");
          }
        }
      }

      //  this.isFullScreen = false;
      //window.onresize();
      //  this.isFullScreen = false;
      //window.onresize();
    },
    //检查全屏
    checkFullScreen() {
      return (
        document.fullscreenElement ||
        document.msFullscreenElement ||
        document.mozFullScreenElement ||
        document.webkitFullscreenElement ||
        false
      );
    },

    //初始化大小
    initpage() {
      let left = document.getElementById("contentleft");
      let right = document.getElementById("contentright");
      let iframediv = document.getElementById("iframediv");
      let alldiv = document.getElementById("alldiv");
      let eltreediv = document.getElementById("eltreediv");
      let menusearch = document.getElementById("menu-search");
      let lefticon = document.getElementById("lefticon");

      let ifheight = right.offsetHeight - 70;
      eltreediv.style.height = ifheight + "px";

      let maxT = right.offsetWidth + left.offsetWidth - 200;
      left.style.width = "200px";
      right.style.width = maxT + "px";
    },
    //向右

    goright() {
      let left = document.getElementById("contentleft");
      let right = document.getElementById("contentright");
      let maxT = right.offsetWidth + left.offsetWidth;
      let lefticon = document.getElementById("lefticon");
      let eltreediv = document.getElementById("eltreediv");
      let menusearch = document.getElementById("menu-search");
      lefticon.style.display = "block";
      menusearch.style.display = "block";
      eltreediv.style.display = "block";

      if (left.style.width == "10%") {
        left.style.width = "15%";
        right.style.width = "85%";

        // right.style.width = maxT + "px";
      } else if (left.style.width == "15%") {
        left.style.width = "20%";
        right.style.width = "80%";
      } else if (left.style.width == "20%") {
        left.style.width = "25%";
        right.style.width = "75%";
      }
      if (left.style.width == "1%") {
        left.style.width = "10%";
        right.style.width = "90%";
      }

      //  let ifheight = right.offsetHeight - 40;
      //   alldiv.style.height = ifheight + "px";
    },
    ///向左
    goleft() {
      let left = document.getElementById("contentleft");
      let right = document.getElementById("contentright");
      let box = document.getElementById("content");
      let eltreediv = document.getElementById("eltreediv");
      let menusearch = document.getElementById("menu-search");
      let lefticon = document.getElementById("lefticon");

      // let maxT = right.offsetWidth + left.offsetWidth - 20;
      //  left.style.width = "20px";
      left.style.width = "1%";
      right.style.width = "99%";
      //right.style.width = maxT + "px";
      lefticon.style.display = "none";
      menusearch.style.display = "none";
      eltreediv.style.display = "none";
    },
    ///拖拉方法(未使用)

    ///tabs方法

    addTab(data) {
      //  this.resetheight();
      var isadd = true;
      this.editableTabs.forEach((x) => {
        if (x.title == data.label) {
          isadd = false;
          ////点击切换到相应的报表
          this.editableTabsValue = x.name;
          this.currentIndex = x.name;
        }
      });
      if (isadd) {
        var pbiUrl = data.path;
        if (pbiUrl.substring(0, 1) == "/") {
          pbiUrl = pbiUrl.substring(1, pbiUrl.length);
        }
        var url =
          this.userInfo.pbiUrl +
          pbiUrl +
          "?rs:embed=true&username=" +
          this.userInfo.userName;

        url += "&token=" + this.userInfo.token;
        url += "&pbitoken=" + this.userInfo.pbitoken;

        let newTabIndex = ++this.tabIndex + "";

        this.editableTabs.push({
          title: data.label,
          reportUrl: url,
          iframename: "iframe" + data.label,
          iframeid: "ifame" + newTabIndex,
          //title: "活动" + ++this.addIndex,
          name: newTabIndex,
          close: true,
        });
        this.editableTabsValue = newTabIndex;
        this.currentIndex = newTabIndex;
      }
    },
    removeTab(targetName) {
      if (this.editableTabs.length <= 1) {
        return false;
      }
      var self = this;
      let tabs = self.editableTabs;
      let activeName = self.editableTabsValue;

      if (activeName === targetName) {
        tabs.forEach((tab, index) => {
          if (tab.name === targetName) {
            let nextTab = tabs[index + 1] || tabs[index - 1];
            if (nextTab) {
              activeName = nextTab.name;
            }
          }
        });
      }

      self.editableTabsValue = activeName;
      self.editableTabs = tabs.filter((tab) => tab.name !== targetName);

      self.editableTabs.map((tab, index) => {
        // tab.title = "首页";
        self.addIndex = index + 1;
      });

      self.currentIndex = self.editableTabsValue;

      // self.$message({
      //  type: "success",
      //   message: "关闭成功!"
      // });
    },

    /* 活动标签切换时触发 */

    beforeLeave(currentName, oldName) {
      var self = this;

      //重点，如果name是add，则什么都不触发

      if (currentName == "add") {
        this.addTab();

        return false;
      } else {
        this.currentIndex = currentName;
      }
    },
    //tabs方法
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClick(data) {},
    to() {
      this.$store.commit("clearUserInfo", "");
      this.$router.push({
        path: "/login",
      });
    },
    openpersion() {
      var isadd = true;
      this.editableTabs.forEach((x) => {
        if (x.title == "个人信息修改") {
          isadd = false;
        }
      });
      if (isadd) {
        let newTabIndex = ++this.tabIndex + "";
        this.editableTabs.push({
          title: "个人信息修改",
          reportUrl: "/perinfo",
          iframename: "iframe个人信息修改",
          iframeid: "ifame" + newTabIndex,
          //title: "活动" + ++this.addIndex,
          name: newTabIndex,
        });
        this.editableTabsValue = newTabIndex;
        this.currentIndex = newTabIndex;
      }
    },
    //GetALLRightCatalogPBI 获取所有 具备 目录 访问权限在报表

    GetALLRightCatalogPBI() {
      this.http
        .post("/api/PBI_CustormCatalogItems/GetALLRightCatalogPBI", "", false)
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          this.allcatalogrpt = result.data;
        });
    },

    //通过数组获取目录
    appendCatalog(data) {
      if (data.type == "folder") {
        let catalogdatatemp = [];
        this.allcatalog.forEach((x) => {
          if (x.parentCatalogId == data.id) {
            let tempdata = [];
            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              path: "",
              sort: x.sort,
              type: "folder",
              icon: "ios-folder-outline",
              children: [],
            };

            let appchindrened = false;
            data.children.forEach((m) => {
              if (m.id == x.catalogId || x.name == "超链报表") {
                appchindrened = true;
              }
            });
            if (!appchindrened) {
              data.children.push(tempdata);
            }
          }
        });
        this.getreport(data);
      } else {
        this.addTab(data);
      }
    },
    //通过api获取目录
    append(data) {
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

            result.data.forEach((x) => {
              let tempdata = [];
              tempdata = {
                id: x.catalogId,
                label: x.name,
                pid: x.parentCatalogId,
                path: "",
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
      } else {
        this.addTab(data);
      }
    },

    getIndexPath() {
      this.http
        .post("/api/V_group_indexpath/GeIndexPath", "", false)
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          //var pbiUrl = "";
          var url = "";
          if (result.data.length == 1) {
            var pbiUrl = result.data[0].pbI_Index_Path;

            if (pbiUrl.substring(0, 1) == "/") {
              pbiUrl = pbiUrl.substring(1, pbiUrl.length);
            }
            url =
              this.userInfo.pbiUrl +
              pbiUrl +
              "?rs:embed=true&username=" +
              this.userInfo.userName;

            url += "&token=" + this.userInfo.token;
            url += "&pbitoken=" + this.userInfo.pbitoken;

            this.indexpath = url;
          }

          let newTabIndex = ++this.tabIndex + "";

          let pbiindex = {
            title: "首页",
            reportUrl: url,
            iframename: "iframe首页",
            iframeid: "ifame" + newTabIndex,
            //title: "活动" + ++this.addIndex,
            name: newTabIndex,
          };
          this.editableTabs.push(pbiindex);
          this.editableTabsValue = newTabIndex;
          this.currentIndex = newTabIndex;
        });
    },

    getallPBI_Catalogs() {
      this.http
        .post("/api/PBI_Catalog/GetALLCatalogbyUser", "", false)
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          this.allcatalog = result.data;

          this.GetCatalog(0);
        });
    },
    //获取有权限的报表
    getallreport() {
      this.http
        .post("/api/V_GetALLUserRightbyRptid/GeAllRptRight", "", false)
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          this.allrpt = result.data;
          this.allrpt.sort(function (a, b) {
            return b.sort - a.sort;
            // return a.sort - b.sort;
          });
        });

      console.log(222);
    },
    //获取子报表目录
    getreport(data) {
      this.http
        .post(
          "/api/PBI_CustormCatalogItems/GetPBICatalog",
          {
            path: "",
            ID: data.id,
          },
          false
        )
        .then((result) => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          result.data.forEach((x) => {
            let tempReportdata = [];
            let reportid = "p-" + x.id;

            tempReportdata = {
              id: reportid,
              label: x.name,
              pid: x.parentCustormCatalogId,
              type: "report",
              path: x.path,
              sort: x.sort,
              icon: "md-stats",
              children: [],
            };

            let appreport = false;
            data.children.forEach((m) => {
              if (m.id == reportid) {
                appreport = true;
              }
            });
            let rptright = false;
            this.allrpt.forEach((n) => {
              if (n.pbI_ID == x.id) {
                rptright = true;
              }
            });

            this.allcatalogrpt.forEach((z) => {
              if (z.id == x.id) {
                rptright = true;
              }
            });

            if (!appreport && rptright) {
              data.children.push(tempReportdata);
            }
            ///添加报表
          });
        });

      //  this.pidCatalogItems = data.id;
      //this.getPowerBIFoder("/");
      //this.loadUser();
    },
    //获得报表目录，从数组中获取
    GetCatalog(m) {
      let catalogdatatemp = [];
      this.allcatalog.sort(function (a, b) {
        return b.sort - a.sort;
        // return a.sort - b.sort;
      });

      this.allcatalog.forEach((x) => {
        if (x.parentCatalogId == m) {
          let tempdata = [];
          tempdata = {
            id: x.catalogId,
            label: x.name,
            pid: x.parentCatalogId,
            path: "",
            type: "folder",
            sort: x.sort,
            icon: "ios-folder-outline",
            children: [],
          };
          let appenddata = true;
          catalogdatatemp.forEach((y) => {
            if (y.id == x.catalogId || x.name == "超链报表") {
              appenddata = false;
            }
          });
          if (appenddata) {
            catalogdatatemp.push(tempdata);
          }
        }
      });

      this.tdata = catalogdatatemp;
      this.tdata.sort(function (a, b) {
        return b.sort - a.sort;
        // return a.sort - b.sort;
      });
    },
    getPowerBIFoder(path) {
      let userInfo = this.$store.getters.getUserInfo();
      this.userName = userInfo.userTrueName;
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

          result.data.forEach((x) => {
            let tempdata = [];
            tempdata = {
              id: x.catalogId,
              label: x.name,
              pid: x.parentCatalogId,
              path: "",
              type: "folder",
              icon: "ios-folder-outline",
              children: [],
            };
            catalogdatatemp.push(tempdata);
          });

          this.tdata = catalogdatatemp;
        });
    },
  },
};
</script>
<style>
.demo-split {
  height: 100%;
  border: 1px solid #dcdee2;
}
.demo-split-pane {
  padding: 1px;
  height: 100%;
}
.ivu-tabs-nav-container {
  background: #e3e8ee;
  height: 42px;
}
.ivu-tabs-content-animated {
  height: 100%;
}

.ivu-tabs-tab {
  border-color: chartreuse;
  background: #bcc1c7;
  color: black;
}

.contentright > .ivu-tabs.ivu-tabs-card > .ivu-tabs-bar > .ivu-tabs-tab {
  border-color: chartreuse;
  margin-right: 2px;
}
.contentright > .ivu-tabs-card > .ivu-tabs-bar .ivu-tabs-tab-active {
  border-color: chartreuse;
}

.contentright > .ivu-tabs-card > .ivu-tabs-bar {
  border-color: chartreuse;
  margin-bottom: 0px;
}
</style>

<style lang="less" scoped>
.contentright > .ivu-tabs-card > .ivu-tabs-bar {
  border-color: chartreuse;
  margin-bottom: 0px;
}
.contentright > .ivu-tabs.ivu-tabs-card > .ivu-tabs-bar > .ivu-tabs-tab {
  border-color: chartreuse;
  margin-right: 2px;
}
.contentright > .ivu-tabs-card > .ivu-tabs-bar .ivu-tabs-tab-active {
  border-color: chartreuse;
}
.contentright {
  //position: absolute;
  background-color: rgb(78, 75, 226);
  width: 100%;
  height: 100%;
}
#line {
  left: 12%;
  height: 99%;
  width: 6px;
  overflow: hidden;
  background: rgb(184, 213, 241);
  cursor: w-resize;
}
.my-tab-paneall {
  background-color: rgb(4, 39, 92);

  //height: 100%;
}
.filter-tree {
  overflow-y: auto;
  overflow-x: no;
  height: 100%;
}
.lineicon {
  display: flex;
  align-items: center;
  justify-content: center;
  display: -webkit-flex;
  cursor: hand;
}
.menu_open {
  font-size: 20px;
  height: 40px;
  align-content: center;
  display: flex;
  align-items: center;
  justify-content: center;
  display: -webkit-flex;
  background-color: rgba(238, 238, 238, 0.733);
  cursor: pointer;
}
.menu_open_icon {
  height: 20px;
  width: 20px;
}
.sitetitle {
  background-color: rgb(253, 248, 248);
}

///框架
.all {
  background-color: rgb(6, 73, 124);
  height: 100%;
  width: 100%;
  position: relative;
}

.head {
  background-color: dodgerblue;
  height: 50px;
  width: 100%;
  line-height: 40px;
}

.content {
  position: absolute;
  top: 50px;
  background-color: rgb(191, 206, 140);
  bottom: 0px;
  width: 100%;
}

.contentleft {
  position: absolute;
  background-color: rgb(203, 200, 211);
  width: 100%;
  height: 100%;
}

.iframediv {
  //position: absolute;
  top: 40px;
  float: right;
  //background-color: rgb(204, 58, 21);
  bottom: 0px;
  width: 100%;
  height: 100%;
  //height: 100%;
}

///框架

.menu-Search {
  height: 40px;
  background-color: rgb(192, 192, 192);
}

.eltreediv {
  position: absolute;
  top: 42px;
  width: 100%;
  bottom: 0px;
  background-color: rgb(132, 194, 130);
}
.rightdiv {
  border-block-color: #ffffff;
  border: 1px;
  // background-color: black;
  width: 50%;
  height: 50px;
  text-align: right;
  margin-right: 50px;
}
.rightspan {
  font-size: 10px;
  color: azure;
  margin-left: 10px;
  line-height: 10px;
  text-align: right;
}
.rightdiv span {
  font-size: 14px;
  color: azure;
  margin-left: 10px;
  line-height: 50px;
  text-align: right;
}
.leftdiv {
  border-block-color: #ffffff;
  border: 1px;
  // background-color: black;
  width: 50%;
}
.spanright {
  width: 100%;
}
.head {
  display: flex;
  width: 100%;
  height: 50px;
  background: dodgerblue;
}
.head img {
  width: 35px;
  height: 35px;
  background: dodgerblue;
  margin-left: 100px;
  vertical-align: middle;
}

.leftspan {
  font-size: 20px;
  color: azure;
  margin-left: 10px;
  line-height: 50px;
}
</style>
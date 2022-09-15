<template>
  <div class="all" id="alldiv">
    <div class="head">
      <div class="leftdiv">
        <span class="leftspan">
          <img :src="logourl" /> &nbsp;&nbsp;&nbsp;&nbsp; Power BI 数据决策支撑平台
        </span>
      </div>
      <div class="rightdiv">
        <span class="rightspan">
          <img :src="headimgUrl" />
          {{userName}}
        </span>
        &nbsp; &nbsp;
        <span @click="to" style="cursor:pointer;font-size:12px;">安全退出</span>
        <span @click="openpersion" style="cursor:pointer;font-size:12px;">个人信息</span>
      </div>
    </div>
    <div class="content" id="content">
      <div class="contentleft" id="contentleft">
        <div class="menu-Search" id="menu-search">
          <el-input placeholder="搜索文件夹和报表" v-model="filterText"></el-input>
        </div>
        <div class="menu_open">
          <Icon @click="goleft" class="menu_open_icon" type="ios-arrow-dropleft" id="lefticon"></Icon>&nbsp; &nbsp;
          <Icon @click="goright" class="menu_open_icon" type="ios-arrow-dropright"></Icon>
        </div>

        <div class="eltreediv" id="eltreediv">
          <el-tree
            class="filter-tree"
            :data="tdata"
            node-key="id"
            default-expand-all
            @node-click="append"
            :filter-node-method="filterNode"
            ref="tree"
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

      <div class="contentright" :style="pbiclass" id="contentright">
        <el-tabs
          v-model="editableTabsValue"
          type="card"
          @tab-remove="removeTab"
          :before-leave="beforeLeave"
          class="my-tab-pane"
        >
          <el-tab-pane
            v-for="(item, index) in editableTabs"
            :key="item.name"
            :label="item.title"
            :name="item.name"
            closable
          >
            <div :style="styleObj1" border="1" id="iframediv">
              <iframe
                :name="item.iframename"
                :id="item.iframeid"
                v-bind:src="item.reportUrl"
                width="100%"
                height="100%"
                style="margin:0px 0px 0px 0px;"
                frameborder="0"
                scrolling="no"
              ></iframe>
            </div>
          </el-tab-pane>
        </el-tabs>
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
  created() {
    //  console.log(document.body.clientHeight);
    // this.iframeheight = document.body.clientHeight - 90;
    // this.styleObj1.height = this.iframeheight + "px";
    // this.initpage();
    //console.log(this.styleObj1);
  },
  mounted() {
    this.initpage();
    this.screenWidth = document.body.clientWidth;
    this.screenHeight = document.body.clientHeight;
    window.onresize = () => {
      return (() => {
        this.screenWidth = document.body.clientWidth;
        this.screenHeight = document.body.clientHeight;
        //console.log(this.screenWidth);
        // console.log(this.screenHeight);

        let left = document.getElementById("contentleft");
        let right = document.getElementById("contentright");
        let iframediv = document.getElementById("iframediv");
        let eltreediv = document.getElementById("eltreediv");

        let menusearch = document.getElementById("menu-search");
        let lefticon = document.getElementById("lefticon");

        let maxT = this.screenWidth - 200;
        left.style.width = "200px";
        right.style.width = maxT + "px";

        let ifheight = this.screenHeight - 110;
        eltreediv.style.height = ifheight + "px";
        iframediv.style.height = ifheight + "px";

        lefticon.style.display = "block";
        menusearch.style.display = "block";
        eltreediv.style.display = "block";

        console.log(ifheight);
      })();
    };
  },
  data() {
    const tdata = [];
    const userInfo = [];
    const allrpt = [];

    return {
      userInfo: [],
      allrpt: [],
      styleObj1: { height: "", overflow: "hidden", margin: "40px 0px 0px 0px" },
      //tabs
      editableTabsValue: "1",
      currentIndex: 1,
      editableTabs: [],
      iframeheight: 0,
      tabIndex: 1,
      addIndex: 1,
      //tabls
      backUrl: backUrl,
      logourl: logourl1,
      headimgUrl: headimgUrl1,
      pbiclass: {
        backgroundImage: "url(" + backUrl + ")"
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
        label: "label"
      }
    };
  },
  methods: {
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
      console.log(ifheight);
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

      if (left.offsetWidth < 200) {
        maxT = right.offsetWidth + left.offsetWidth;
        left.style.width = "200px";
        maxT = maxT - 200;
        right.style.width = maxT + "px";
      } else if (left.offsetWidth == 200) {
        maxT = right.offsetWidth + left.offsetWidth;
        maxT = maxT - 350;
        left.style.width = "350px";
        right.style.width = maxT + "px";
      } else if (left.offsetWidth == 350) {
        maxT = right.offsetWidth + left.offsetWidth;
        maxT = maxT - 450;
        left.style.width = "450px";
        right.style.width = maxT + "px";
      }

      console.log(left.offsetWidth);

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

      console.log(right.offsetWidth);
      console.log(right.offsetHeight);

      console.log(left.offsetWidth);
      console.log(left.offsetHeight);

      let maxT = right.offsetWidth + left.offsetWidth - 20;
      left.style.width = "20px";
      right.style.width = maxT + "px";
      lefticon.style.display = "none";
      menusearch.style.display = "none";
      eltreediv.style.display = "none";
      console.log(left.offsetWidth);
    },
    ///拖拉方法(未使用)
    dragControllerDiv: function() {
      let resize = document.getElementById("line");
      let left = document.getElementById("contentleft");
      let right = document.getElementById("contentright");
      let box = document.getElementById("content");

      resize.onmousedown = function(e) {
        let startX = e.clientX;
        resize.left = resize.offsetLeft;
        document.onmousemove = function(e) {
          let endX = e.clientX;
          let moveLen = resize.left + (endX - startX);
          let maxT = box.clientWidth - resize.offsetWidth;
          // console.log(maxT);
          //  console.log(moveLen);
          console.log(right.offsetWidth);
          console.log(right.offsetHeight);
          if (moveLen < 20) moveLen = 20;
          if (moveLen > maxT - 1000) moveLen = maxT - 1000;

          resize.style.left = moveLen;
          left.style.width = moveLen + "px";
          right.style.width = box.clientWidth - moveLen - 5 + "px";
        };
        document.onmouseup = function() {
          document.onmousemove = null;
          document.onmouseup = null;
          resize.releaseCapture && resize.releaseCapture();
        };
        resize.setCapture && resize.setCapture();
        return false;
      };
    },

    ///tabs方法

    addTab(data) {
      var isadd = true;
      this.editableTabs.forEach(x => {
        if (x.title == data.label) {
          isadd = false;
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

        // console.log(newTabIndex);
        this.editableTabs.push({
          title: data.label,
          reportUrl: url,
          iframename: "iframe" + data.label,
          iframeid: "ifame" + newTabIndex,
          //title: "活动" + ++this.addIndex,
          name: newTabIndex
        });
        this.editableTabsValue = newTabIndex;
        this.currentIndex = newTabIndex;
      }
      //  console.log("pane-" + newTabIndex);
      //  console.log(document.getElementById("pane-" + newTabIndex));
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
      self.editableTabs = tabs.filter(tab => tab.name !== targetName);

      self.editableTabs.map((tab, index) => {
        // tab.title = "首页";
        self.addIndex = index + 1;
      });

      self.currentIndex = self.editableTabsValue;

      self.$message({
        type: "success",

        message: "关闭成功!"
      });
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
    handleNodeClick(data) {
      //console.log(data);
    },
    to() {
      this.$store.commit("clearUserInfo", "");
      this.$router.push({
        path: "/login"
      });
    },
    openpersion() {
      var isadd = true;
      this.editableTabs.forEach(x => {
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
          name: newTabIndex
        });
        this.editableTabsValue = newTabIndex;
        this.currentIndex = newTabIndex;
      }
    },
    append(data) {
      //  console.log(data);

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
                path: "",
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
        this.getreport(data);
        //console.log(data.type);
      } else {
        this.addTab(data);
      }
    },

    getIndexPath() {
      this.http
        .post("/api/V_group_indexpath/GeIndexPath", "", false)
        .then(result => {
          if (!result.status) return this.$Message.error(result.message); /*  */

          //var pbiUrl = "";
          var url = "";
          if (result.data.length == 1) {
            var pbiUrl = result.data[0].pbI_Index_Path;
            console.log("pbiUrl:" + pbiUrl);
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

            //console.log(url);
          }

          //console.log(url);
          let newTabIndex = ++this.tabIndex + "";
          console.log(newTabIndex);

          let pbiindex = {
            title: "首页",
            reportUrl: url,
            iframename: "iframe首页",
            iframeid: "ifame" + newTabIndex,
            //title: "活动" + ++this.addIndex,
            name: newTabIndex
          };
          this.editableTabs.push(pbiindex);
          this.editableTabsValue = newTabIndex;
          this.currentIndex = newTabIndex;

          // console.log(result.data);
          // this.allrpt = result.data;
          //  console.log(this.allrpt);
        });
    },

    getallreport() {
      this.http
        .post("/api/V_GetALLUserRightbyRptid/GeAllRptRight", "", false)
        .then(result => {
          if (!result.status) return this.$Message.error(result.message); /*  */
          //    console.log(result.data.length);
          this.allrpt = result.data;
          //  console.log(this.allrpt);
        });
    },
    getreport(data) {
      //console.log(data.id);
      this.http
        .post(
          "/api/PBI_CustormCatalogItems/GetPBICatalog",
          {
            path: "",
            ID: data.id
          },
          false
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
              path: x.path,
              icon: "md-stats",
              children: []
            };

            let appreport = false;
            data.children.forEach(m => {
              if (m.id == reportid) {
                appreport = true;
              }
            });
            let rptright = false;
            this.allrpt.forEach(n => {
              if (n.pbI_ID == x.id) {
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

    getPowerBIFoder(path) {
      let userInfo = this.$store.getters.getUserInfo();
      this.userName = userInfo.userTrueName;
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
              path: "",
              type: "folder",
              icon: "ios-folder-outline",
              children: []
            };
            catalogdatatemp.push(tempdata);
          });
          //  console.log(treedata);
          this.tdata = catalogdatatemp;
        });
    }
  }
};
</script>
<style>
.el-tabs__header.el-top {
  height: 0px;
  margin: 0px 0px 0px 0px;
}

.el-tabs__header {
  height: 0px;
  margin: 0px 0px 0px 0px;
}
</style>

<style lang="less" scoped>
.my-tab-pane {
  background-color: rgb(198, 210, 219);
  height: 40px;
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

  //  height: 100%;
}
.filter-tree {
  overflow-y: auto;
  overflow-x: no;
  //height: 98%;
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
  height: 20px;
  align-content: center;
  display: flex;
  align-items: center;
  justify-content: center;
  display: -webkit-flex;
  cursor: pointer;
}
.menu_open_icon {
  height: 20px;
  width: 20px;
}
.sitetitle {
  background-color: rgb(253, 248, 248);
}
.pbiclass {
  display: flex;
  width: 100%;
  height: 100%;
  background-position: center center;
  background-repeat: no-repeat;
  overflow: hidden;
  //background-size: auto;
}
.content {
  display: flex;
  width: 100%;
  height: 95%;
  background-color: gray;
}
.contentright {
  width: 88%;
  height: 100%;
  background-color: rgb(57, 114, 160);
  background-size: cover;
}
.eltreediv {
  background-color: white;
  height: 80%;
}

.contentleft {
  width: 12%;
  //height: 100%;
  background-color: rgb(206, 212, 230);
}
.all {
  height: 100%;
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
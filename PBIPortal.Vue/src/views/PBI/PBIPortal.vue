<template>
  <div class="menu-container">
    <div class="menu-left">
      <div class="headlogo">
        <img v-bind:src="logo" />
      </div>
      <div class="menu-tree" style="height:95%;">
        <el-input placeholder="搜索文件夹和报表" v-model="filterText"></el-input>
        <Icon type="md-add"></Icon>
        <span>Power BI报表平台</span>
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
    <div class="menu-right">
      <div class="head">
        <span class="project-name">Power BI 报表服务器管理平台</span>
        <div class="header-info">
          <div class="h-link">
            <ul>
              <li
                v-for="(item,index) in links"
                :key="index"
                v-bind:class="{actived:selectId==item.id}"
              >
                <a href="javascript:void(0)" @click="to(item)">{{item.text}}</a>
              </li>
            </ul>
          </div>

          <div class="user">
            <span>{{userName}}</span>
            <br />
            <span>{{date}}</span>
            <!-- <span>星期五</span> -->
          </div>
        </div>
      </div>

      <div class="content">
        <div class="book-detail-store-item align-center-vertical" id="contenid">Power BI 报表服务器管理平台</div>
        <div :style="fromdisplay">
          <iframe
            name="report"
            id="report"
            v-bind:src="reportUrl"
            width="100%"
            height="90%"
            style="margin:5px 20px 5px 0px;"
            frameborder="0"
            scrolling="yes"
          ></iframe>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import VolMenu from "@/components/basic/VolMenu.vue";
let imgUrl = require("@/assets/imgs/logo.png");
var $vueIndex;

export default {
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    }
  },

  created() {
    this.getPowerBIFoder("/");
    $vueIndex = this;
    this.showTime();
    setInterval(function() {
      $vueIndex.showTime();
    }, 1000);
    // console.log(this.logo);
  },
  data() {
    const tdata = [];

    return {
      log: imgUrl,
      date: "",
      selectId: 0,
      links: [{ text: "安全退出", path: "/login", id: -4 }],
      userName: "--",
      token: "--",
      reportUrl: "--",
      filterText: "",
      fromdisplay: "display:none;",
      titledisplay: "display:block",
      tdata: JSON.parse(JSON.stringify(tdata)),
      logo: imgUrl,
      defaultProps: {
        children: "children",
        label: "label"
      }
    };
  },
  methods: {
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClick(data) {
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
    append(data) {
      if (data.type == "Folder") {
        var pathstr = data.path;
        //console.log(pathstr);
        let treedata = [];
        this.http
          .post(
            "/api/PBI_CatalogItems/GetPBICatalogItems",
            {
              path: pathstr,
              id: data.id
            },
            "获取数据...."
          )
          .then(result => {
            if (!result.status) return this.$Message.error(result.message);
            //console.log(result.data);
            let m = 0;
            result.data.forEach(x => {
              let tempdata = [];
              if (x.Type == "Folder") {
                tempdata = {
                  id: x.Id,
                  label: x.Name,
                  path: x.Path,
                  type: x.Type,
                  icon: "ios-folder-outline",
                  children: []
                };
              } else {
                tempdata = {
                  id: x.Id,
                  label: x.Name,
                  path: x.Path,
                  type: x.Type,
                  icon: "md-stats",
                  children: []
                };
              }

              let appchindrened = false;
              data.children.forEach(m => {
                if (m.id == x.Id) {
                  appchindrened = true;
                }
              });
              if (!appchindrened) {
                data.children.push(tempdata);
              }

              m++;
            });
          });
      } else {
        document.getElementById("contenid").style.display = "none";
        this.fromdisplay = "display:block;border: 1px solid #eee; height:100%";
        let userInfo = this.$store.getters.getUserInfo();
        var reportFrame = document.getElementById("report");
        var url =
          userInfo.pbiUrl +
          data.path +
          "?rs:embed=true&username=" +
          userInfo.userName;
        url += "&Token=" + userInfo.token;
        //console.log(url);
        this.reportUrl = url;
      }
      //  console.log(data);
    },
    getPowerBIFoder(path) {
      let userInfo = this.$store.getters.getUserInfo();
      this.userName = userInfo.userTrueName;
      let treedata = [];
      this.http
        .post(
          "/api/PBI_CatalogItems/GetPBICatalogItems",
          {
            path: path,
            id: ""
          },
          "获取数据...."
        )
        .then(result => {
          if (!result.status) return this.$Message.error(result.message);
          //console.log(result.data);
          result.data.forEach(x => {
            let tempdata = [];
            if (x.Type == "Folder") {
              tempdata = {
                id: x.Id,
                label: x.Name,
                path: x.Path,
                type: x.Type,
                icon: "ios-folder-outline",
                children: []
              };
            } else if (x.Type == "PowerBIReport") {
              tempdata = {
                id: x.Id,
                label: x.Name,
                path: x.Path,
                type: x.Type,
                icon: "md-stats",
                children: []
              };
            }
            treedata.push(tempdata);
          });
          //  console.log(treedata);
          this.tdata = treedata;
        });
    }
  }
};
</script>

<style lang="less" scoped>
.book-detail-store-item {
  width: 100%;
  height: 800px;
}

/*flex垂直居中对齐*/
.align-center-vertical {
  display: flex;
  align-items: center;
  justify-content: space-around;
  flex-direction: column;
  font-size: 50px;
}
.on-icon {
  line-height: 20px;
  position: relative;
  .remove {
    display: none;
    color: red;
    right: 7px;
    position: absolute;
    top: -14px;
    font-size: 13px;
  }
}
.on-icon:hover {
  cursor: pointer;
  .remove {
    display: block;
  }
}
.user {
  padding: 12px;
  position: relative;
  /* right: 20px; */
  color: #ececec;
  /* float: right; */
  display: inline-block;
  height: 100%;
}
.user span {
  position: relative;
}
.header-info:hover {
  cursor: pointer;
}
.action {
  width: 100%;
  display: flex;

  margin-bottom: 15px;
  .ivu-checkbox-wrapper {
    margin-right: 20px;
  }
  .ck {
    line-height: 33px;
    display: inline-block;
    display: flex;
    label:first-child {
      min-width: 58px;
      float: left;
      margin-top: 1px;
    }
    > div {
      float: left;
    }
  }
}

.h-link ul {
  height: 100%;
  display: inline-block;
}

.h-link li {
  height: 100%;
  list-style: none;
  float: left;
  padding: 20px 10px;
  position: relative;
  cursor: pointer;
  z-index: 3;
  /*transition: all .2s ease-in-out;*/
}

.h-link a {
  color: #b0b0b0;
  font-size: 15px;
  text-decoration: none;
  padding: 20px 20px;
  color: #fff;
}

.h-link .actived {
  border-bottom: 2px solid;
  color: #fff;
}
.h-link .actived a {
  color: #fff;
}
.h-link a:hover {
  color: #dfdfdf !important;
}
.header-info {
  right: 30px;
  display: inline-block;
  position: absolute;
  height: 100%;
  /* width: 300px; */
  /* text-align: right; */
}
.header-info > div {
  float: left;
  height: 100%;
}
.project-name {
  font-size: 50px;
  position: relative;
  color: white;
  font-weight: 600;
  margin-left: 9px;
}
.menu-container {
  display: flex;
  position: fixed;
  width: 100%;
  height: 100%;
  border-radius: 5px;
  .menu-left {
    width: 201px;
    border: 1px solid #eee;
    .module-name {
      border-radius: 0px;
      /* height: 5%; */
      line-height: 21px;
      margin-bottom: 0;
    }
  }
  .menu-right {
    flex: 1;
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
      border: 1px solid #eee;
      margin-top: 5px;
      width: 100%;
      padding: 25px;
      box-shadow: rgb(214, 214, 214) 0px 4px 21px;
    }
  }
}
.headlogo {
  text-align: center;
  align-content: center;
}
.headlogo img {
  height: 100px;
  width: 74px;
}
.m-btn {
  text-align: right;
  button {
    margin-left: 10px;
  }
}
</style>


<style  scoped>
.menu-left >>> .ivu-menu {
  width: 200px !important;
}
</style>


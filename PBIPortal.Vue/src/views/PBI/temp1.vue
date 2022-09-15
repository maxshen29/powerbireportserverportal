<template>
  <div class="all" id="alldiv">
    <div class="head">
      <div class="leftdiv"></div>
    </div>
    <div class="content" id="content">
      <div class="contentleft" id="contentleft">
        <div class="menu-Search" id="menu-search"></div>
        <div class="menu_open">
          <Icon class="menu_open_icon" type="ios-arrow-dropleft" id="lefticon"></Icon>&nbsp; &nbsp;
          <Icon class="menu_open_icon" type="ios-arrow-dropright"></Icon>
        </div>

        <div class="eltreediv" id="eltreediv">sssss</div>
      </div>
      <div class="contentright" id="contentright">
        <div class="tabdiv" id="tabdiv">swww</div>
        <div class="iframediv" id="iframediv">
          <iframe
            width="100%"
            height="100%"
            style="margin:0px 0px 0px 0px;"
            frameborder="0"
            scrolling="no"
            src="http://ei1.chinaonebuild.com/MhSSOLoginServ/sso_login.html?redirect_uri=http://192.168.2.18/reports/powerbi/ceshi2?rs:embed=true"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="less" scoped>
.all {
  background-color: rgb(6, 73, 124);
  height: 100%;
  width: 100%;
  position: relative;
}

.head {
  background-color: rgb(23, 196, 32);
  height: 40px;
  width: 100%;
  line-height: 40px;
}

.content {
  position: absolute;
  top: 40px;
  background-color: rgb(147, 184, 13);
  bottom: 0px;
  width: 100%;
}

.contentleft {
  float: left;
  background-color: rgb(61, 15, 226);
  width: 15%;
  height: 100%;
  position: relative;
}
.contentright {
  background-color: rgb(184, 13, 161);
  // top: 40px;
  float: right;
  width: 85%;
  height: 100%;
  border: 4px;
  border-color: black;
  border-top: 4px;
  position: relative;
}

.menu-Search {
  height: 40px;
  background-color: rgb(29, 11, 26);
}
.menu_open {
  height: 40px;
  background-color: rgb(124, 62, 114);
}
.eltreediv {
  position: absolute;
  top: 80px;
  width: 100%;
  bottom: 0px;
  background-color: rgb(132, 194, 130);
}
.tabdiv {
  height: 40px;
}
.iframediv {
  position: absolute;
  float: right;
  top: 40px;
  background-color: rgb(197, 184, 126);
  bottom: 0px;
  width: 100%;
}
</style>
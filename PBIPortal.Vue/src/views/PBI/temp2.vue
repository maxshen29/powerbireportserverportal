<template>
  <div class="all" id="alldiv">
    <div class="head">
      <div class="leftdiv"></div>
    </div>
    <div class="content" id="content">
      <Split v-model="split1" :max="0.55" :min="0.1">
        <div slot="left" class="demo-split-pane">
          <div class="contentleft" id="contentleft">
            <div class="menu-Search" id="menu-search"></div>

            <div class="eltreediv" id="eltreediv">sssss</div>
          </div>
        </div>
        <div slot="right" class="demo-split-pane">
          <div class="contentright" id="contentright">
            <Tabs type="card" style="height: 100%; border: 5px solid #000">
              <TabPane
                label="标签一"
                v-if="tab0"
                :closable="true"
                style="height: 100%; border: 5px solid #dcdee2"
              >
              </TabPane>
              <TabPane
                label="标签二"
                v-if="tab1"
                :closable="false"
                style="height: 100%; border: 5px solid #dcdee2"
                >标签二的内容</TabPane
              >
              <TabPane
                label="标签三"
                v-if="tab2"
                :closable="false"
                style="height: 100%; border: 5px solid #dcdee2"
                >标签三的内容</TabPane
              >
            </Tabs>
          </div>
        </div>
      </Split>
    </div>
  </div>
</template>
<script>
import tempVue from "./temp.vue";
export default {
  data() {
    return {
      split1: 0.2,
      tab0: true,
      tab1: true,
      tab2: true,
    };
  },
  methods: {
    handleTabRemove(name) {
      this["tab" + name] = true;
      this.tab0 = true;
      console.log(name);
    },
  },
};
</script>
<style>
.ivu-tabs-content-animated {
  height: 100%;
}
</style>
<style lang="less" scoped>
.ivu-tabs-content-animated {
  height: 100%;
}
.demo-split {
  height: 200px;
  border: 5px solid #dcdee2;
}
.demo-split-pane {
  padding: 1px;
}
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
  position: absolute;
  background-color: rgb(61, 15, 226);
  width: 100%;
  height: 100%;
}
.contentright {
  background-color: rgb(13, 184, 27);
  // top: 40px;
  position: absolute;
  width: 100%;
  height: 100%;
  border: 4px;
  border-color: black;
  border-top: 4px;
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
  background-color: rgb(219, 187, 41);
  bottom: 0px;
  height: 100%;
  width: 100%;
}
</style>
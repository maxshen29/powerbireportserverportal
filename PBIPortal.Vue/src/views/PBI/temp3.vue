<template>
  <div class="content">
    <div class="top-container">ssss</div>
    <div class="components-container">
      <split-pane
        split="vertical"
        :min-percent="5"
        :max-percent="35"
        :default-percent="15"
      >
        <template slot="paneL" :max-percent="35" :default-percent="15"
          >ssss B
        </template>
        <template slot="paneR">
          <div class="bottom-container">
            <Tabs type="card" style="height: 100%; border: 5px solid #000">
              <TabPane
                label="标签一"
                v-if="tab0"
                :closable="true"
                style="height: 100%; border: 5px solid #dcdee2"
              >
                <iframe
                  width="100%"
                  height="100%"
                  style="margin: 0px 0px 0px 0px"
                  frameborder="0"
                  scrolling="no"
                ></iframe>
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
        </template>
      </split-pane>
    </div>
  </div>
</template>

<script>
import splitPane from "vue-splitpane";

export default {
  name: "SplitpaneDemo",
  components: { splitPane },
  methods: {
    resize() {
      console.log("resize");
    },
  },
};
</script>

<style  scoped>
.components-container {
  position: relative;
  height: 100%;
}
.content {
  height: 100%;
}

.left-container {
  background-color: #f38181;
  height: 40px;
}

.right-container {
  background-color: #fce38a;
  height: 200px;
}

.top-container {
  background-color: #fce38a;
  width: 100%;
  height: 40px;
}

.bottom-container {
  width: 100%;
  background-color: #95e1d3;
  height: 100%;
}
</style>


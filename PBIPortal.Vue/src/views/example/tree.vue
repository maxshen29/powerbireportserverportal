<template>
  <div class="t-tree">
    <div class="custom-tree-container">
      <div class="block">
        <p>使用 scoped slot</p>
        <el-tree
          @check-change="onChange"
          @node-click=" handleNodeClick"
          :data="data"
          show-checkbox
          node-key="id"
          :default-checked-keys="checkid"
          :expand-on-click-node="false"
          ref="tree"
        >
          <div class="action-group" slot-scope="{ node, data }">
            <Icon :type="data.icon"></Icon>
            <div class="action-text" :style="{width:((4-data.lv)*18+300)+'px'}">{{ data.label }}</div>
          </div>
        </el-tree>
      </div>
    </div>
  </div>
</template>
<script>
let id = 1000;
export default {
  data() {
    return {
      checkid: [0, 2],
      checked: false,
      tree: [
        {
          id: 22,
          name: "天河",
          label: "天河",
          pid: 2,
          icon: "ios-folder-outline",
          actions: [{ text: "查询", value: "Search", checked: false }]
        },
        { id: 4, name: "白云", pid: 2, icon: "ios-man" },
        { id: 5, name: "广西", pid: 0 },
        { id: 6, name: "玉林", pid: 5 },
        { id: 7, name: "北流", pid: 6 },
        { id: 8, name: "深圳", pid: 22 },
        { id: 9, name: "东莞", pid: 1 },
        { id: 10, name: "松山湖", pid: 9 },
        { id: -111111111111, name: "广东", label: "广东", pid: 0 },
        { id: 2, name: "广州", label: "广州", pid: 22 },
        { id: 33, name: "dd", label: "ddd", pid: 7 }
      ],

      data: [
        // {
        //   id: 1,
        //   label: "一级 1",
        //   actions: [{ text: "查询", value: "Search", checked: false }],
        //   lv: 1,
        //   children: [
        //     {
        //       id: 4,
        //       label: "二级 1-1",
        //       actions: [{ text: "查询", value: "Search", checked: true }],
        //       lv: 2,
        //       children: [
        //         {
        //           id: 9,
        //           label: "三级 1-1-1",
        //           lv: 3,
        //           actions: [{ text: "查询", value: "Search", checked: false }]
        //         },
        //         {
        //           id: 10,
        //           label: "三级 1-1-2",
        //           lv: 3,
        //           actions: [{ text: "查询", value: "Search", checked: false }]
        //         }
        //       ]
        //     },
        //     {
        //       id: 10,
        //       label: "三级 1-1-2",
        //       lv: 2,
        //       actions: [
        //         { text: "查询", value: "Search", checked: false },
        //         { text: "导出", value: "Export", checked: false }
        //       ]
        //     }
        //   ]
        // }
      ]
    };
  },
  created() {},
  methods: {
    handleNodeClick(data) {
      // console.log(data);
    },
    async GetALLDept() {
      let resutdata = [];
      //if (!this.addBefore(formData)) return;

      //let url = this.getUrl(this.currentAction);
      let url = "api/PBI_SSO_DEPT/GetALLDept",
        param = {};
      await this.http.post(url, param, true).then(result => {
        resutdata = result.data;

        // console.log(roo.childrent);
      });

      let url1 = "api/PBI_SSO_USER/GetALLUser";
      await this.http.post(url1, param, true).then(result => {
        result.data.forEach(x => {
          resutdata.push(x);
        });
      });
      //console.log(resutdata);

      this.tree = resutdata;

      let root = {
        id: 0,
        label: "树形菜单",
        lv: 0,
        actions: [{ text: "查询", value: "Search", checked: false }]
      };
      this.menuFn(0, root);
      this.data = root.children;

      // this.$set()
    },

    adduser(x) {
      this.data.forEach(m => {
        if (m.id == x.pid) {
          console.log(m);
          x.lv = m.lv + 1;
          if (!this.data.children) this.data.children = [];
          this.data.children.push(x);
        }
      });
    },
    //https://www.cnblogs.com/vipsoft/p/4343493.html
    menuFn(id, data) {
      // console.log(this.tree);s
      this.tree.forEach(x => {
        if (x.pid == id) {
          x.lv = data.lv + 1;
          if (!data.children) data.children = [];
          data.children.push(x);
          this.menuFn(x.id, x);
        }
      });
    },

    leftCheckChange(node, selected) {
      node.actions.forEach((x, index) => {
        //  x.checked = selected;
        this.$set(x, "checked", selected);
      });
    },
    onChange() {
      this.$message("sss");

      /// console.log(this.$refs.tree.getCheckedKeys());
    },
    append(data) {
      const newChild = { id: id++, label: "testtest", children: [] };
      if (!data.children) {
        this.$set(data, "children", []);
      }
      data.children.push(newChild);
    },

    remove(node, data) {
      const parent = node.parent;
      const children = parent.data.children || parent.data;
      const index = children.findIndex(d => d.id === data.id);
      children.splice(index, 1);
    },

    renderContent(h, { node, data, store }) {
      return (
        <span class="custom-tree-node">
          <span>{node.label}ppp </span>
          <span>
            <el-checkbox checked="checked">备选项</el-checkbox>
          </span>
        </span>
      );
    }
  },
  computed: {},
  async mounted() {
    await this.GetALLDept();
  }
};
</script>

<style scoped>
.t-tree {
  padding: 100px;
}
.action-group {
  width: 100%;
  display: flex;
}
.action-text {
  /* width: 200px; */
  margin-right: 10px;
}
.action-item {
  flex: 1;
}
.action-item > label {
  width: 80px;
}
</style>

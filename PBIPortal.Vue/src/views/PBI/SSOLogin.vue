<template>
  <div class="tm-bg" style="height: 100%; width: 100%">
    <!-- <div class="log-bg"></div> -->
    <div class="login-contianer">
      <div class="login-project">
        <span class="project-name">Power BI数据决策支撑平台</span>
        <span class="desc"></span>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  created() {
    if (this.$route.params) {
    //  console.log(11);
    // console.log(this.$route.params.sign);
    //  alert(this.$route.params);
      var sign = this.$route.params.sign;
      var url = "/api/User/SSOLogin/?sign=" + sign;
      this.http.post(url, "", "正在登陆....").then((result) => {
        if (!result.status) return this.$Message.error(result.message);
        //this.$Message.info("登陆成功,正在跳转!");
        this.$store.commit("setUserInfo", result.data);
        this.$router.push({ path: "/ReportINPortal" });
        /// this.$router.push({ path: "/" });
        //console.log(result.data);
      });
    }
  },
};
</script>


<style lang="less" scoped>
.tm-bg {
  min-width: 700px;
  background-color: darkorange;
  background-attachment: fixed;
  background-size: cover;
}
.f-remove {
  display: none;
  cursor: pointer;
}

// .log-bg {
//   width: 100%;
//   height: 100%;
//   background-image: url(xxxxx.jpg);
//   background-repeat: no-repeat;
//   background-size: 100% 100%;
//   -moz-background-size: 100% 100%;
// }
.form-user {
  margin: 40px 0;
  .item:hover .f-remove {
    display: block;
  }
  .item {
    display: flex;
    padding-bottom: 5px;
    border-bottom: 1px solid #eee;
    margin-bottom: 30px;
    display: flex;
    .f-text {
      color: #868484;
      font-weight: 400;
      width: 90px;
      font-size: 16px;
      i {
        position: relative;
        top: -2px;
        right: 5px;
      }
    }
    .f-input {
      border: 0px;
      flex: 1;
      input {
        padding-left: 15px;
        font-size: 16px;
        font-weight: 400;
        color: #807f7f;
        width: 100%;
        outline: none;
        border: none;
      }
    }
    input:focus {
      outline: none;
      background-color: transparent;
    }
    input::selection {
      background: transparent;
    }
    input::-moz-selection {
      background: transparent;
    }
  }
}
input:-webkit-autofill {
  box-shadow: 0 0 0px 1000px white inset;
}
.login-contianer {
  transform: translateY(-50%);
  top: 50%;
  position: absolute;
  margin: 0 auto;
  left: 0;
  width: 800px;
  height: 560px;
  right: 0;
  text-align: center;
  .login-form {
    margin-top: 25px;
    border-radius: 5px;
    padding: 10px 30px 20px 30px;
    right: 0;
    left: 0;
    margin: 0 auto;
    position: absolute;
    width: 600px;
    min-height: 340px;
    background: white;
    box-shadow: 0px 4px 21px #d6d6d6;
  }
}
.login-project {
  line-height: 70px;
  img {
    height: 80px;
  }
  .project-name {
    font-size: 40px;
    position: relative;
    color: white;
    font-weight: 500;
    margin-left: 9px;
  }
  .desc {
    color: wheat;
    font-size: 15px;
  }
}
.loging-btn {
  margin-top: 40px;
}
.action {
  text-align: right;
  margin-top: 20px;
  a {
    margin-left: 20px;
  }
}
.login-footer {
  padding: 10px;
  background: #4c4b4b;
  text-align: center;
  font-size: 16px;
  position: absolute;
  /* margin-bottom: 0px; */
  /* margin-top: 20px; */
  width: 100%;
  bottom: 0px;
  border-top: 1px solid #969393;
  i {
    position: relative;
    top: -2px;
    margin-right: 5px;
  }
  a {
    margin-left: 30px;
    color: #f9ebd0;
  }
}
</style>
<style scoped>
.login-contianer >>> .ivu-form .ivu-form-item-content {
  margin-left: 0px !important;
}
</style>
<style>
input:-webkit-autofill,
input:-webkit-autofill:hover,
input:-webkit-autofill:focus {
  -webkit-box-shadow: 0 0 0px 1000px white inset !important;
  box-shadow: 0 0 0 60px #eee inset;
  -webkit-text-fill-color: #878787;
}
</style>



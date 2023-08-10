<template>
  <div id="login">
    <main class="loginPage">
      <form @submit.prevent="login">
        <h1 style="padding-top: 15px">Please Sign In</h1>
        <div role="alert" v-if="invalidCredentials" style="color: red; padding-bottom: 15px">
         <b-icon icon="exclamation-circle-fill" variant="warning"></b-icon>  Invalid username and password!
        </div>
        <div style="color: green; padding-bottom: 15px" role="alert" v-if="this.$route.query.registration">
          Thank you for registering, please sign in.
        </div>
        <div class="form-input-group form-floating">
          <label for="username">Username <b-icon icon="person-fill"/> </label>
          <input type="text" id="username" v-model="user.username" required autofocus />
        </div>
        <div class="form-input-group form-floating" style="margin-left: 3px">
          <label for="password">Password <b-icon icon="lock-fill"/></label>
          <input type="password" id="password" v-model="user.password" required />
        </div>
        <button class="btn btn-primary py-2" type="submit">Sign in</button>
        <p style="padding-top: 10px">
        <router-link :to="{ name: 'register' }">Need an account? Sign up.</router-link></p>
      </form>
    </main>
  </div>
</template>
<script>
import authService from "../services/AuthService";
export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;
          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>
<style scoped>
.form-input-group {
  margin-bottom: 1rem;
}
label {
  margin-right: 0.5rem;
}
form {
  text-align: center;
}
h1 {
  padding-bottom: 14px;
  font-family:Georgia, 'Times New Roman', Times, serif
}
main{
  background-color:whitesmoke;
  height: 100vh;
}

</style>
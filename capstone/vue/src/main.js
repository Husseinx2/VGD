import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
import Vuelidate from 'vuelidate'
import { BootstrapVue, BootstrapVueIcons, FormSelectPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Multiselect from 'vue-multiselect'
Vue.use(BootstrapVue)
Vue.use(BootstrapVueIcons)
Vue.use(FormSelectPlugin)
Vue.use(Vuelidate)
Vue.component('multiselect', Multiselect)
Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

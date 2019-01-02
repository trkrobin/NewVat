import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import {ServerTable, ClientTable, Event} from 'vue-tables-2';
import ToggleButton from 'vue-js-toggle-button';
import VeeValidate from 'vee-validate';

Vue.component('icon', FontAwesomeIcon)
Vue.use(ClientTable);
Vue.use(ToggleButton);
Vue.use(VeeValidate);
Vue.prototype.$http = axios

sync(store, router)

const app = new Vue({
  store,
  router,
  ...App
})

export {
  app,
  router,
  store
}

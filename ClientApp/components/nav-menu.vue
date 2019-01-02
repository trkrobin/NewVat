<template>
    <div class="main-nav">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark">
            <button class="navbar-toggler" type="button" @click="toggleCollapsed">
                <span class="navbar-toggler-icon"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <router-link class="navbar-brand" to="/"><icon :icon="['fab', 'microsoft']"/> ASP.NET Core with Vue.js 2</router-link>

            <transition name="slide">
                <div v-show="!collapsed">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item">
                            <router-link to="/" exact-active-class="active">
                                <icon icon="home" class="mr-2" /><span>Home</span> 
                            </router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/customers">
                                <icon icon="list" class="mr-2" /><span>Customers</span> 
                            </router-link>
                        </li>
                    </ul>
                </div>
            </transition>
        </nav>
    </div>
</template>

<script>
    import { routes } from '../router/routes'

    export default {
      data () {
        return {
          routes,
          collapsed: true
        }
      },
      methods: {
        toggleCollapsed: function (event) {
          this.collapsed = !this.collapsed
        }
      }
    }
</script>

<style scoped>
</style>
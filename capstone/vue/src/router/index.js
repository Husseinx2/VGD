import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import AddGame from '../views/AddGame.vue'
import Game from '../views/Game.vue'
import EditGame from '../views/EditGame.vue'
import NotFound from '../views/NotFound.vue'
import deleteGame from '../views/DeleteGame.vue'
import Unauthorized from '../views/Unauthorized.vue'
import search from '../views/Search.vue'
import AdvancedSearch from '../views/AdvancedSearch.vue'
import Profile from '../views/Profile.vue'
import Profiles from '../views/Profiles.vue'
import Review from '../views/Review.vue'
import AboutUs from '../views/AboutUs.vue'
import FAQ from '../views/FAQ.vue'
import Features from '../views/Features.vue'
import Careers from '../views/Careers.vue'
import OldHomePage from '../views/OldHomePage.vue'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: true
      }
    },

    {
      path: '/game/:id',
      name: 'game',
      component: Game,
      
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },

    {
      path: "/delete/:id",
      name: "delete",
      component: deleteGame,
      meta: {
        requiresAuth: true,
        hideNavbar: true,
        adminOnly: true
      }
    },
    {
      path: "/addGame",
      Name: "AddGame",
      component: AddGame,
      meta: {
        requiresAuth: true,
        hideNavbar: true,
        adminOnly: true

      }
    },
    {
      path:"/Advancedsearch",
      Name:"Advancedsearch",
      component:AdvancedSearch,
      meta: {
        requiresAuth: true,
        hideNavbar: false,
      }
    },

    {
      path: '/editGame/:id',
      name: 'edit',
      component: EditGame,
      meta: {
        requiresAuth: true,
        hideNavbar: true,
        adminOnly: true
      }
    },
    {
      path: '/search',
      name: 'search',
      component: search,
      props: (route) => ({
        queryParams: route.query.key
      }),
      meta: {
        requiresAuth: true,
        hideNavbar: false
      }
    },

    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },

    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },

    {
      path: "*",
      name: "notFound",
      component: NotFound,
    },
    {
      path: "/unauthorized",
      name: "unauthorized",
      component: Unauthorized
    },
    {
      path: "/profile/:id",
      name: "profile",
      component: Profile,
      meta: {
        requiresAuth:true,
        requiresAccess:true
      }
    },
    {
      path:"/profiles",
      name:"profiles",
      component:Profiles,
      meta:{
        requiresAuth: true,
        adminOnly: true,
      }
    },
    {
      path: "/review/:id",
      name: "review",
      component: Review,
      meta: {
        requiresAuth:true,
      }
    },
    {
      path:"/About",
      name: "About",
      component: AboutUs,
      meta:{
        requiresAuth: false,
      }
    },
    {
      path: "/FAQ",
      name: "FAQ",
      component: FAQ,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/Features",
      name: "Features",
      component: Features,
      meta: {
        requiresAuth: false
      }
    },
    {
      path:"/careers",
      name:"careers",
      component:Careers,
      meta: {
        requiresAuth: true,
      }
    },
    {
      path:"/old",
      name: "old",
      component: OldHomePage,
    }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);
  const adminOnly = to.matched.some(x => x.meta.adminOnly)
  const requiresAccess = to.matched.some(x => x.meta.requiresAccess)
  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
    // denies access to non users 
  if (adminOnly && store.state.user.role == 'user') {
    next("/unauthorized")
  }
  else {
    next();
  }
//user profile restrictions
  if (requiresAccess && store.state.user.role == 'user' && to.params.id != store.state.user.userId) {
    next("/unauthorized")
  }
  else {
    next();
  }

  


});


export default router;

import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import AccountLogin from '../views/Account/Login.vue'
import AccountRegister from '../views/Account/Register.vue'
import TransportsIndex from '../views/Transports/Index.vue'
import TransportsDetails from '../views/Transports/Details.vue'
import TransportsDelete from '../views/Transports/Delete.vue'
import TransportsCreate from '../views/Transports/Create.vue'
import TransportsEdit from '../views/Transports/Edit.vue'
import ToppingsIndex from '../views/Toppings/Index.vue'
import ToppingsDetails from '../views/Toppings/Details.vue'
import ToppingsDelete from '../views/Toppings/Delete.vue'
import ToppingsCreate from '../views/Toppings/Create.vue'
import ToppingsEdit from '../views/Toppings/Edit.vue'
import SizesIndex from '../views/Sizes/Index.vue'
import SizesDetails from '../views/Sizes/Details.vue'
import SizesDelete from '../views/Sizes/Delete.vue'
import SizesCreate from '../views/Sizes/Create.vue'
import SizesEdit from '../views/Sizes/Edit.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    { path: '/', name: 'Home', component: Home },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },
    { path: '/account/register', name: 'AccountRegister', component: AccountRegister },

    { path: '/sizes', name: 'Sizes', component: SizesIndex },
    { path: '/sizes/details/:id?', name: 'SizesDetails', component: SizesDetails, props: true },
    { path: '/sizes/delete/:id?', name: 'SizesDelete', component: SizesDelete, props: true },
    { path: '/sizes/create/', name: 'SizesCreate', component: SizesCreate, props: true },
    { path: '/sizes/edit/:id?', name: 'SizesEdit', component: SizesEdit, props: true },

    { path: '/toppings', name: 'Toppings', component: ToppingsIndex },
    { path: '/toppings/details/:id?', name: 'ToppingsDetails', component: ToppingsDetails, props: true },
    { path: '/toppings/delete/:id?', name: 'ToppingsDelete', component: ToppingsDelete, props: true },
    { path: '/toppings/create/', name: 'ToppingsCreate', component: ToppingsCreate, props: true },
    { path: '/toppings/edit/:id?', name: 'ToppingsEdit', component: ToppingsEdit, props: true },

    { path: '/transports', name: 'Transports', component: TransportsIndex },
    { path: '/transports/details/:id?', name: 'TransportsDetails', component: TransportsDetails, props: true },
    { path: '/transports/delete/:id?', name: 'TransportsDelete', component: TransportsDelete, props: true },
    { path: '/transports/create/', name: 'TransportsCreate', component: TransportsCreate, props: true },
    { path: '/transports/edit/:id?', name: 'TransportsEdit', component: TransportsEdit, props: true }

]

const router = new VueRouter({
    routes
})

export default router

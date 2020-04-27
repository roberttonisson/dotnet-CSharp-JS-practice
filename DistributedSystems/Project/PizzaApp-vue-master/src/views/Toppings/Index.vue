<template>
    <div>
        <h1>Toppings Index</h1>
        <h5>
            <router-link :to="{path: '/toppings/create/'}" class="nav-link" >Create new</router-link>
        </h5>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="topping in toppings" :key="topping.id">
                    <td>{{topping.name}}</td>
                    <td>{{topping.price}}</td>
                    <td>
                        <router-link :to="{path: '/toppings/details/'+ topping.id}" class="nav-link text-dark d-inline" ><button type="button" class="btn btn-primary">Details</button></router-link>
                        <router-link :to="{path: '/toppings/edit/'+ topping.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-primary">Edit</button></router-link>
                        <router-link :to="{path: '/toppings/delete/'+ topping.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-danger">Delete</button></router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ITopping } from "../../domain/ITopping";
import store from "../../store";

@Component
export default class ToppingIndex extends Vue {
    get toppings(): ITopping[] {
        return store.state.toppings;
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        console.log("mounted");
        store.dispatch("getToppings");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>

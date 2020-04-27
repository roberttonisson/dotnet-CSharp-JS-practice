<template>
    <div>
        <h1>Sizes Index</h1>
        <h5>
            <router-link :to="{path: '/sizes/create/'}" class="nav-link" >Create new</router-link>
        </h5>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>SizeCm</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="size in sizes" :key="size.id">
                    <td>{{size.name}}</td>
                    <td>{{size.sizeCm}}</td>
                    <td>{{size.price}}</td>
                    <td>
                        <router-link :to="{path: '/sizes/details/'+ size.id}" class="nav-link text-dark d-inline" ><button type="button" class="btn btn-primary">Details</button></router-link>
                        <router-link :to="{path: '/sizes/edit/'+ size.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-primary">Edit</button></router-link>
                        <router-link :to="{path: '/sizes/delete/'+ size.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-danger">Delete</button></router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ISize } from "../../domain/ISize";
import store from "../../store";

@Component
export default class SizeIndex extends Vue {
    get sizes(): ISize[] {
        return store.state.sizes;
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
        store.dispatch("getSizes");
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

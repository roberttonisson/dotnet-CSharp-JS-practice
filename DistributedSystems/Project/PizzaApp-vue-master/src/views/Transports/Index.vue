<template>
    <div>
        <h1>Transports Index</h1>
        <h5>
            <router-link :to="{path: '/transports/create/'}" class="nav-link" >Create new</router-link>
        </h5>
        <table class="table">
            <thead>
                <tr>
                    <th>Cost</th>
                    <th>Address</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="transport in transports" :key="transport.id">
                    <td>{{transport.cost}}</td>
                    <td>{{transport.address}}</td>
                    <td>
                        <router-link :to="{path: '/transports/details/'+ transport.id}" class="nav-link text-dark d-inline" ><button type="button" class="btn btn-primary">Details</button></router-link>
                        <router-link :to="{path: '/transports/edit/'+ transport.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-primary">Edit</button></router-link>
                        <router-link :to="{path: '/transports/delete/'+ transport.id}" class="nav-link text-dark d-inline"><button type="button" class="btn btn-danger">Delete</button></router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ITransport } from "../../domain/ITransport";
import store from "../../store";

@Component
export default class TransportIndex extends Vue {
    get transports(): ITransport[] {
        return store.state.transports;
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
        store.dispatch("getTransports");
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

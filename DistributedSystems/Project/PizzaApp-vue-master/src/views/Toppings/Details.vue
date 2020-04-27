<template>
    <div>
        <h1>Details</h1>
        <div>
            <h4>Topping</h4>
            <hr />
            <dl class="row">
                <dt class="col-sm-2">Name</dt>
                <dd class="col-sm-10">{{topping.name}}</dd>
                <dt class="col-sm-2">Price</dt>
                <dd class="col-sm-10">{{topping.price}}</dd>
            </dl>
        </div>
        <div>
            <router-link :to="{path: '/toppings/edit/'+ topping.id}" class="nav-link">Edit</router-link>
            <router-link :to="{path: '/toppings'}" class="nav-link">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ITopping } from "../../domain/ITopping";
import store from "../../store";

@Component
export default class ToppingDetails extends Vue {
    @Prop()
    private id!: string;

    get topping(): ITopping {
        return store.state.topping;
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
        store.dispatch("getTopping", this.id);
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

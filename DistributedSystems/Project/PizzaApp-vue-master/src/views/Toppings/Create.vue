<template>
    <div>
        <h1>Details</h1>

        <div>
            <h4>Topping</h4>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="name">Name</label>
                        <input v-model="toppingDTO.name" class="form-control" type="text" id="name" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="price">Price</label>
                        <input v-model="toppingDTO.price" class="form-control" type="text" id="price" />
                    </div>
                    <div class="form-group">
                        <router-link :to="{path: '/toppings'}" class="nav-link"><button @click="createOnSumbit($event)" class="btn btn-primary">Create</button></router-link>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <router-link :to="{path: '/toppings'}" class="nav-link">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "../../router";
import { IToppingCreate } from "../../domain/ITopping";
import store from "../../store";

@Component
export default class ToppingDetails extends Vue {
    private toppingDTO: IToppingCreate = {
        name: "",
        price: 0
    };

    createOnSumbit(topping: IToppingCreate): void {
        store.dispatch("createTopping", {
            name: this.toppingDTO.name,
            price: Number(this.toppingDTO.price)
        });
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

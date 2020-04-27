<template>
    <div>
        <h1>Details</h1>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label" for="name">Name</label>
                    <input v-model="topping.name" class="form-control" type="text" id="name" />
                </div>
                <div class="form-group">
                    <label class="control-label" for="price">Price</label>
                    <input v-model="topping.price" class="form-control" type="text" id="price" />
                </div>
                <div class="form-group">
                    <router-link :to="{path: '/toppings'}" class="nav-link">
                        <button @click="editOnSumbit($event)" class="btn btn-primary">Edit</button>
                    </router-link>
                </div>
            </div>
        </div>
        <div>
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

    editOnSumbit(): void {
        this.topping.id = this.id;
        store.dispatch("editTopping", {
            id: this.id,
            name: this.topping.name,
            price: Number(this.topping.price)
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

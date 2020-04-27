<template>
    <div>
        <h1>Details</h1>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label" for="name">Name</label>
                    <input v-model="size.name" class="form-control" type="text" id="name" />
                </div>
                <div class="form-group">
                    <label class="control-label" for="price">Price</label>
                    <input v-model="size.price" class="form-control" type="text" id="price" />
                </div>
                <div class="form-group">
                    <label class="control-label" for="sizeCm">Size in cm</label>
                    <input v-model="size.sizeCm" class="form-control" type="text" id="sizeCm" />
                </div>
                <div class="form-group">
                    <router-link :to="{path: '/sizes'}" class="nav-link"><button @click="editOnSumbit($event)" class="btn btn-primary">Edit</button></router-link>
                </div>
            </div>
        </div>
        <div>
            <router-link :to="{path: '/sizes'}" class="nav-link">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ISize } from "../../domain/ISize";
import store from "../../store";

@Component
export default class SizeDetails extends Vue {
    @Prop()
    private id!: string;

    get size(): ISize {
        return store.state.size;
    }

    editOnSumbit(): void {
        this.size.id = this.id;
        store.dispatch("editSize", {
            id: this.id,
            name: this.size.name,
            price: Number(this.size.price),
            sizeCm: Number(this.size.sizeCm)
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
        store.dispatch("getSize", this.id);
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

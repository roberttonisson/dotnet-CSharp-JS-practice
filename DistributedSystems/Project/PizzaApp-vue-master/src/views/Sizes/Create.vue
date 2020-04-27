<template>
    <div>
        <h1>Details</h1>

        <div>
            <h4>Size</h4>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="name">Name</label>
                        <input v-model="sizeDTO.name" class="form-control" type="text" id="name" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="price">Price</label>
                        <input v-model="sizeDTO.price" class="form-control" type="text" id="price" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="sizeCm">Size in cm</label>
                        <input
                            v-model="sizeDTO.sizeCm"
                            class="form-control"
                            type="text"
                            id="sizeCm"
                        />
                    </div>
                    <div class="form-group">
                        <router-link :to="{path: '/sizes'}" class="nav-link"><button @click="createOnSumbit($event)" class="btn btn-primary">Create</button></router-link>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <router-link :to="{path: '/sizes'}" class="nav-link">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "../../router";
import { ISizeCreate } from "../../domain/ISize";
import store from "../../store";

@Component
export default class SizeDetails extends Vue {
    private sizeDTO: ISizeCreate = {
        name: "",
        price: 0,
        sizeCm: 0
    };

    createOnSumbit(size: ISizeCreate): void {
        store.dispatch("createSize", {
            name: this.sizeDTO.name,
            price: Number(this.sizeDTO.price),
            sizeCm: Number(this.sizeDTO.sizeCm)
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

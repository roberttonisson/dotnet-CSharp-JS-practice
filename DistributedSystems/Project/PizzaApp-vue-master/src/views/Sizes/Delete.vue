<template>
    <div>
        <h1>Delete</h1>

        <div>
            <h4>Size</h4>
            <hr />
            <dl class="row">
                <dt class="col-sm-2">Name</dt>
                <dd class="col-sm-10">{{size.name}}</dd>
                <dt class="col-sm-2">Price</dt>
                <dd class="col-sm-10">{{size.price}}</dd>
                <dt class="col-sm-2">Size in cm</dt>
                <dd class="col-sm-10">{{size.sizeCm}}</dd>
            </dl>

            <router-link :to="{path: '/sizes/'}" class="nav-link text-dark">
                <button
                    @click="deleteOnClick(size)"
                    type="button"
                    class="btn btn-danger"
                >Delete</button>
            </router-link>
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

    deleteOnClick(size: ISize): void {
        store.dispatch("deleteSize", size.id);
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

<template>
    <div>
        <h1>Delete</h1>

        <div>
            <h4>Transport</h4>
            <hr />
            <dl class="row">
                <dt class="col-sm-2">Cost</dt>
                <dd class="col-sm-10">{{transport.cost}}</dd>
                <dt class="col-sm-2">Address</dt>
                <dd class="col-sm-10">{{transport.address}}</dd>
            </dl>

            <router-link :to="{path: '/transports/'}" class="nav-link text-dark">
                <button
                    @click="deleteOnClick(transport)"
                    type="button"
                    class="btn btn-danger"
                >Delete</button>
            </router-link>
        </div>
        <div>
            <router-link :to="{path: '/transports'}" class="nav-link">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { ITransport } from "../../domain/ITransport";
import store from "../../store";

@Component
export default class TransportDetails extends Vue {
    @Prop()
    private id!: string;

    get transport(): ITransport {
        return store.state.transport;
    }

    deleteOnClick(transport: ITransport): void {
        store.dispatch("deleteTransport", transport.id);
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
        store.dispatch("getTransport", this.id);
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

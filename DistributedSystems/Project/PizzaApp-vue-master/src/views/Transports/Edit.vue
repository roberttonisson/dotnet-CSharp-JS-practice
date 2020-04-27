<template>
    <div>
        <h1>Details</h1>

        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label" for="cost">Cost</label>
                    <input v-model="transport.cost" class="form-control" type="text" id="cost" />
                </div>
                <div class="form-group">
                    <label class="control-label" for="address">Address</label>
                    <input
                        v-model="transport.address"
                        class="form-control"
                        type="text"
                        id="address"
                    />
                </div>

                <div class="form-group">
                    <button @click="editOnSumbit($event)" class="btn btn-primary">Edit</button>
                </div>
            </div>
        </div>

        <div>
            <router-link :to="{path: '/transports'}" class="nav-link" >Back to list</router-link>
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

    editOnSumbit(): void {
        this.transport.id = this.id;
        this.transport.cost = Number(this.transport.cost);
        store.dispatch("editTransport", { id: this.transport.id, address: this.transport.address, cost: this.transport.cost });
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

<template>
    <div>
        <h1>Details</h1>

        <div>
            <h4>Transport</h4>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="cost">Cost</label>
                        <input
                            v-model="transportDTO.cost"
                            class="form-control"
                            type="text"
                            id="cost"
                        />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="address">Address</label>
                        <input
                            v-model="transportDTO.address"
                            class="form-control"
                            type="text"
                            id="address"
                        />
                    </div>

                    <div class="form-group">
                        <button @click="createOnSumbit($event)" class="btn btn-primary">Create</button>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <router-link :to="{path: '/transports'}" class="nav-link" >Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "../../router";
import { ITransportCreate } from "../../domain/ITransport";
import store from "../../store";

@Component
export default class TransportDetails extends Vue {
    private transportDTO: ITransportCreate = {
        cost: 0,
        address: ""
    };

    createOnSumbit(transport: ITransportCreate): void {
        store.dispatch("createTransport", { address: this.transportDTO.address, cost: Number(this.transportDTO.cost) });
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

<template>
    <div class="row">
        <div class="col-md-6">
            <h4>Register a new user.</h4>
            <h2 v-if="loginWasOk === false">Bad login attempt</h2>
            <hr />
            <div class="form-group">
                <label for="Input_Email">Email</label>
                <input v-model="registerInfo.email" class="form-control" type="email" id="Input_Email" />
            </div>
            <div class="form-group">
                <label for="Input_FirstName">FirstName</label>
                <input v-model="registerInfo.firstName" class="form-control" type="text" id="Input_FirstName" />
            </div>
            <div class="form-group">
                <label for="Input_LastName">LastName</label>
                <input v-model="registerInfo.lastName" class="form-control" type="text" id="Input_LastName" />
            </div>
            <div class="form-group">
                <label for="Input_Address">Address</label>
                <input v-model="registerInfo.address" class="form-control" type="text" id="Input_Address" />
            </div>
            <div class="form-group">
                <label for="Input_Password">Password</label>
                <input v-model="registerInfo.password" class="form-control" type="password" id="Input_Password" />
            </div>
            <div class="form-group">
                <button @click="registerOnClick($event)" class="btn btn-primary">Register</button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRegisterDTO } from "@/types/IRegisterDTO";
import store from "../../store";
import router from "../../router";

@Component
export default class GpsLocationTypesIndex extends Vue {
    private registerInfo: IRegisterDTO = {
        email: "",
        firstName: "",
        lastName: "",
        address: "",
        password: ""
    };

    private loginWasOk: boolean | null = null;

    registerOnClick(): void {
        if (
            this.registerInfo.email.length > 0 &&
            this.registerInfo.password.length > 0
        ) {
            store
                .dispatch("registerUser", this.registerInfo)
                .then((isLoggedIn: boolean) => {
                    if (isLoggedIn) {
                        this.loginWasOk = true;
                        router.push("/");
                    } else {
                        this.loginWasOk = false;
                    }
                });
        }
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

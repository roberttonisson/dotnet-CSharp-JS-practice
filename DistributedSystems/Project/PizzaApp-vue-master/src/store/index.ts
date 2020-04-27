import { IRegisterDTO } from './../types/IRegisterDTO';
import Vue from 'vue'
import Vuex, { Store } from 'vuex'
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';
import { TransportsApi } from '@/services/TransportsApi';
import { ToppingsApi } from '@/services/ToppingsApi';
import { SizesApi } from '@/services/SizesApi';
import { ISize, ISizeCreate } from './../domain/ISize';
import { ITopping, IToppingCreate } from './../domain/ITopping';
import { ITransport, ITransportCreate } from './../domain/ITransport';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        transports: [] as ITransport[],
        transport: {} as ITransport,
        toppings: [] as ITopping[],
        topping: {} as ITopping,
        sizes: [] as ISize[],
        size: {} as ISize
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
        setTransports(state, transports: ITransport[]) {
            state.transports = transports;
        },
        setTransport(state, transport: ITransport) {
            state.transport = transport;
        },
        setToppings(state, toppings: ITopping[]) {
            state.toppings = toppings;
        },
        setTopping(state, topping: ITopping) {
            state.topping = topping;
        },
        setSizes(state, sizes: ISize[]) {
            state.sizes = sizes;
        },
        setSize(state, size: ISize) {
            state.size = size;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    actions: {

        /* Methods for login/register/authenication */

        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<boolean> {
            const jwt = await AccountApi.register(registerDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },

        /* Methods for Transports */
        async getTransports(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const transports = await TransportsApi.getAll(context.state.jwt!);
                context.commit('setTransports', transports);
            }
        },
        async getTransport(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const transport = await TransportsApi.getSingle(id, context.state.jwt!);
                context.commit('setTransport', transport);
            }
        },
        async deleteTransport(context, id: string): Promise<void> {
            console.log('deleteTransport', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await TransportsApi.delete(id, context.state.jwt);
                await context.dispatch('getTransports');
            }
        },
        async createTransport(context, transport: ITransportCreate): Promise<void> {
            console.log('createTransport', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await TransportsApi.create(transport, context.state.jwt);
                await context.dispatch('getTransports');
            }
        },
        async editTransport(context, transport: ITransport): Promise<void> {
            console.log('editTransport', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await TransportsApi.edit(transport, context.state.jwt);
                await context.dispatch('getTransports');
            }
        },

        /* Methods for Toppings */
        async getToppings(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const toppings = await ToppingsApi.getAll(context.state.jwt!);
                context.commit('setToppings', toppings);
            }
        },
        async getTopping(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const topping = await ToppingsApi.getSingle(id, context.state.jwt!);
                context.commit('setTopping', topping);
            }
        },
        async deleteTopping(context, id: string): Promise<void> {
            console.log('deleteTopping', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ToppingsApi.delete(id, context.state.jwt);
                await context.dispatch('getToppings');
            }
        },
        async createTopping(context, topping: IToppingCreate): Promise<void> {
            console.log('createTopping', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ToppingsApi.create(topping, context.state.jwt);
                await context.dispatch('getToppings');
            }
        },
        async editTopping(context, topping: ITopping): Promise<void> {
            console.log('editTopping', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ToppingsApi.edit(topping, context.state.jwt);
                await context.dispatch('getToppings');
            }
        },

        /* Methods for Sizes */
        async getSizes(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const sizes = await SizesApi.getAll(context.state.jwt!);
                context.commit('setSizes', sizes);
            }
        },
        async getSize(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const size = await SizesApi.getSingle(id, context.state.jwt!);
                context.commit('setSize', size);
            }
        },
        async deleteSize(context, id: string): Promise<void> {
            console.log('deleteSize', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await SizesApi.delete(id, context.state.jwt);
                await context.dispatch('getSizes');
            }
        },
        async createSize(context, size: ISizeCreate): Promise<void> {
            console.log('createSize', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await SizesApi.create(size, context.state.jwt);
                await context.dispatch('getSizes');
            }
        },
        async editSize(context, size: ISize): Promise<void> {
            console.log('editSize', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await SizesApi.edit(size, context.state.jwt);
                await context.dispatch('getSizes');
            }
        }
    },
    modules: {
    }
})

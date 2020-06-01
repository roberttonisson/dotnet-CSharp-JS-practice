export interface ILayoutResourceStrings {
    language: string;
    login: string;
    logout: string;
    register: string;
    Home: string;
    Pizzas: string;
    Drinks: string;
    Cart: string;
    Orders: string;
}
export interface ILayoutResources {
    'en-GB': ILayoutResourceStrings;
    'et-EE': ILayoutResourceStrings;
}
export const LayoutResources: ILayoutResources = {
    'en-GB':{
        language: 'Language',
        logout: 'Logout',
        login: 'Login',
        register: 'Register',
        Home: 'Home',
        Pizzas: 'Pizzas',
        Drinks: 'Drinks',
        Cart: 'Cart',
        Orders: 'Orders',

    },
    'et-EE': {
        language: 'Keel',
        login: 'Logi sisse',
        logout: 'Logi välja',
        register: 'Registreeri',
        Home: 'Avaleht',
        Pizzas: 'Pitsad',
        Drinks: 'Joogid',
        Cart: 'Ostukorv',
        Orders: 'Tellimused',
    },
}

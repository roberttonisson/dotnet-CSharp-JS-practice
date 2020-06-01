export interface IPizzaResourceStrings {
    pizzas: string;
    placeholder: string;
    defaultToppingsAre: string;
    crust: string;
    size: string;
    selectAdditionalToppings: string;
    choosePizza: string;
    quantity: string;
    total: string;
    close: string;
    addToCart: string;
}
export interface IPizzaResources {
    'en-GB': IPizzaResourceStrings;
    'et-EE': IPizzaResourceStrings;
}
export const PizzaResources: IPizzaResources = {
    'en-GB':{
        pizzas: 'Pizzas',
        placeholder: 'This is a placeholder for our incredibly specific pizza description.',
        defaultToppingsAre: 'Default toppings are:',
        crust: 'Crust:',
        size: 'Size:',
        selectAdditionalToppings: 'Select additional toppings:',
        choosePizza: 'Choose pizza',
        quantity: 'Quantity',
        total: 'Total',
        close: 'Close',
        addToCart: 'Add to cart',    
    },
    'et-EE': {
        pizzas: 'Pitsad',
        placeholder: 'Siia käib meie väga spetsfiifiline kirjeldus selle pitsa kohta.',
        defaultToppingsAre: 'Pitsa lisandid:',
        crust: 'Pitsapõhi:',
        size: 'Suurus:',
        selectAdditionalToppings: 'Vali veel lisandeid:',
        choosePizza: 'Vali pitsa',
        quantity: 'Kogus',
        total: 'Kokku',
        close: 'Sule',
        addToCart: 'Lisa ostukorvi',  
    },
}

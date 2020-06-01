export interface IDrinkResourceStrings {
    drinks: string;
    placeholder: string;
    chooseDrink: string;
    quantity: string;
    total: string;
    close: string;
    addToCart: string;
}
export interface IDrinkResources {
    'en-GB': IDrinkResourceStrings;
    'et-EE': IDrinkResourceStrings;
}
export const DrinkResources: IDrinkResources = {
    'en-GB':{
        drinks: 'Drinks',
        placeholder: 'This is a placeholder for our incredibly specific drink description.',
        chooseDrink: 'Choose drink',
        quantity: 'Quantity',
        total: 'Total',
        close: 'Close',
        addToCart: 'Add to cart',    
    },
    'et-EE': {
        drinks: 'Joogid',
        placeholder: 'Siia käib meie väga spetsfiifiline kirjeldus selle joogi kohta.',
        chooseDrink: 'Vali jook',
        quantity: 'Kogus',
        total: 'Kokku',
        close: 'Sule',
        addToCart: 'Lisa ostukorvi',  
    },
}

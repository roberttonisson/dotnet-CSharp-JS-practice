export interface ICartResourceStrings {
    loading: string;
    cart: string;
    pizzasAndDrinksInCart: string;
    pizza: string;
    type: string;
    additionalToppings: string;
    quantity: string;
    cost: string;
    drink: string;
    size: string;
    price: string;
    total: string;
    orderNow: string;
    selectpayment: string;
    shippingAddress: string;
    pay: string;
    cash: string;
    continue: string;
    required: string;

}
export interface ICartResources {
    'en-GB': ICartResourceStrings;
    'et-EE': ICartResourceStrings;
}
export const CartResources: ICartResources = {
    'en-GB':{
        loading: 'Loading',
        cart: 'Cart',
        pizzasAndDrinksInCart: 'Pizzas and drinks in cart:',
        pizza: 'Pizza',
        type: 'Type',
        additionalToppings: 'Additional toppings',
        quantity: 'Quantity',
        cost: 'Cost',
        drink: 'Drink',
        size: 'Size',
        price: 'Price',
        total: 'Total',
        orderNow: 'Order now',
        selectpayment: 'Select payment method and pay',
        shippingAddress: 'Shipping address',
        pay: 'Pay',
        cash: 'Cash',
        continue: 'Continue',
        required: 'You need to have at least one pizza in cart to order!'
        
    },
    'et-EE': {
        loading: 'Lehte laetakse',
        cart: 'Ostukorv',
        pizzasAndDrinksInCart: 'Pitsad ja joogid sinu ostukorvis:',
        pizza: 'Pitsa',
        type: 'Tüüp',
        additionalToppings: 'Lisatud lisandid',
        quantity: 'Kogus',
        cost: 'Maksumus',
        drink: 'Jook',
        size: 'Suurus',
        price: 'Hind',
        total: 'Kokku:',
        orderNow: 'Telli kohe',
        selectpayment: 'Vali makseviis ja tasu ostu eest',
        shippingAddress: 'Kättesaamise aadress',
        pay: 'Maksa',
        cash: 'Sularahas',
        continue: 'Jätka',
        required: 'YOstukorvis peab olema vähemalt üks pitsa et tellida!'
    },
}

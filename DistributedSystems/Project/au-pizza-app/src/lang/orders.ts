export interface IOrderResourceStrings {
    loading: string;
    orders: string;
    item: string;
    price: string;
    quantity: string;
    sum: string;
    total: string;
    reorderThis: string;
    successful: string;
    unsuccessful: string;
}
export interface IOrderResources {
    'en-GB': IOrderResourceStrings;
    'et-EE': IOrderResourceStrings;
}
export const OrderResources: IOrderResources = {
    'en-GB':{
        loading: 'Loading...',
        orders: 'Orders',
        item: 'Item',
        price: 'Price',
        quantity: 'Quantity',
        sum: 'Sum',
        total: 'Total:',
        reorderThis: 'Reorder this',
        successful: 'Successfully paid',
        unsuccessful: 'Paid at delivery'

    },
    'et-EE': {
        loading: 'Tellimusi laetakse...',
        orders: 'Varasemad tellimused',
        item: 'Toode',
        price: 'Hind',
        quantity: 'Quantity',
        sum: 'Summa',
        total: 'Kokku:',
        reorderThis: 'Telli see uuesti',
        successful: 'Edukalt tasutud',
        unsuccessful: 'Makstakse kättesaamisel'
    },
}

import { IPizzaInCart } from 'domain/IPizzaInCart';
import { IDrink } from 'domain/IDrink';
import { CartService } from './../../service/cart-service';
import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { ICart } from 'domain/ICart';
import { IDrinkInCart } from 'domain/IDrinkInCart';

@autoinject
export class CartsIndex {
    private _carts: ICart[] = [];

    private _cart: ICart | null = null;

    private _alert: IAlertData | null = null;

    private _loading: boolean = true;

    private _total: number = 0;


    constructor(private cartService: CartService) {

    

    }

    attached() {
        this.cartService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._carts = response.data!;
                    this._cart = this._carts[0];
                    this._loading = false;
                } else {
                    this._loading = false;
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

    drinkTotal(drinkInCart: IDrinkInCart): number {
        let total = 0;
        if (drinkInCart.drink != null) {
            total += drinkInCart.drink.price * drinkInCart.quantity;
        }
        this._total += total;
        return total;
    }

    pizzaTotal(pizzaInCart: IPizzaInCart): number {
        let total = 0
        if (pizzaInCart.pizzaType != null) {
            total += pizzaInCart.pizzaType.price
        }
        if (pizzaInCart.crust != null) {
            total += pizzaInCart.crust.price
        }
        if (pizzaInCart.size != null) {
            total += pizzaInCart.size.price
        }
        if (pizzaInCart.additionalToppings != null) {
            for (const topping of pizzaInCart.additionalToppings) {
                total += topping.topping!.price
            }
        }
        this._total += total;
        return total * pizzaInCart.quantity;
    }

}

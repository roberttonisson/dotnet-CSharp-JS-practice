import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CartService } from 'service/cart-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from "types/AlertType";
import { IAlertData } from "types/IAlertData";
import { ICart } from 'domain/ICart';
import { IDrink } from 'domain/IDrink';
import { IDrinkInCart } from 'domain/IDrinkInCart';
import { DrinkService } from 'service/drink-service';
import { DrinkInCartService } from 'service/drink-in-cart-service';
import { DrinkResources } from './../../lang/drinks';
import 'style/pizzasDrinks.css'
import { AppState } from 'state/app-state';



@autoinject
export class DrinksIndex {
    private langResources = DrinkResources;

    private _drinks: IDrink[] = [];

    private _alert: IAlertData | null = null;

    private _cart: ICart | null = null;

    private _drink: IDrink| null = null;

    private _drinkInCart: IDrinkInCart| null = null;

    private _quantity: number = 1;

    private _row: number = 0;



    constructor(
        private appState: AppState,
        private drinkService: DrinkService,
        private drinkInCartService: DrinkInCartService,
        private cartService: CartService,
        private router: Router
    ) {

    }

    attached() {
         
        this.cartService.getActive().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._cart = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );

        this.drinkService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._drinks = response.data!;
                    this._row = Math.ceil(this._drinks.length / 3)
                } else {
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    selectDrink(drink: IDrink) {
        if (this.appState.jwt == null) {
            this.router.navigateToRoute('account-login');
            return;
        }
        this._drink = drink;
    }

    deselectDrink() {
        this._drink = null;
    }

    addToCart(event: Event) {
        console.log(event);
        this.drinkInCartService
            .create({
                drinkId: this._drink!.id,
                cartId: this._cart!.id,
                quantity: this._quantity,
                id: undefined
            })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        //console.log(response.data)
                        this._drinkInCart = response.data!
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );
        event.preventDefault();
    }

    plus(){
        this._quantity = Number(this._quantity) + 1;
        console.log(this._quantity)
    }
    minus(){
        this._quantity = Number(this._quantity) -  1;
        if (this._quantity < 1) {
            this._quantity = 1;
        }
        console.log(this._quantity)
    }

    get total(): number {
        if (this._drink != null) {
            return this._quantity * this._drink.price;
        }
        return 0;
       
    }

    

}

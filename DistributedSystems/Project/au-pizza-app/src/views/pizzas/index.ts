import { DefaultToppingService } from './../../service/default-topping-service';
import { AdditionalToppingService } from './../../service/additional-topping-service';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { CartService } from './../../service/cart-service';
import { PizzaInCartService } from './../../service/pizza-in-cart-service';
import { IPizzaInCart } from './../../domain/IPizzaInCart';
import { ToppingService } from './../../service/topping-service';
import { SizeService } from './../../service/size-service';
import { ITopping } from './../../domain/ITopping';
import { ISize } from './../../domain/ISize';
import { autoinject } from 'aurelia-framework';
import { AlertType } from "types/AlertType";
import { PizzaTypeService } from "service/pizza-type-service";
import { IAlertData } from "types/IAlertData";
import { IPizzaType } from "domain/IPizzaType";
import { ICrust } from 'domain/ICrust';
import { CrustService } from 'service/crust-service';
import { ICart } from 'domain/ICart';
import 'style/pizzasDrinks.css'
import { IDefaultTopping } from 'domain/IDefaultTopping';



@autoinject
export class CartsIndex {
    private _pizzas: IPizzaType[] = [];

    private _crusts: ICrust[] = [];

    private _sizes: ISize[] = [];

    private _toppings: ITopping[] = [];

    private _defaultToppings: IDefaultTopping[] = [];


    private _alert: IAlertData | null = null;


    private _cart: ICart | null = null;

    private _pizza: IPizzaType | null = null;

    private _crust: ICrust | null = null;

    private _size: ICrust | null = null;

    private _pizzaInCart: IPizzaInCart | null = null;

    private _additional: ITopping[] = [];

    private _default: ITopping[] = [];

    private _available: ITopping[] = [];

    private _quantity: number = 1;




    constructor(
        private pizzaService: PizzaTypeService,
        private crustService: CrustService,
        private sizeService: SizeService,
        private toppingService: ToppingService,
        private pizzaInCartService: PizzaInCartService,
        private cartService: CartService,
        private additionalToppingService: AdditionalToppingService,
        private defaultToppingService: DefaultToppingService
    ) {

    }

    attached() {
        this.cartService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._cart = response.data![0];
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

        this.defaultToppingService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._defaultToppings = response.data!;
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

        this.pizzaService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._pizzas = response.data!;
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

        this.crustService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._crusts = response.data!;
                    this._crust = this._crusts[0];
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

        this.toppingService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._toppings = response.data!;
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

        this.sizeService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._sizes = response.data!;
                    this._size = this._sizes[0];
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

    selectPizza(pizza: IPizzaType) {
        this._pizza = pizza;
        for (const defaultTopping of this._defaultToppings) {
            if (defaultTopping.pizzaTypeId === pizza.id) {
                this._default.push(defaultTopping.topping!)
            }
        }
        this.availableToppings();
    }

    availableToppings() {
        console.log(this._toppings)
        console.log(this._default)
        for (const t of this._toppings) {
            let inDefault = false;
            for (const d of this._default) {
                if (d.id == t.id) {
                    inDefault = true;
                }
            }
            if (!inDefault) {
                this._available.push(t)
            }

        }

    }

    deselectPizza() {
        this._pizza = null;
        this._additional = [];
        this._default = []
    }

    chooseCrust(crust: ICrust) {
        this._crust = crust;
    }

    chooseSize(size: ISize) {
        this._size = size;
    }

    plus() {
        this._quantity = Number(this._quantity) + 1;
        console.log(this._quantity)
    }
    minus() {
        this._quantity = Number(this._quantity) - 1;
        if (this._quantity < 1) {
            this._quantity = 1;
        }
        console.log(this._quantity)
    }

    addToCart(event: Event) {
        console.log(event);
        this.pizzaInCartService
            .create({
                pizzaTypeId: this._pizza!.id,
                cartId: this._cart!.id,
                crustId: this._crust!.id,
                sizeId: this._size!.id,
                quantity: this._quantity,
                id: undefined
            })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        //console.log(response.data)
                        this._pizzaInCart = response.data!
                        for (const topping of this._additional) {
                            console.log(this._pizzaInCart!.id)
                            this.additionalToppingService
                                .create({
                                    pizzaInCartId: this._pizzaInCart!.id,
                                    toppingId: topping.id,
                                    id: undefined
                                })
                                .then(
                                    response => {
                                        if (response.statusCode >= 200 && response.statusCode < 300) {
                                            this._alert = null;
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

    get total(): number {
        let total = 0;
        if (this._pizza != null) {
            total = total + this._pizza.price
        }
        if (this._crust != null) {
            total = total + this._crust.price
        }
        if (this._size != null) {
            total = total + this._size.price
        }
        for (const topping of this._additional) {
            total = total + topping.price
        }

        

        return total * this._quantity;
    }

}

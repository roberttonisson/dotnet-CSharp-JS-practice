import { DrinkInCartService } from './../../service/drink-in-cart-service';
import { inject } from 'aurelia-dependency-injection';
import { TransportService } from './../../service/transport-service';
import { ITransport } from 'domain/ITransport';
import { IInvoice } from './../../domain/IInvoice';
import { InvoiceService } from './../../service/invoice-service';
import { InvoiceLineService } from './../../service/invoice-line-service';
import { IPizzaInCart } from 'domain/IPizzaInCart';
import { CartService } from './../../service/cart-service';
import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { ICart } from 'domain/ICart';
import { IDrinkInCart } from 'domain/IDrinkInCart';
import { Router } from 'aurelia-router';
import 'style/payment.css'
import { PizzaInCartService } from 'service/pizza-in-cart-service';

@autoinject
export class CartsIndex {

    private _cart: ICart | null = null;

    private _alert: IAlertData | null = null;

    private _loading: boolean = true;
    private _loadingPayment: boolean = false;

    private _total: number = 0;

    private _empty: boolean = true;

    private _address: string = "";

    private _invoice: IInvoice | null = null;

    private _transport: ITransport | null = null;

    private _changedPizzas: IPizzaInCart[] = [];
    private _changedDrinks: IDrinkInCart[] = [];


    constructor(private cartService: CartService, private invoiceLineService: InvoiceLineService, private invoiceService: InvoiceService,
        private transportService: TransportService, private drinkInCartService: DrinkInCartService, private pizzaInCartService: PizzaInCartService, private router: Router) {
        this.router = router;
    }

    attached() {
        this.cartService.getActive().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._cart = response.data!;

                    this.calculateTotals();
                    this._loading = false;
                    if (this._cart.drinkInCarts!.length > 0 || this._cart.pizzaInCarts!.length > 0) {
                        this._empty = false;
                    }
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

    detached() {
        this.updateQuantity();
    }

    private calculateTotals() {
        this._cart!.total = 0;
        //calculate pizza prizes
        if (this._cart!.pizzaInCarts != null) {
            for (const pic of this._cart!.pizzaInCarts) {
                pic.price = 0;
                if (pic.additionalToppings != null) {
                    for (const additional of pic.additionalToppings) {                        
                        pic.price += additional.topping!.price;
                    }
                }
                pic.price += pic.size!.price;
                pic.price += pic.crust!.price;
                pic.price += pic.pizzaType!.price;
                this._cart!.total += pic.price * pic.quantity;

            }
        }

        //calculate drink prizes
        if (this._cart!.drinkInCarts != null) {
            for (const dic of this._cart!.drinkInCarts) {
                dic.price = 0;
                dic.price += dic.drink!.price;
                this._cart!.total += dic.price * dic.quantity;
            }
        }
    }

    addQuantity(pizzaInCart: IPizzaInCart | null = null, drinkInCart: IDrinkInCart | null = null, nr: number = 0) {
        if (pizzaInCart != null) {

            
            if (nr > 0) {
                this._cart!.total! += pizzaInCart.price!;
            }
            if (nr < 0) {
                if (pizzaInCart.quantity <= 1) {
                    return;
                }
                this._cart!.total! -= pizzaInCart.price!;
            }
            pizzaInCart.quantity += nr;
            this._changedPizzas.push(pizzaInCart);

        }
        if (drinkInCart != null) {
            drinkInCart.quantity += nr;
            this._changedDrinks.push(drinkInCart);
        }

    }

    updateQuantity() {
        this._loadingPayment = true;
        for (const pizzaInCart of this._changedPizzas) {
            this.pizzaInCartService.update(pizzaInCart.id, {
                pizzaTypeId: pizzaInCart.pizzaTypeId,
                crustId: pizzaInCart.crustId, sizeId: pizzaInCart.sizeId, cartId: pizzaInCart.cartId,
                id: pizzaInCart.id, quantity: (pizzaInCart.quantity)
            })
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;              
                        } else {
                        this._loadingPayment = false;
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
        for (const drinkInCart of this._changedDrinks) {
            this.drinkInCartService.update(drinkInCart.id, {
                id: drinkInCart.id, drinkId: drinkInCart.drinkId,
                cartId: drinkInCart.cartId, quantity: (drinkInCart.quantity)
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
        this._loadingPayment = false;
    }

    remove(pizzaInCart: IPizzaInCart | null = null, drinkInCart: IDrinkInCart | null = null) {

        if (pizzaInCart != null) {
            this._cart!.total! -= pizzaInCart.price! * pizzaInCart.quantity;
            const index = this._cart!.pizzaInCarts!.indexOf(pizzaInCart)
            if (index > -1) {
                this._cart!.pizzaInCarts!.splice(index, 1);
            }
            this.pizzaInCartService.delete(pizzaInCart.id)
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            if (this._cart!.drinkInCarts!.length == 0 || this._cart!.pizzaInCarts!.length == 0) {
                                this._empty = true;
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
        }
        if (drinkInCart != null) {
            const index = this._cart!.drinkInCarts!.indexOf(drinkInCart)
            if (index > -1) {
                this._cart!.drinkInCarts!.splice(index, 1);
            }
            this.drinkInCartService.delete(drinkInCart.id)
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            if (this._cart!.drinkInCarts!.length == 0 || this._cart!.pizzaInCarts!.length == 0) {
                                this._empty = true;
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
        }
    }

    order(paid: boolean = true) {
        this.transportService
            .create({
                address: this._address,
                cost: 2,
                id: undefined
            })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._transport = response.data!;

                        this.invoiceService
                            .create({
                                isPaid: paid,
                                id: undefined,
                                transportId: this._transport!.id,
                                appUserId: this._cart!.appUserId,
                                orderStatusId: "fd73bd7c-e2f5-4836-166e-08d7f67bef1b",
                                estimated: null,

                            })
                            .then(
                                response => {
                                    if (response.statusCode >= 200 && response.statusCode < 300) {
                                        this._alert = null;
                                        this._invoice = response.data!;

                                        if (this._cart?.drinkInCarts) {
                                            for (const dic of this._cart.drinkInCarts) {
                                                this.invoiceLineService
                                                    .create({
                                                        id: undefined,
                                                        quantity: dic.quantity,
                                                        pizzaInCartId: null,
                                                        drinkInCartId: dic.id,
                                                        invoiceId: this._invoice!.id
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
                                        }

                                        if (this._cart?.pizzaInCarts) {
                                            for (const pic of this._cart.pizzaInCarts) {
                                                this.invoiceLineService
                                                    .create({
                                                        id: undefined,
                                                        quantity: pic.quantity,
                                                        pizzaInCartId: pic.id,
                                                        drinkInCartId: null,
                                                        invoiceId: this._invoice!.id
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
                                        }

                                        this.cartService.update(this._cart!.id, { id: this._cart!.id, active: false, appUserId: this._cart!.appUserId })
                                            .then(response => {
                                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                                    this._alert = null;

                                                    this.cartService.create({ id: undefined, active: true, appUserId: this._cart!.appUserId })
                                                        .then(response => {
                                                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                                                this._alert = null;
                                                                this.router.navigateToRoute('orders-index');
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

}

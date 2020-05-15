import { IInvoice } from './../../domain/IInvoice';
import { InvoiceService } from './../../service/invoice-service';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AlertType } from "types/AlertType";
import { IAlertData } from "types/IAlertData";
import { bindable } from 'aurelia-framework';
import 'style/pizzasDrinks.css'



@autoinject
export class DrinksIndex {


    private _invoices: IInvoice[] = [];

    private _alert: IAlertData | null = null;

    private _loading: boolean = true;


    constructor(
        private invoiceService: InvoiceService,

    ) {

    }

    attached() {

        this.invoiceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._invoices = response.data!;
                    this._loading = false;
                } else {
                    // show error message
                    this._loading = false;
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

    calculateTotals(invoice: IInvoice) {
        invoice.total = 0;
        for (const invoiceLine of invoice.invoiceLines!) {
            invoiceLine.total = 0;
            if (invoiceLine.pizzaInCart != null) {
                invoiceLine.pizzaInCart.price = 0;
                if (invoiceLine.pizzaInCart.additionalToppings != null) {
                    for (const additional of invoiceLine.pizzaInCart.additionalToppings) {
                        invoice.total += additional.topping!.price;
                        invoiceLine.pizzaInCart.price += additional.topping!.price;
                    }
                }
                invoiceLine.pizzaInCart.price += invoiceLine.pizzaInCart.size!.price;
                invoiceLine.pizzaInCart.price += invoiceLine.pizzaInCart.crust!.price;
                invoiceLine.pizzaInCart.price += invoiceLine.pizzaInCart.pizzaType!.price;
                invoiceLine.total += invoiceLine.pizzaInCart.price * invoiceLine.pizzaInCart.quantity;
                invoice.total += invoiceLine.total;
            }

            if (invoiceLine.drinkInCart != null) {
                invoiceLine.drinkInCart.price = 0;
                invoiceLine.drinkInCart.price += invoiceLine.drinkInCart.drink!.price;
                invoiceLine.total += invoiceLine.drinkInCart.price * invoiceLine.drinkInCart.quantity;
                invoice.total += invoiceLine.total;
            }
        }

    }

}

import { INewProduct } from './../../domain/INewProduct';
import { NewProductService } from './../../service/new-product-service';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from "types/IAlertData";
import { AlertType } from "types/AlertType";




@autoinject
export class HomeIndex {

    private _alert: IAlertData | null = null;

    private _newProducts: INewProduct[] = [];



    constructor(public newProductService: NewProductService

    ) {
        console.log(Math.ceil(3.6));
        console.log(Math.trunc(3.6));
    console.log(Math.round(-1.6));
    console.log(Math.floor(3.6) );
    
    
    }

    attached() {
        this.newProductService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._newProducts = response.data!;
                    console.log(this._newProducts)
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



}

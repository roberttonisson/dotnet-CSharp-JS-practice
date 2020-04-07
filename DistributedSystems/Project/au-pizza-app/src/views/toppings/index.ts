import { AlertType } from './../../types/AlertType';
import { ITopping } from './../../domain/ITopping';
import { autoinject } from 'aurelia-framework';
import { ToppingService } from 'service/topping-service';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class ToppingsIndex {
    private _toppings: ITopping[] = [];

    private _alert: IAlertData | null = null;

    constructor(private toppingService: ToppingService) {

    }

    attached() {
        this.toppingService.getToppings().then(
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
    }

}

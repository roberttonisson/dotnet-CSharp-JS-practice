import { ITopping } from '../../domain/ITopping';
import { autoinject } from 'aurelia-framework';
import { ToppingService } from 'service/topping-service';

@autoinject
export class ToppingsIndex {
    private _toppings: ITopping[] = [];

    constructor(private toppingService: ToppingService) {

    }

    attached() {
        this.toppingService.getToppings().then(
            data => this._toppings = data
        );
    }

}

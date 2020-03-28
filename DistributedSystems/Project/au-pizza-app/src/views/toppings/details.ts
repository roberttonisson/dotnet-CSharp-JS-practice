import { ITopping } from '../../domain/ITopping';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

import { ToppingService } from 'service/topping-service';

@autoinject
export class ToppingDetails {

    private _topping: ITopping | null = null;

    constructor(private toppingService: ToppingService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.toppingService.getTopping(params.id).then(
                data => this._topping = data
            );
        }
    }

}

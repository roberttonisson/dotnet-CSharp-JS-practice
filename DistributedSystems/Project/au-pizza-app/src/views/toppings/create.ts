import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ToppingService } from 'service/topping-service';
import { ITopping } from 'domain/ITopping';

@autoinject
export class ToppingsCreate {

    private _topping: ITopping | null = null;

    constructor(private toppingService: ToppingService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.toppingService
            .createTopping(this._topping!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('toppings-index', {});
            });

        event.preventDefault();
    }

}

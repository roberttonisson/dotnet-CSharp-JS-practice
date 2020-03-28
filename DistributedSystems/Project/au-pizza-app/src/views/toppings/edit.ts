import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ToppingService } from 'service/topping-service';
import { ITopping } from 'domain/ITopping';

@autoinject
export class ToppingsEdit {

    private _topping: ITopping | null = null;

    constructor(private toppingService: ToppingService, private router: Router) {
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

    onSubmit(event: Event) {
        console.log(event);
        this.toppingService
            .updateTopping(this._topping!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('toppings-index', {});
            });

        event.preventDefault();
    }
}

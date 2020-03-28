import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { SizeService } from 'service/size-service';
import { ISize } from 'domain/ISize';

@autoinject
export class SizesCreate {

    private _size: ISize | null = null;

    constructor(private sizeService: SizeService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.sizeService
            .createSize(this._size!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('sizes-index', {});
            });

        event.preventDefault();
    }

}

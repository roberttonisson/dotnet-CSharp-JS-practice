import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { SizeService } from 'service/size-service';
import { ISize } from 'domain/ISize';

@autoinject
export class SizesDelete {
    private _size: ISize | null = null;

    constructor(private sizeService: SizeService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.sizeService.getSize(params.id).then(
                data => this._size = data
            );
        }
    }

    onSubmit(event: Event) {
        this.sizeService
            .deleteSize(this._size!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('sizes-index', {});
            });
        event.preventDefault();
    }
}

import { ISize } from '../../domain/ISize';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

import { SizeService } from 'service/size-service';

@autoinject
export class SizeDetails {

    private _size: ISize | null = null;

    constructor(private sizeService: SizeService) {

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

}

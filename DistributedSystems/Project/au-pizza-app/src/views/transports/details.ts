import { ITransport } from '../../domain/ITransport';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

import { TransportService } from 'service/transport-service';

@autoinject
export class TransportDetails {

    private _transport: ITransport | null = null;

    constructor(private transportService: TransportService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.transportService.getTransport(params.id).then(
                data => this._transport = data
            );
        }
    }

}

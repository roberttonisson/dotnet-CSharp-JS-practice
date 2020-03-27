import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { TransportService } from 'service/transport-service';
import { ITransport } from 'domain/ITransport';

@autoinject
export class TransportsCreate {

    _cost = 0;
    _address = "";

    constructor(private transportService: TransportService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.transportService
            .createTransport({ cost: this._cost, address: this._address, id: '' })
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('transports-index', {});
            });

        event.preventDefault();
    }

}

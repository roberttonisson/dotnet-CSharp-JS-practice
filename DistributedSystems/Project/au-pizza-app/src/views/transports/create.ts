import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { TransportService } from 'service/transport-service';
import { ITransport } from 'domain/ITransport';

@autoinject
export class TransportsCreate {

    private _transport: ITransport | null = null;

    constructor(private transportService: TransportService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.transportService
            .createTransport(this._transport!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('transports-index', {});
            });

        event.preventDefault();
    }

}

import { ITransport } from '../../domain/ITransport';
import { autoinject } from 'aurelia-framework';
import { TransportService } from 'service/transport-service';

@autoinject
export class TransportsIndex {
    private _transports: ITransport[] = [];

    constructor(private transportService: TransportService) {

    }

    attached() {
        this.transportService.getTransports().then(
            data => this._transports = data
        );
    }

}

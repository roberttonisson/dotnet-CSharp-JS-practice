import { AlertType } from './../../types/AlertType';
import { ITransport } from './../../domain/ITransport';
import { autoinject } from 'aurelia-framework';
import { TransportService } from 'service/transport-service';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class TransportsIndex {
    private _transports: ITransport[] = [];

    private _alert: IAlertData | null = null;

    constructor(private transportService: TransportService) {

    }

    attached() {
        this.transportService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._transports = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

}

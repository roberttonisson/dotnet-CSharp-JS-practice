import { AlertType } from './../../types/AlertType';
import { ISize } from './../../domain/ISize';
import { autoinject } from 'aurelia-framework';
import { SizeService } from 'service/size-service';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class SizesIndex {
    private _sizes: ISize[] = [];

    private _alert: IAlertData | null = null;

    constructor(private sizeService: SizeService) {

    }

    attached() {
        this.sizeService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._sizes = response.data!;
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

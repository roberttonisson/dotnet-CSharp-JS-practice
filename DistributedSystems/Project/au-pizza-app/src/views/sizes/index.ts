import { ISize } from '../../domain/ISize';
import { autoinject } from 'aurelia-framework';
import { SizeService } from 'service/size-service';

@autoinject
export class SizesIndex {
    private _sizes: ISize[] = [];

    constructor(private sizeService: SizeService) {

    }

    attached() {
        this.sizeService.getSizes().then(
            data => this._sizes = data
        );
    }

}

import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { AccountResources } from './../../../lang/accounts';

@autoinject
export class AccountIndex {
    private langResources = AccountResources;

    private _errorMessage: string | null = null;

    constructor(private appState: AppState, private router: Router) {

    }

}

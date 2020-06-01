import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { AccountResources } from './../../../lang/accounts';

@autoinject
export class ChangePassword {
    private langResources = AccountResources;
    private _errorMessage: string | null = null;
    private _successful: boolean = false;

    private _oldPassword: string = "";
    private _password: string = "";
    private _passwordConfirm: string = "";

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        if (this._password != this._passwordConfirm) {
            this._errorMessage = "Passwords do not match"; 
            return;      
        }
        event.preventDefault();

        this.accountService.changePassword(this._oldPassword, this._password, this.appState.email!).then(
            response => {
                console.log(response);
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.email = response.data!.email
                    this._successful = true;
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' '
                        + response.errorMessage!
                }
            }
        );
    }

}

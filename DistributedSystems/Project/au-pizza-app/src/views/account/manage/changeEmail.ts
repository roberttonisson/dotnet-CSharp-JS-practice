import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { AccountResources } from './../../../lang/accounts';

@autoinject
export class ChangeEmail{
    private langResources = AccountResources;
    private _errorMessage: string | null = null;
    private _successful: boolean = false;

    private _newEmail: string = "";
    private _email: string = "";

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        if (this.appState.email == this._newEmail) {
            this._errorMessage = "Current email is the same"; 
            return;      
        }
        event.preventDefault();

        this.accountService.changeEmail(this.appState.email!, this._newEmail).then(
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

import { Router } from 'aurelia-router';
import { AppState } from './../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AccountResources } from './../../lang/accounts';

@autoinject
export class AccountRegister {
    private langResources = AccountResources;
    private _email: string = "";
    private _password: string = "";
    private _firstName: string = "";
    private _lastName: string = "";
    private _address: string = "";
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        console.log(this._email, this._password);
        event.preventDefault();

        this.accountService.register(this._email, this._password, this._firstName, this._lastName, this._address).then(
            response => {
                console.log(response);
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.email = response.data!.email
                    this.router!.navigateToRoute('home');
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' '
                        + response.errorMessage!
                }
            }
        );
    }

}

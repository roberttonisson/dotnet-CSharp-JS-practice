import { ICulture } from 'domain/ICulture';
export class AppState {
    constructor(){
    }

    //public readonly baseUrl = 'https://localhost:5001/api/v1.0/';
    public readonly baseUrl = 'https://rotoni-pizza.azurewebsites.net/api/v1.0/';

    // JavaScript Object Notation Web Token 
    // to keep track of logged in status
    // https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
    get jwt():string | null {
        return localStorage.getItem('jwt');
    }

    set jwt(value: string | null){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }

    get email():string | null {
        return localStorage.getItem('email');
    }

    set email(value: string | null){
        if (value){
            localStorage.setItem('email', value);
        } else {
            localStorage.removeItem('email');
        }
    }

    get cultures():ICulture[] | null {
        return JSON.parse(localStorage.getItem('cultures')!);
    }
    set cultures(value: ICulture[] | null){
        if (value){
            localStorage.setItem('cultures', JSON.stringify(value) );
        } else {
            localStorage.removeItem('cultures');
        }
    }

    get selectedCulture():ICulture | null {
        return JSON.parse(localStorage.getItem('selectedCulture')!);
    }
    set selectedCulture(value: ICulture | null){
        if (value){
            localStorage.setItem('selectedCulture', JSON.stringify(value));
        } else {
            localStorage.removeItem('selectedCulture');
        }
    }


}

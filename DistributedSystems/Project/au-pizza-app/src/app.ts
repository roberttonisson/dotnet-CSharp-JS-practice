import { AppState } from 'state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';


@autoinject
export class App {
    router?: Router;

    constructor(private appState: AppState) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "Toppings";

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },


          /*  { route: ['transports', 'transports/index'], name: 'transports-index', moduleId: PLATFORM.moduleName('views/transports/index'), nav: true, title: 'Transports' },
            { route: ['transports/details/:id'], name: 'transports-details', moduleId: PLATFORM.moduleName('views/transports/details'), nav: false, title: 'Transports Details' },
            { route: ['transports/edit/:id'], name: 'transports-edit', moduleId: PLATFORM.moduleName('views/transports/edit'), nav: false, title: 'Transports Edit' },
            { route: ['transports/delete/:id'], name: 'transports-delete', moduleId: PLATFORM.moduleName('views/transports/delete'), nav: false, title: 'Transports Delete' },
            { route: ['transports/create'], name: 'transports-create', moduleId: PLATFORM.moduleName('views/transports/create'), nav: false, title: 'Transports Create' },

            { route: ['toppings', 'toppings/index'], name: 'toppings-index', moduleId: PLATFORM.moduleName('views/toppings/index'), nav: true, title: 'Toppings' },
            { route: ['toppings/details/:id'], name: 'toppings-details', moduleId: PLATFORM.moduleName('views/toppings/details'), nav: false, title: 'Toppings Details' },
            { route: ['toppings/edit/:id'], name: 'toppings-edit', moduleId: PLATFORM.moduleName('views/toppings/edit'), nav: false, title: 'Toppings Edit' },
            { route: ['toppings/delete/:id'], name: 'toppings-delete', moduleId: PLATFORM.moduleName('views/toppings/delete'), nav: false, title: 'Toppings Delete' },
            { route: ['toppings/create'], name: 'toppings-create', moduleId: PLATFORM.moduleName('views/toppings/create'), nav: false, title: 'Toppings Create' },

            { route: ['sizes', 'sizes/index'], name: 'sizes-index', moduleId: PLATFORM.moduleName('views/sizes/index'), nav: true, title: 'Sizes' },
            { route: ['sizes/details/:id'], name: 'sizes-details', moduleId: PLATFORM.moduleName('views/sizes/details'), nav: false, title: 'Sizes Details' },
            { route: ['sizes/edit/:id'], name: 'sizes-edit', moduleId: PLATFORM.moduleName('views/sizes/edit'), nav: false, title: 'Sizes Edit' },
            { route: ['sizes/delete/:id'], name: 'sizes-delete', moduleId: PLATFORM.moduleName('views/sizes/delete'), nav: false, title: 'Sizes Delete' },
            { route: ['sizes/create'], name: 'sizes-create', moduleId: PLATFORM.moduleName('views/sizes/create'), nav: false, title: 'Sizes Create' },*/

           
            { route: ['pizzas', 'pizzas/index'], name: 'pizzas-index', moduleId: PLATFORM.moduleName('views/pizzas/index'), nav: true, title: 'Pizzas' },
            { route: ['drinks', 'drinks/index'], name: 'drinks-index', moduleId: PLATFORM.moduleName('views/drinks/index'), nav: true, title: 'Drinks' },
            { route: ['carts', 'carts/index'], name: 'carts-index', moduleId: PLATFORM.moduleName('views/carts/index'), nav: true, title: 'Carts' },
        ]
        );

        config.mapUnknownRoutes('views/home/index');
    }

    logoutOnClick(){
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }

}


export interface IAccountResourceStrings {
    manageAccount: string;
    changeYourAccountSettings: string;
    changePassword: string;
    changeEmail: string;
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
    currentEmail: string;
    newEmail: string;
    emailChangedSuccessfully: string;
    passwordChangedSuccessfully: string;
    register: string;
    registerAnAccount: string;
    password: string;
    firstName: string;
    lastName: string;
    address: string;
    login: string;
    use: string;
    email: string;

}
export interface IAccountResources {
    'en-GB': IAccountResourceStrings;
    'et-EE': IAccountResourceStrings;
}
export const AccountResources: IAccountResources = {
    'en-GB':{
        manageAccount: 'Manage account',
        changeYourAccountSettings: 'Change your account settings',
        changePassword: 'Change Password',
        changeEmail: 'Change Email',
        currentPassword: 'Current password',
        newPassword: 'New password',
        confirmPassword: 'Change password',
        currentEmail: 'Current email',
        newEmail: 'New email',
        emailChangedSuccessfully: 'Email changed successfully',
        passwordChangedSuccessfully: 'Password changed successfully',
        register: 'Register',
        registerAnAccount: 'Register an account',
        password: 'Password',
        firstName: 'First name',
        lastName: 'Last name',
        address: 'Address',
        login: 'Log In',
        use: 'Use a local account to log in:',
        email: 'Email',

    },
    'et-EE': {
        manageAccount: 'Kasutaja seaded',
        changeYourAccountSettings: 'Muuda oma kasutaja andmeid',
        changePassword: 'Muuda parooli',
        changeEmail: 'Muuda e-maili',
        currentPassword: 'Praegune parool',
        newPassword: 'Uus parool',
        confirmPassword: 'Kinnita parool',
        currentEmail: 'Praegune email ',
        newEmail: 'Uus email',
        emailChangedSuccessfully: 'Email vahetati edukalt',
        passwordChangedSuccessfully: 'Parrol vahetati edukalt',
        register: 'Registreeru',
        registerAnAccount: 'Registreerige kasutaja',
        password: 'Parool',
        firstName: 'Eesnimi',
        lastName: 'Perenimi',
        address: 'Aadress',
        login: 'Logi sisse',
        use: 'Logige oma kasutajaga sisse:',
        email: 'Email'
    },
}

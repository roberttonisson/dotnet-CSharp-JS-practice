~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp


server=alpha.akaver.com;database=student2018_rotoni_pizza;user=student2018;password=student2018
server=localhost;port=3307;database=MySQLServer;user=root;password=password
~~~



run in WebApp folder
~~~
dotnet aspnet-codegenerator controller -name AdditionalToppingsController   -actions -m AdditionalTopping   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CartsController                -actions -m Cart                -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CrustsController               -actions -m Crust               -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DefaultToppingsController      -actions -m DefaultTopping      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DrinksController               -actions -m Drink               -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DrinkInCartsController         -actions -m DrinkInCart         -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoicesController             -actions -m Invoice             -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceLinesController         -actions -m InvoiceLine         -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaInCartsController         -actions -m PizzaInCart         -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaRestaurantsController     -actions -m PizzaRestaurant     -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaTypesController           -actions -m PizzaType           -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SizesController                -actions -m Size                -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ToppingsController             -actions -m Topping             -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TransportsController           -actions -m Transport           -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PartyOrdersController          -actions -m PartyOrder          -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PartyOrderInvoicesController   -actions -m PartyOrderInvoice   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

Generate Identity UI
~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext  -f  
~~~


~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~

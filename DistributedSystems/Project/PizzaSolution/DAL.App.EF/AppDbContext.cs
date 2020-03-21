using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<AdditionalTopping> AdditionalToppings { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<Crust> Crusts { get; set; } = default!;
        public DbSet<DefaultTopping> DefaultToppings { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<DrinkInCart> DrinkInCarts { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
        public DbSet<PizzaInCart> PizzaInCarts { get; set; } = default!;
        public DbSet<PizzaRestaurant> PizzaRestaurants { get; set; } = default!;
        public DbSet<PizzaType> PizzaTypes { get; set; } = default!;
        public DbSet<Size> Sizes { get; set; } = default!;
        public DbSet<Topping> Toppings { get; set; } = default!;
        public DbSet<Transport> Transports { get; set; } = default!;
        
        public DbSet<PartyOrder> PartyOrders { get; set; } = default!;
        
        public DbSet<PartyOrderInvoice> PartyOrderInvoices { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
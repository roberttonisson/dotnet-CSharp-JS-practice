using System;
using System.Data.Entity;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public Microsoft.EntityFrameworkCore.DbSet<AdditionalTopping> AdditionalToppings { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Cart> Carts { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Crust> Crusts { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<DefaultTopping> DefaultToppings { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Drink> Drinks { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<DrinkInCart> DrinkInCarts { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Invoice> Invoices { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<PizzaInCart> PizzaInCarts { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<PizzaType> PizzaTypes { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Size> Sizes { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Topping> Toppings { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<Transport> Transports { get; set; } = default!;
        
        public Microsoft.EntityFrameworkCore.DbSet<PartyOrder> PartyOrders { get; set; } = default!;
        
        public Microsoft.EntityFrameworkCore.DbSet<PartyOrderInvoice> PartyOrderInvoices { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasIndex(u => u.UserId).IsUnique();
        }
        
    }
}
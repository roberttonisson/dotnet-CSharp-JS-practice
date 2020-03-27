using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.AdditionalTopping> AdditionalTopping { get; set; }

        public DbSet<Domain.Cart> Cart { get; set; }

        public DbSet<Domain.Crust> Crust { get; set; }

        public DbSet<Domain.DefaultTopping> DefaultTopping { get; set; }

        public DbSet<Domain.Drink> Drink { get; set; }

        public DbSet<Domain.DrinkInCart> DrinkInCart { get; set; }

        public DbSet<Domain.Invoice> Invoice { get; set; }

        public DbSet<Domain.InvoiceLine> InvoiceLine { get; set; }

        public DbSet<Domain.PizzaInCart> PizzaInCart { get; set; }

        public DbSet<Domain.PizzaType> PizzaType { get; set; }

        public DbSet<Domain.Size> Size { get; set; }

        public DbSet<Domain.Topping> Topping { get; set; }

        public DbSet<Domain.Transport> Transport { get; set; }

        public DbSet<Domain.PartyOrder> PartyOrder { get; set; }

        public DbSet<Domain.PartyOrderInvoice> PartyOrderInvoice { get; set; }
    }

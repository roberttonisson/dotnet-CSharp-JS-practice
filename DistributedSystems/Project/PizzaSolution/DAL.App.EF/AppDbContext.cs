using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain;
using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly IUserNameProvider _userNameProvider;


        public DbSet<AdditionalTopping> AdditionalToppings { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<Crust> Crusts { get; set; } = default!;
        public DbSet<DefaultTopping> DefaultToppings { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<DrinkInCart> DrinkInCarts { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
        public DbSet<PizzaInCart> PizzaInCarts { get; set; } = default!;
        public DbSet<PizzaType> PizzaTypes { get; set; } = default!;
        public DbSet<Size> Sizes { get; set; } = default!;
        public DbSet<Topping> Toppings { get; set; } = default!;
        public DbSet<Transport> Transports { get; set; } = default!;
        public DbSet<PartyOrder> PartyOrders { get; set; } = default!;
        public DbSet<PartyOrderInvoice> PartyOrderInvoices { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = default!;
        public DbSet<NewProduct> NewProducts { get; set; } = default!;

        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider) : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

/*
            // enable cascade delete on GpsSession->GpsLocation
            builder.Entity<GpsSession>()
                .HasMany(s => s.GpsLocations)
                .WithOne(l => l.GpsSession!)
                .OnDelete(DeleteBehavior.Cascade);


            // indexes
            builder.Entity<GpsSession>().HasIndex(i => i.CreatedAt);
            builder.Entity<GpsLocation>().HasIndex(i => i.CreatedAt);*/
        }

        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)
                {
                    entityWithMetaData.CreatedAt = DateTime.Now;
                    entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                    entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                    entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
                }
                
                
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)
                {
                    entityWithMetaData.ChangedAt = DateTime.Now;
                    entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                    // do not let changes on these properties get into generated db sentences - db keeps old values
                    entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                    entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
                }
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}
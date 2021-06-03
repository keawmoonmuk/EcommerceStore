using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAddregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //constucter create
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        //crate dbset
        public DbSet<Product> Products { get; set; }            //add product
        public DbSet<ProductBrand> ProductBrands { get; set; }      // add product brand
        public DbSet<ProductType> ProductTypes { get; set; }     // add product types
        public DbSet<Order> Orders { get; set; }        //add dbset Order
        public DbSet<OrderItem> OrderItems { get; set; }    // add orderItem
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }   // add delivery method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType  
                    == typeof(decimal));

                    var dateTimeProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset));
                        

                    foreach(var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }
                    
                    
                    foreach(var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
using Amazone.Core.Entities.Data;
using Amazone.Core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Repository.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }
        public DbSet<Product> Products { get; set; }
        public DbSet <ProductBrand> ProductBrands { get; set; }    
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<DeliveryMethod> deliveryMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}

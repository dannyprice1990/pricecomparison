using Chetail.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chetail.API.Data
{
    public class AppDBContext: IdentityDbContext<IdentityUser>
    {
        public AppDBContext()
            : base("ChetailDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        // Product
        public DbSet<Product> Products { get; set; }
        // Product Categories
        public DbSet<ProductCategory> ProductCategories { get; set; }
        // Wholesaler
        public DbSet<Wholesaler> Wholesalers { get; set; }
        // Retailer
        public DbSet<Retailer> Retailers { get; set; }
        // ProductPrices
        public DbSet<ProductPrice> ProductPrices { get; set; }
    }
}
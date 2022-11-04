using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using cloud_db.Domain;
using Azure.Identity;

namespace cloud_db.DAL
{
    public class AssignmentContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetail> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToContainer("Users")
                .HasPartitionKey(u => u.Id)
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);

            modelBuilder.Entity<Order>()
                .ToContainer("Orders")
                .HasPartitionKey("UserId")
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order);

            modelBuilder.Entity<Product>()
                .ToContainer("Products")
                .HasPartitionKey(p => p.Id)
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product);

            modelBuilder.Entity<OrderDetail>()
                .ToContainer("OrderDetails")
                .HasPartitionKey(od => od.OrderId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            ConfigWrapper config = new(new ConfigurationBuilder()
                .AddAzureKeyVault(new Uri("https://cloud-db.vault.azure.net/"), new DefaultAzureCredential())
                .Build());

            Environment.SetEnvironmentVariable("DBNAME", config.DBNAME);
            Environment.SetEnvironmentVariable("DBHost", config.DBHOST);
            Environment.SetEnvironmentVariable("DBCONNECTION", config.DBCONNECTION);

            builder.UseCosmos(
                Environment.GetEnvironmentVariable("DBHost"),
                Environment.GetEnvironmentVariable("DBCONNECTION"),
                databaseName: Environment.GetEnvironmentVariable("DBNAME"));

            builder.UseLazyLoadingProxies();
            builder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));

        }
    }

    public class ConfigWrapper
    {
        private readonly IConfiguration _config;

        public ConfigWrapper(IConfiguration config)
        {
            _config = config;

        }

        public string DBNAME
        {
            get { return _config["DBNAME"]; }
        }

        public string DBHOST
        {
            get { return _config["DBHOST"]; }

        }

        public string DBCONNECTION
        {
            get { return _config["DBCONNECTION"]; }
        }


    }

}


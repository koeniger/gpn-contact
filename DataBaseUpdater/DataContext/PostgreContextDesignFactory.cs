using System;
using System.Collections.Generic;
using System.Text;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DataBaseUpdater.DataContext
{

    // Необходим для выполениея миграции  Миграции для Postgre
    class PostgreContextDesignFactory : IDesignTimeDbContextFactory<PostgreDbContext>
    {
        public PostgreDbContext CreateDbContext(string[] args)
        {
            var configuration = Helpers.ReadConfigFromAppconfig();
            var connectionString = configuration.GetConnectionString(StringResources.ConnectionStringName);
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new PostgreDbContext(optionsBuilder.Options);
        }

    }

    /// <summary>
    /// контекст для выполенния миграции
    /// </summary>
    public class PostgreDbContext : ContactContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public PostgreDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

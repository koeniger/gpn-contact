using DataContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataBaseUpdater.DataContext
{
    public sealed class UpdateContext: ContactContext
    {
        public UpdateContext(DbContextOptions options) : base(options)
        {
            try
            {
                Database.ExecuteSqlCommand(Helpers.AddDbRolesScript());
                Database.EnsureCreated();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
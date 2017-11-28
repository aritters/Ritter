﻿using Microsoft.EntityFrameworkCore;
using Ritter.Samples.Application;
using Ritter.Samples.Domain;
using Ritter.Samples.Infra.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ritter.Samples.Console
{
    static class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=RitterSample;Integrated Security=True";
            string migrationsAssembly = typeof(UnitOfWork).GetTypeInfo().Assembly.GetName().Name;
            DbContextOptionsBuilder<UnitOfWork> optionsBuilder = new DbContextOptionsBuilder<UnitOfWork>();

            optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly));

            // using (UnitOfWork uow = new UnitOfWork(optionsBuilder.Options))
            // {
            //     IEmployeeRepository repository = new EmployeeRepository(uow);
            //     IEmployeeAppService appService = new EmployeeAppService(repository);

            //     await EnsureMigrateDatabase(uow);
            //     await appService.AddValidEmployee();
            // }
        }

        private static async Task EnsureMigrateDatabase(UnitOfWork uow)
        {
            var pendingMigrations = (uow as DbContext).Database.GetPendingMigrations();

            if (pendingMigrations.Any())
                await (uow as DbContext).Database.MigrateAsync();
        }
    }
}
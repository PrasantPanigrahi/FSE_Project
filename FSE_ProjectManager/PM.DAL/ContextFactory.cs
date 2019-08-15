using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PM.DAL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<PMDbContext>
    {
        public string BasePath { get; protected set; }

        private string _environmentName;

        public PMDbContext Create()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, _environmentName);
        }

        public PMDbContext Create(DbContextFactoryOptions options)
        {
            return Create(Directory.GetCurrentDirectory(), _environmentName);
        }

        private PMDbContext Create(string basePath, string environmentName)
        {
            BasePath = basePath;
            var configuration = Configuration(basePath, environmentName);
            var connectionString = ConnectionString(configuration.Build());

            return Create(connectionString);
        }

        private PMDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"{nameof(connectionString)} is null or empty", nameof(connectionString));
            }

            return Configure(connectionString);
        }

        protected virtual IConfigurationBuilder Configuration(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appSettings.json")
                .AddJsonFile($"appSettings.{environmentName}.json", true);
                //.AddEnvironmentVariables();

            return builder;
        }

        protected virtual string ConnectionString(IConfigurationRoot configuration)
        {
            string connectionString = configuration["connectionStrings:defaultConnection"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named '(default)'.");
            }

            return connectionString;
        }

        protected virtual PMDbContext Configure(string connectionString)
        {
            Console.WriteLine($"ENVIRONMENT :{_environmentName}");
            Console.WriteLine($"CONNECTION STRING :{connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<PMDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();

            return new PMDbContext(optionsBuilder.Options);
        }

        public PMDbContext CreateDbContext(string[] args)
        {
            // https://stackoverflow.com/questions/41663537/ef-core-add-migration-debugging
            // The just-in-time debugger should prompt you to attach a debugger when it hits that line.
            // System.Diagnostics.Debugger.Launch();

            return new DbContextFactory().Create();
        }
    }
}

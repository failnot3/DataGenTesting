using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DataGenerator.Tests
{
    [TestFixture]
    public class DataGeneratorTests
    {
        [Test]
        public void EnsureDataGenerationAndInsertionCompletes()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsDem.json") // Ensure that you have the connection string setting set-up correctly
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql(connectionString)
                .Options;

            // Act & Assert
            using (var context = new DataContext(options))
            {
                context.Database.EnsureDeleted(); // Delete the existing table
                context.Database.EnsureCreated(); // Recreate the table with the required schema

                var stopwatch = Stopwatch.StartNew();
                Program.GenerateAndInsertData(context, 1000000); // Adjust the number of records as needed
                stopwatch.Stop();

                Console.WriteLine($"Data generation and insertion complete. Elapsed time: {stopwatch.Elapsed}");
                // Assert.Pass("Data generation and insertion completed successfully.");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting tests...");
            var testSuite = new DataGeneratorTests();
            testSuite.EnsureDataGenerationAndInsertionCompletes();
            Console.WriteLine("All tests completed successfully.");
        }

        public static void GenerateAndInsertData(DataContext context, int recordsToGenerate)
        {
            for (int i = 1; i < recordsToGenerate; i++)
            {
                var newData = new Data
                {
                    Name = "Name_" + i,
                    Value = i * 10,
                    CreatedAt = DateTime.UtcNow,
                    // Add sample data for additional fields
                    Field1 = "Field1_" + i,
                    Field2 = i,
                    Field3 = DateTime.UtcNow.AddDays((i % 300) % 365), // Use a smaller increment to avoid overflow
                    Field4 = "Field4_" + i,
                    Field5 = i % 2 == 0,
                    Field6 = i * 1,
                    Field7 = $"Field7_{i}",
                    Field8 = TimeSpan.FromHours(i),
                    Field9 = DateTime.UtcNow.AddHours(i % 24 ),
                    // Add more fields here...
                    Field10 = $"Field10_{i}",
                    Field11 = i * 5.5,
                    Field12 = Guid.NewGuid(),
                    Field13 = $"Field13_{i}",
                    Field14 = i % 3 == 0 ? "A" : i % 3 == 1 ? "B" : "C",
                    Field15 = DateTime.UtcNow.AddDays(i % 31),
                    // Add more fields...
                    Field16 = i * 10,
                    Field17 = TimeSpan.FromMinutes(i * 2),
                    Field18 = i % 2 == 0,
                    Field19 = $"Field19_{i}",
                    Field20 = DateTime.UtcNow.AddDays((i % 300) % 365 * 2), // Use a smaller increment to avoid overflow
                    // Add more fields...
                    Field21 = i * 0.5,
                    Field22 = $"Field22_{i}",
                    Field23 = i % 2 == 0 ? "X" : "Y",
                    Field24 = TimeSpan.FromMinutes(i * 5),
                    Field25 = DateTime.UtcNow.AddDays(i % 365), // Use a smaller increment to avoid overflow
                    // Add more fields...
                    Field26 = i * 2.5,
                    Field27 = $"Field27_{i}",
                    Field28 = i % 3 == 0 ? "P" : i % 3 == 1 ? "Q" : "R",
                    Field29 = DateTime.UtcNow.AddDays(i % 365), // Use a smaller increment to avoid overflow
                    Field30 = i * 20,
                    // Add more fields...
                    Field31 = $"Field31_{i}",
                    Field32 = i % 2 == 0,
                    Field33 = TimeSpan.FromMinutes((i * 2) %60 ),
                    Field34 = DateTime.UtcNow.AddDays(i % 365), // Use a smaller increment to avoid overflow
                    Field35 = i * 15.5,
                    // Add more fields...
                    Field36 = $"Field36_{i}",
                    Field37 = i % 3 == 0 ? "X" : i % 3 == 1 ? "Y" : "Z",
                    Field38 = DateTime.UtcNow.AddDays(i % 365), // Use a smaller increment to avoid overflow
                    Field39 = i * 25,
                    Field40 = TimeSpan.FromMinutes(i * 7)
                    // Add more fields...
                };

                context.Add(newData);
            }

            context.SaveChanges();
        }
    }

    public class DataContext : DbContext
    {
        public DbSet<Data> Data { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use the exact table name as it exists in your database
            modelBuilder.Entity<Data>().ToTable("Data");
        }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }

        // Additional fields
        public string Field1 { get; set; }
        public int Field2 { get; set; }
        public DateTime Field3 { get; set; }
        // Add more fields here...
        public string Field4 { get; set; }
        public bool Field5 { get; set; }
        public int Field6 { get; set; }
        public string Field7 { get; set; }
        public TimeSpan Field8 { get; set; }
        public DateTime Field9 { get; set; }
        // Add more fields...
        public string Field10 { get; set; }
        public double Field11 { get; set; }
        public Guid Field12 { get; set; }
        public string Field13 { get; set; }
        public string Field14 { get; set; }
        public DateTime Field15 { get; set; }
        // Add more fields...
        public int Field16 { get; set; }
        public TimeSpan Field17 { get; set; }
        public bool Field18 { get; set; }
        public string Field19 { get; set; }
        public DateTime Field20 { get; set; }
        // Add more fields...
        public double Field21 { get; set; }
        public string Field22 { get; set; }
        public string Field23 { get; set; }
        public TimeSpan Field24 { get; set; }
        public DateTime Field25 { get; set; }
        // Add more fields...
        public double Field26 { get; set; }
        public string Field27 { get; set; }
        public string Field28 { get; set; }
        public DateTime Field29 { get; set; }
        public int Field30 { get; set; }
        // Add more fields...
        public string Field31 { get; set; }
        public bool Field32 { get; set; }
        public TimeSpan Field33 { get; set; }
        public DateTime Field34 { get; set; }
        public double Field35 { get; set; }
        // Add more fields...
        public string Field36 { get; set; }
        public string Field37 { get; set; }
        public DateTime Field38 { get; set; }
        public int Field39 { get; set; }
        public TimeSpan Field40 { get; set; }
        // Add more fields...
    }
}

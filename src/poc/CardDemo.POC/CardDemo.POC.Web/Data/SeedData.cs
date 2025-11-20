using CardDemo.POC.Web.Data;
using CardDemo.POC.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardDemo.POC.Web.Data;

public static class SeedData
{
    public static async Task InitializeAsync(CardDemoDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (await context.Customers.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Seed customers
        var customers = new[]
        {
            new Customer
            {
                CustomerId = "000000001",
                FirstName = "John",
                MiddleName = "Michael",
                LastName = "Doe",
                AddressLine1 = "123 Main Street",
                AddressLine2 = "Apt 4B",
                AddressLine3 = "",
                StateCode = "NY",
                CountryCode = "USA",
                ZipCode = "10001",
                PhoneNumber1 = "555-0101",
                PhoneNumber2 = "555-0102",
                SSN = "123456789",
                GovernmentIssuedId = "DL-NY-12345678",
                DateOfBirth = new DateTime(1985, 6, 15),
                EftAccountId = "EFT001",
                PrimaryCardholderIndicator = "Y",
                FicoCreditScore = 750
            },
            new Customer
            {
                CustomerId = "000000002",
                FirstName = "Jane",
                MiddleName = "Elizabeth",
                LastName = "Smith",
                AddressLine1 = "456 Oak Avenue",
                AddressLine2 = "",
                AddressLine3 = "",
                StateCode = "CA",
                CountryCode = "USA",
                ZipCode = "90210",
                PhoneNumber1 = "555-0201",
                PhoneNumber2 = "",
                SSN = "987654321",
                GovernmentIssuedId = "DL-CA-87654321",
                DateOfBirth = new DateTime(1990, 3, 22),
                EftAccountId = "EFT002",
                PrimaryCardholderIndicator = "Y",
                FicoCreditScore = 820
            },
            new Customer
            {
                CustomerId = "000000003",
                FirstName = "Robert",
                MiddleName = "",
                LastName = "Johnson",
                AddressLine1 = "789 Pine Road",
                AddressLine2 = "Suite 200",
                AddressLine3 = "",
                StateCode = "TX",
                CountryCode = "USA",
                ZipCode = "75001",
                PhoneNumber1 = "555-0301",
                PhoneNumber2 = "555-0302",
                SSN = "456789123",
                GovernmentIssuedId = "DL-TX-45678912",
                DateOfBirth = new DateTime(1978, 11, 8),
                EftAccountId = "EFT003",
                PrimaryCardholderIndicator = "Y",
                FicoCreditScore = 680
            },
            new Customer
            {
                CustomerId = "000000004",
                FirstName = "Maria",
                MiddleName = "Carmen",
                LastName = "Garcia",
                AddressLine1 = "321 Elm Street",
                AddressLine2 = "",
                AddressLine3 = "",
                StateCode = "FL",
                CountryCode = "USA",
                ZipCode = "33101",
                PhoneNumber1 = "555-0401",
                PhoneNumber2 = "",
                SSN = "789123456",
                GovernmentIssuedId = "DL-FL-78912345",
                DateOfBirth = new DateTime(1992, 8, 30),
                EftAccountId = "EFT004",
                PrimaryCardholderIndicator = "Y",
                FicoCreditScore = 715
            },
            new Customer
            {
                CustomerId = "000000005",
                FirstName = "David",
                MiddleName = "Lee",
                LastName = "Chen",
                AddressLine1 = "555 Market Street",
                AddressLine2 = "Floor 15",
                AddressLine3 = "",
                StateCode = "WA",
                CountryCode = "USA",
                ZipCode = "98101",
                PhoneNumber1 = "555-0501",
                PhoneNumber2 = "555-0502",
                SSN = "321654987",
                GovernmentIssuedId = "DL-WA-32165498",
                DateOfBirth = new DateTime(1988, 1, 12),
                EftAccountId = "EFT005",
                PrimaryCardholderIndicator = "Y",
                FicoCreditScore = 795
            }
        };

        context.Customers.AddRange(customers);
        await context.SaveChangesAsync();

        Console.WriteLine($"Seeded {customers.Length} customers");
    }
}

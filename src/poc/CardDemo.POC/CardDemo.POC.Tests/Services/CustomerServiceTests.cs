using CardDemo.POC.Web.Data;
using CardDemo.POC.Web.Data.Entities;
using CardDemo.POC.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace CardDemo.POC.Tests.Services;

public class CustomerServiceTests
{
    private CardDemoDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<CardDemoDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new CardDemoDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task GetAllCustomersAsync_ReturnsAllCustomers()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        context.Customers.AddRange(
            new Customer
            {
                CustomerId = "000000001",
                FirstName = "John",
                LastName = "Doe",
                FicoCreditScore = 750
            },
            new Customer
            {
                CustomerId = "000000002",
                FirstName = "Jane",
                LastName = "Smith",
                FicoCreditScore = 800
            }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetAllCustomersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateCustomerAsync_ValidCustomer_CreatesCustomer()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750
        };

        // Act
        var result = await service.CreateCustomerAsync(customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.CustomerId, result.CustomerId);

        var savedCustomer = await context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
        Assert.NotNull(savedCustomer);
    }

    [Fact]
    public async Task CreateCustomerAsync_DuplicateCustomer_ThrowsException()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        context.Customers.Add(new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750
        });
        await context.SaveChangesAsync();

        var duplicateCustomer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "Jane",
            LastName = "Smith",
            FicoCreditScore = 800
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.CreateCustomerAsync(duplicateCustomer));
    }

    [Fact]
    public async Task GetCustomerAsync_ExistingCustomer_ReturnsCustomer()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetCustomerAsync("000000001");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public async Task GetCustomerAsync_NonExistentCustomer_ReturnsNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        // Act
        var result = await service.GetCustomerAsync("999999999");

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("12345678")]  // Too short
    [InlineData("12345678A")] // Contains letter
    [InlineData("1234567890")] // Too long
    public async Task CreateCustomerAsync_InvalidCustomerId_ThrowsException(string invalidId)
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = invalidId,
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.CreateCustomerAsync(customer));
    }

    [Theory]
    [InlineData(299)]  // Too low
    [InlineData(851)]  // Too high
    public async Task CreateCustomerAsync_InvalidFicoScore_ThrowsException(int invalidScore)
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = invalidScore
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.CreateCustomerAsync(customer));
    }

    [Theory]
    [InlineData("X")]  // Invalid value
    [InlineData("YES")] // Too long
    public async Task CreateCustomerAsync_InvalidPrimaryCardholderIndicator_ThrowsException(string invalidIndicator)
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750,
            PrimaryCardholderIndicator = invalidIndicator
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.CreateCustomerAsync(customer));
    }

    [Fact]
    public async Task CreateCustomerAsync_CustomerTooYoung_ThrowsException()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750,
            DateOfBirth = DateTime.Now.AddYears(-17) // 17 years old
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.CreateCustomerAsync(customer));
    }

    [Fact]
    public async Task CreateCustomerAsync_ValidAgeCustomer_CreatesCustomer()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750,
            DateOfBirth = DateTime.Now.AddYears(-25) // 25 years old
        };

        // Act
        var result = await service.CreateCustomerAsync(customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task UpdateCustomerAsync_ValidCustomer_UpdatesCustomer()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750,
            PhoneNumber1 = "555-1234"
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        // Act
        customer.PhoneNumber1 = "555-5678";
        customer.FicoCreditScore = 780;
        await service.UpdateCustomerAsync(customer);

        // Assert
        var updated = await context.Customers.FindAsync("000000001");
        Assert.NotNull(updated);
        Assert.Equal("555-5678", updated.PhoneNumber1);
        Assert.Equal(780, updated.FicoCreditScore);
    }

    [Fact]
    public async Task DeleteCustomerAsync_ExistingCustomer_DeletesCustomer()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(context, logger);

        var customer = new Customer
        {
            CustomerId = "000000001",
            FirstName = "John",
            LastName = "Doe",
            FicoCreditScore = 750
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        // Act
        await service.DeleteCustomerAsync("000000001");

        // Assert
        var deleted = await context.Customers.FindAsync("000000001");
        Assert.Null(deleted);
    }

    [Fact]
    public void FullName_WithMiddleName_ReturnsFullName()
    {
        // Arrange
        var customer = new Customer
        {
            FirstName = "John",
            MiddleName = "Robert",
            LastName = "Doe"
        };

        // Act
        var fullName = customer.FullName;

        // Assert
        Assert.Equal("John Robert Doe", fullName);
    }

    [Fact]
    public void FullName_WithoutMiddleName_ReturnsFullName()
    {
        // Arrange
        var customer = new Customer
        {
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var fullName = customer.FullName;

        // Assert
        Assert.Equal("John Doe", fullName);
    }
}

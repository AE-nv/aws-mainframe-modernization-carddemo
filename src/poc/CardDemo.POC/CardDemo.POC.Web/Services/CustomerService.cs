using CardDemo.POC.Web.Data;
using CardDemo.POC.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardDemo.POC.Web.Services;

/// <summary>
/// Service for managing customer operations
/// </summary>
public class CustomerService
{
    private readonly CardDemoDbContext _context;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(CardDemoDbContext context, ILogger<CustomerService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all customers
    /// </summary>
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .Include(c => c.Accounts)
            .ToListAsync();
    }

    /// <summary>
    /// Get customer by ID
    /// </summary>
    public async Task<Customer?> GetCustomerAsync(string customerId)
    {
        return await _context.Customers
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _logger.LogInformation("Creating customer {CustomerId}", customer.CustomerId);

        // Validate customer doesn't already exist
        var exists = await _context.Customers
            .AnyAsync(c => c.CustomerId == customer.CustomerId);
        
        if (exists)
        {
            throw new InvalidOperationException(
                $"Customer {customer.CustomerId} already exists");
        }

        // Basic business rule validations
        ValidateCustomer(customer);

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Customer {CustomerId} created successfully", customer.CustomerId);

        return customer;
    }

    /// <summary>
    /// Validate customer business rules
    /// </summary>
    private void ValidateCustomer(Customer customer)
    {
        // Customer ID must be 9 digits
        if (customer.CustomerId.Length != 9 || !customer.CustomerId.All(char.IsDigit))
        {
            throw new ArgumentException("Customer ID must be exactly 9 digits");
        }

        // FICO score must be between 300 and 850
        if (customer.FicoCreditScore < 300 || customer.FicoCreditScore > 850)
        {
            throw new ArgumentException("FICO credit score must be between 300 and 850");
        }

        // Primary cardholder indicator must be Y or N
        if (customer.PrimaryCardholderIndicator != "Y" && customer.PrimaryCardholderIndicator != "N")
        {
            throw new ArgumentException("Primary cardholder indicator must be 'Y' or 'N'");
        }

        // Date of birth validation - must be 18+ years old
        if (customer.DateOfBirth.HasValue)
        {
            var age = DateTime.Now.Year - customer.DateOfBirth.Value.Year;
            if (customer.DateOfBirth.Value > DateTime.Now.AddYears(-age)) age--;
            
            if (age < 18)
            {
                throw new ArgumentException("Customer must be at least 18 years old");
            }
        }
    }

    /// <summary>
    /// Update an existing customer
    /// </summary>
    public async Task UpdateCustomerAsync(Customer customer)
    {
        _logger.LogInformation("Updating customer {CustomerId}", customer.CustomerId);

        // Detach any existing tracked entity with the same key
        var trackedEntity = _context.ChangeTracker.Entries<Customer>()
            .FirstOrDefault(e => e.Entity.CustomerId == customer.CustomerId);
        
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
        }

        // Validate customer business rules
        ValidateCustomer(customer);

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Customer {CustomerId} updated successfully", customer.CustomerId);
    }

    /// <summary>
    /// Delete a customer
    /// </summary>
    public async Task DeleteCustomerAsync(string customerId)
    {
        _logger.LogInformation("Deleting customer {CustomerId}", customerId);

        var customer = await GetCustomerAsync(customerId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer {CustomerId} deleted successfully", customerId);
        }
    }
}

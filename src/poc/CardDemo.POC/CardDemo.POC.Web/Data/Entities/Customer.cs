namespace CardDemo.POC.Web.Data.Entities;

/// <summary>
/// Customer entity - maps to COBOL CUSTOMER-RECORD
/// Based on CUSTREC.cpy copybook
/// </summary>
public class Customer
{
    /// <summary>
    /// Customer ID - maps to COBOL CUST-ID PIC 9(09)
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// First name - maps to COBOL CUST-FIRST-NAME PIC X(25)
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Middle name - maps to COBOL CUST-MIDDLE-NAME PIC X(25)
    /// </summary>
    public string MiddleName { get; set; } = string.Empty;

    /// <summary>
    /// Last name - maps to COBOL CUST-LAST-NAME PIC X(25)
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Address line 1 - maps to COBOL CUST-ADDR-LINE-1 PIC X(50)
    /// </summary>
    public string AddressLine1 { get; set; } = string.Empty;

    /// <summary>
    /// Address line 2 - maps to COBOL CUST-ADDR-LINE-2 PIC X(50)
    /// </summary>
    public string AddressLine2 { get; set; } = string.Empty;

    /// <summary>
    /// Address line 3 - maps to COBOL CUST-ADDR-LINE-3 PIC X(50)
    /// </summary>
    public string AddressLine3 { get; set; } = string.Empty;

    /// <summary>
    /// State code - maps to COBOL CUST-ADDR-STATE-CD PIC X(02)
    /// </summary>
    public string StateCode { get; set; } = string.Empty;

    /// <summary>
    /// Country code - maps to COBOL CUST-ADDR-COUNTRY-CD PIC X(03)
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// ZIP code - maps to COBOL CUST-ADDR-ZIP PIC X(10)
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Phone number 1 - maps to COBOL CUST-PHONE-NUM-1 PIC X(15)
    /// </summary>
    public string PhoneNumber1 { get; set; } = string.Empty;

    /// <summary>
    /// Phone number 2 - maps to COBOL CUST-PHONE-NUM-2 PIC X(15)
    /// </summary>
    public string PhoneNumber2 { get; set; } = string.Empty;

    /// <summary>
    /// SSN - maps to COBOL CUST-SSN PIC 9(09)
    /// </summary>
    public string SSN { get; set; } = string.Empty;

    /// <summary>
    /// Government issued ID - maps to COBOL CUST-GOVT-ISSUED-ID PIC X(20)
    /// </summary>
    public string GovernmentIssuedId { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth - maps to COBOL CUST-DOB-YYYYMMDD PIC X(10)
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// EFT account ID - maps to COBOL CUST-EFT-ACCOUNT-ID PIC X(10)
    /// </summary>
    public string EftAccountId { get; set; } = string.Empty;

    /// <summary>
    /// Primary cardholder indicator - maps to COBOL CUST-PRI-CARD-HOLDER-IND PIC X(01)
    /// Y = Yes, N = No
    /// </summary>
    public string PrimaryCardholderIndicator { get; set; } = "Y";

    /// <summary>
    /// FICO credit score - maps to COBOL CUST-FICO-CREDIT-SCORE PIC 9(03)
    /// </summary>
    public int FicoCreditScore { get; set; }

    /// <summary>
    /// Full name (computed property for convenience)
    /// </summary>
    public string FullName => string.IsNullOrWhiteSpace(MiddleName) 
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    /// <summary>
    /// Navigation property to accounts
    /// </summary>
    public List<Account> Accounts { get; set; } = new();
}

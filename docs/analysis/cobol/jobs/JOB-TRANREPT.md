# Job Analysis: TRANREPT

## Overview
**Source File**: `app/jcl/TRANREPT.jcl`
**Frequency**: Daily / On-demand
**Type**: Transaction report generation

## Purpose
Generates formatted transaction report for specified date range. Unloads transactions, filters by date, sorts by card number, and produces detailed report with transaction types and categories.

## Processing Steps
1. **STEP05R**: Unload Transaction Master to flat file
   - Uses REPROC procedure
   - Source: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS
   - Target: AWS.M2.CARDDEMO.TRANSACT.BKUP(+1) GDG
2. **STEP05R (Sort)**: Filter and sort transactions
   - Filter: Date range (PARM-START-DATE to PARM-END-DATE)
   - Sort: By card number (16 bytes starting at position 263)
   - Output: AWS.M2.CARDDEMO.TRANSACT.DALY(+1) GDG
3. **STEP10R**: Execute CBTRN03C to produce formatted report
   - Reads filtered transactions
   - Lookups: Card X-Ref, Transaction Type, Category
   - Output: AWS.M2.CARDDEMO.TRANREPT(+1) GDG

## Data Flow
**Inputs**:
- TRANFILE: Transaction Master VSAM
- CARDXREF: Card cross-reference (for account lookup)
- TRANTYPE: Transaction type descriptions
- TRANCATG: Transaction category balances
- DATEPARM: Date parameters

**Output**: TRANREPT GDG (LRECL=133, formatted report)

## Sort Specifications (STEP05R)
**Symbolic Names**:
- TRAN-CARD-NUM: 263,16,ZD (zoned decimal, 16 bytes)
- TRAN-PROC-DT: 305,10,CH (processed date, 10 bytes)
- PARM-START-DATE: '2022-01-01' (configurable)
- PARM-END-DATE: '2022-07-06' (configurable)

**Sort Logic**:
- Primary sort: Card number (ascending)
- Filter: Processed date >= start AND <= end

## Report Program (CBTRN03C)
- Produces formatted transaction report
- LRECL=133 (wide report format, likely 132 print + 1 carriage control)
- Includes transaction details with type and category lookups

## GDG Usage
- Creates new generation for daily/filtered transactions
- Creates new generation for formatted report
- Historical report retention

## Date Parameters
- Hardcoded in JCL SYMNAMES (requires JCL edit for different dates)
- Format: YYYY-MM-DD (ISO 8601)
- Example: 2022-01-01 to 2022-07-06

## Dependencies
- REPROC procedure in PROC library
- CBTRN03C program compiled
- All input VSAM files accessible
- SORT utility
- GDG bases defined (TRANSACT.DALY, TRANREPT)

## Use Cases
- Daily transaction reports for management
- Audit reports for specified date ranges
- Customer transaction history
- Regulatory reporting
- Month-end or year-end transaction summaries

## Modernization Notes
- VSAM unload → SQL SELECT with date range
- SORT with filter → SQL WHERE and ORDER BY
- Date parameters in SYMNAMES → Application configuration or API parameters
  ```sql
  SELECT * FROM Transactions 
  WHERE ProcessedDate BETWEEN @StartDate AND @EndDate
  ORDER BY CardNumber
  ```
- CBTRN03C program → .NET report generator or SQL Server Reporting Services (SSRS)
- GDG reports → Azure Blob Storage with dated folders
- Report format (LRECL=133) → PDF, Excel, or HTML
- Consider: Power BI, Tableau, or custom web dashboard
- Reference data lookups (TRANTYPE, TRANCATG) → SQL JOINs
- Eliminate intermediate files: Stream query results to report generator
- Date selection: Web UI parameter input or API query parameters

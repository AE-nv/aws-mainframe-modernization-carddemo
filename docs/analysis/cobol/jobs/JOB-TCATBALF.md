# Job Analysis: TCATBALF

## Overview
**Source File**: `app/jcl/TCATBALF.jcl`
**Frequency**: Initial setup / Periodic refresh
**Type**: Transaction category balance file definition

## Purpose
Defines and loads Transaction Category Balance file containing balance summaries by account, transaction type, and category. Supports transaction categorization and balance reporting.

## Processing Steps
1. **STEP05**: Delete existing TCATBALF VSAM file
   - Deletes: AWS.M2.CARDDEMO.TCATBALF.VSAM.KSDS
   - Sets MAXCC=0
2. **STEP10**: Define TCATBALF VSAM KSDS
   - Key: 17 bytes at offset 0 (Account ID + Type + Category)
   - Record: 50 bytes (small balance record)
   - Space: 1 CYL primary, 5 CYL secondary
   - SHAREOPTIONS(2,3) - Multi-region update access
3. **STEP15**: Load category balance data
   - Source: AWS.M2.CARDDEMO.TCATBALF.PS
   - Target: TCATBALF VSAM

## VSAM File Specifications
- **Type**: KSDS
- **Key**: 17 bytes at offset 0
  - Account ID: 11 bytes
  - Transaction Type: 2 bytes
  - Category Code: 4 bytes (likely numeric)
- **Record**: 50 bytes (balance amount + metadata)
- **Share Options**: (2,3) - Updated by batch, read by online

## Key Structure Analysis
Composite key (17 bytes total):
- Account ID: 11 bytes (e.g., "00001000001")
- Type Code: 2 bytes (e.g., "01" = Purchase)
- Category Code: 4 bytes (e.g., "0001" = Groceries)

## Data Purpose
Maintains running balances by:
- Account
- Transaction type (Purchase, Payment, etc.)
- Category (Groceries, Gas, Restaurants, etc.)

Enables queries like:
- "Show all Purchase categories for account X"
- "Total grocery purchases for account Y"
- "Category breakdown for transaction reporting"

## Dependencies
- Source file: AWS.M2.CARDDEMO.TCATBALF.PS
- Updated by: CBTRN03C, TRANCATG.jcl job
- Referenced by: PRTCATBL.jcl (print report), CORPT00C (online reports)

## Share Options (2,3)
- Region 2: Multi-region can update (batch processes)
- Region 3: Transactional integrity, cross-region recovery
- Appropriate for balance file updated by batch, queried online

## Related Jobs
- **TRANCATG.jcl**: Updates category balances (already analyzed)
- **PRTCATBL.jcl**: Prints category balance report

## Modernization Notes
- VSAM → SQL table with composite key
  ```sql
  CREATE TABLE TransactionCategoryBalances (
      AccountId CHAR(11),
      TypeCode CHAR(2),
      CategoryCode CHAR(4),
      Balance DECIMAL(15,2),
      LastUpdated DATETIME2,
      PRIMARY KEY (AccountId, TypeCode, CategoryCode)
  );
  ```
- Composite key → SQL clustered primary key
- Balance maintenance → SQL UPDATE or MERGE during posting
- Consider: Materialized view or aggregate table
- Real-time option: Calculate balances on-the-fly with SUM() GROUP BY
- Performance: Indexed for fast category lookups
- SHAREOPTIONS → SQL transaction isolation (READ COMMITTED)
- Reporting: Power BI can query directly for category analysis
- API endpoint: GET /api/accounts/{id}/category-balances

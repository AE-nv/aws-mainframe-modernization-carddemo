# Job Analysis: PRTCATBL

## Overview
**Source File**: `app/jcl/PRTCATBL.jcl`
**Frequency**: On-demand / Monthly
**Type**: Category balance report generation

## Purpose
Prints formatted Transaction Category Balance report. Unloads TCATBALF VSAM file, sorts, formats, and produces readable report for management review.

## Processing Steps
1. **DELDEF**: Delete previous report file
   - Pre-deletes: AWS.M2.CARDDEMO.TCATBALF.REPT
2. **STEP05R**: Unload TCATBALF VSAM to flat file
   - Uses REPROC procedure
   - Source: AWS.M2.CARDDEMO.TCATBALF.VSAM.KSDS
   - Output: AWS.M2.CARDDEMO.TCATBALF.BKUP(+1) GDG
3. **STEP10R**: Sort and format category balances
   - Sort: Account ID, Type Code, Category Code (all ascending)
   - Format: Edits balance amount with decimal point
   - Output: AWS.M2.CARDDEMO.TCATBALF.REPT

## Data Flow
**Input**: AWS.M2.CARDDEMO.TCATBALF.VSAM.KSDS  
**Output**: AWS.M2.CARDDEMO.TCATBALF.REPT (formatted report, LRECL=40)

## Sort/Format Specifications (STEP10R)
**Symbolic Names**:
- TRANCAT-ACCT-ID: 1,11,ZD (Account ID)
- TRANCAT-TYPE-CD: 12,2,CH (Type Code)
- TRANCAT-CD: 14,4,ZD (Category Code)
- TRAN-CAT-BAL: 18,11,ZD (Balance amount)

**Sort Keys**:
1. Account ID (ascending)
2. Type Code (ascending)
3. Category Code (ascending)

**Format (OUTREC)**:
- Account ID + space
- Type Code + space
- Category Code + space
- Balance formatted with EDIT=(TTTTTTTTT.TT) - inserts decimal point
- Output LRECL: 40 bytes (compact formatted report)

## Report Format
Produces columnar report with:
- Account ID (11 chars)
- Type Code (2 chars)
- Category Code (4 chars)
- Balance Amount (formatted with decimal: 999999999.99)

## Use Cases
- Management reporting of category-wise spending
- Monthly transaction category analysis
- Budget monitoring by category
- Account-level category breakdown

## Dependencies
- REPROC procedure
- TCATBALF VSAM file populated
- SORT utility
- GDG TCATBALF.BKUP defined

## Modernization Notes
- VSAM unload → SQL SELECT with formatting
  ```sql
  SELECT AccountId, TypeCode, CategoryCode, 
         FORMAT(Balance, 'N2') AS FormattedBalance
  FROM TransactionCategoryBalances
  ORDER BY AccountId, TypeCode, CategoryCode
  ```
- SORT with formatting → SQL ORDER BY with CONVERT/FORMAT
- EDIT mask → SQL FORMAT or .NET string formatting
- Report generation → SQL Server Reporting Services (SSRS) or Power BI
- Consider: Crystal Reports, Telerik Reporting, or DevExpress
- Modern output: PDF, Excel, HTML instead of fixed-width text
- API endpoint: GET /api/reports/category-balances (returns JSON)
- Eliminate intermediate files: Stream SQL results to report formatter
- Scheduled execution: Azure Logic Apps or Azure Functions with timer trigger

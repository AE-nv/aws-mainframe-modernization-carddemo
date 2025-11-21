# Program Analysis: CBTRN03C

## Overview
**Source File**: `app/cbl/CBTRN03C.cbl`  
**Type**: Batch Reporting  
**Module**: Transaction Reporting

## Business Purpose
Generates detailed transaction reports with page totals, account totals, and grand totals for a specified date range. Enriches transaction data with card cross-reference information, transaction type descriptions, and category descriptions.

## Key Logic
1. Reads date range parameters from input file
2. Processes transaction file sequentially
3. For each transaction:
   - Filters by date range (TRAN-PROC-TS between start/end dates)
   - Looks up card cross-reference data (account ID, customer ID)
   - Looks up transaction type description
   - Looks up transaction category description
   - Writes formatted detail line to report
4. Generates:
   - Page headers (repeating every 20 lines)
   - Transaction detail lines
   - Page totals (every 20 lines)
   - Account totals (when card number changes)
   - Grand totals (end of report)

## Data Dependencies

**Key Copybooks**:
- `CVTRA05Y` - Transaction record layout
- `CVACT03Y` - Card cross-reference record
- `CVTRA03Y` - Transaction type record
- `CVTRA04Y` - Transaction category record
- `CVTRA07Y` - Report file layout

**Files Accessed**:
- `TRANFILE` - Transaction master file (sequential read)
- `CARDXREF` - Card cross-reference file (random access)
- `TRANTYPE` - Transaction type lookup file (random access)
- `TRANCATG` - Transaction category lookup file (random access)
- `TRANREPT` - Report output file (sequential write)
- `DATEPARM` - Date parameter input file (sequential read)

## Program Relationships
**Calls**: None  
**Called By**: Transaction reporting jobs (TRANREPT.jcl)

## Notable Patterns
- Date range filtering (read from parameter file)
- Report pagination (20 lines per page)
- Multi-level totaling (page, account, grand)
- Data enrichment (multiple lookup files)
- Break logic (account total when card number changes)
- Running totals maintained in working storage
- Formatted report output with headers and detail lines
- Uses 1500-series paragraphs for lookup operations
- Comprehensive error handling for invalid keys

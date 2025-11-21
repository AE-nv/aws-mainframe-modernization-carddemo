# Program Analysis: CBIMPORT

## Overview
**Source File**: `app/cbl/CBIMPORT.cbl`  
**Type**: Batch Data Migration Import  
**Module**: Data Management / Migration

## Business Purpose
Imports complete customer profiles from a branch migration export file, splitting the multi-record export format into separate normalized target files. Used for migrating customer data from other branches or systems into CardDemo. Validates data integrity and generates import statistics.

## Key Logic
1. Reads multi-record export file sequentially
2. For each record, evaluates record type:
   - 'C' = Customer record → write to CUSTOMER-OUTPUT
   - 'A' = Account record → write to ACCOUNT-OUTPUT
   - 'X' = Cross-reference record → write to XREF-OUTPUT
   - 'T' = Transaction record → write to TRANSACTION-OUTPUT
   - 'D' = Card record → write to CARD-OUTPUT
   - Other = Unknown record → write to ERROR-OUTPUT
3. Maps fields from export format to target file format
4. Tracks import statistics by record type
5. Generates error records for unknown types
6. Produces summary report with counts

## Data Dependencies

**Key Copybooks**:
- `CVEXPORT` - Export file multi-record layout
- `CVCUS01Y` - Customer output record
- `CVACT01Y` - Account output record
- `CVACT03Y` - Cross-reference output record
- `CVTRA05Y` - Transaction output record
- `CVACT02Y` - Card output record

**Files Accessed**:
- `EXPFILE` - Export input file (indexed, sequential read)
- `CUSTOUT` - Customer output file (sequential write)
- `ACCTOUT` - Account output file (sequential write)
- `XREFOUT` - Cross-reference output file (sequential write)
- `TRNXOUT` - Transaction output file (sequential write)
- `CARDOUT` - Card output file (sequential write)
- `ERROUT` - Error output file (sequential write)

## Program Relationships
**Calls**: None  
**Called By**: Data migration jobs (CBIMPORT.jcl)

## Notable Patterns
- Record type switching (EVALUATE on EXPORT-REC-TYPE)
- Multi-file output (writes to 6 different files)
- Data denormalization/splitting (one input → multiple outputs)
- Error logging for unknown record types
- Statistics tracking (counters for each record type)
- Timestamp capture (FUNCTION CURRENT-DATE)
- Field-by-field mapping between different layouts
- Comprehensive import reporting

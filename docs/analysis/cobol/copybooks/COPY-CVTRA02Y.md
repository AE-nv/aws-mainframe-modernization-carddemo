# Copybook Analysis: CVTRA02Y

## Overview
**Source File**: `app/cpy/CVTRA02Y.cpy`  
**Type**: Lookup/Reference Data  
**Used By**: Transaction processing and account management programs

## Purpose
Defines the disclosure group record structure (50-byte record length) that links account groups, transaction types, and categories to interest rates.

## Structure Overview
Composite key structure with account group ID, transaction type code, and transaction category code, associated with an interest rate.

## Key Fields
- `DIS-GROUP-KEY` - Composite key containing:
  - `DIS-ACCT-GROUP-ID` - Account group identifier (10 characters)
  - `DIS-TRAN-TYPE-CD` - Transaction type code (2 characters)
  - `DIS-TRAN-CAT-CD` - Transaction category code (4 digits)
- `DIS-INT-RATE` - Interest rate (signed, 4 digits + 2 decimal places, COMP-3)

## Notable Patterns
- Composite key (group + type + category)
- COMP-3 packed decimal for interest rate (efficient storage)
- Used for interest rate determination based on transaction characteristics

## Usage Context
Used primarily in interest calculation (CBACT04C) and potentially in account management programs for rate lookup and disclosure purposes.

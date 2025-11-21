# Copybook Analysis: CVTRA03Y

## Overview
**Source File**: `app/cpy/CVTRA03Y.cpy`  
**Type**: Lookup/Reference Data  
**Used By**: Transaction processing and reporting programs

## Purpose
Defines the transaction type lookup record (60-byte record length) that maps 2-character transaction type codes to descriptive names.

## Structure Overview
Simple key-value structure with transaction type code and corresponding description.

## Key Fields
- `TRAN-TYPE` - 2-character transaction type code (primary key)
- `TRAN-TYPE-DESC` - Transaction type description (50 characters)

## Notable Patterns
- Reference data for transaction classification
- Used to enrich reports with human-readable type names
- Short code (2 chars) for efficient storage in transaction records

## Usage Context
Used extensively in transaction reporting (CBTRN03C) to display transaction type descriptions, and in transaction processing programs for validation and categorization.

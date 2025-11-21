# Copybook Analysis: CVTRA04Y

## Overview
**Source File**: `app/cpy/CVTRA04Y.cpy`  
**Type**: Lookup/Reference Data  
**Used By**: Transaction processing and reporting programs

## Purpose
Defines the transaction category record structure (60-byte record length) with composite key of transaction type and category code, providing detailed categorization descriptions.

## Structure Overview
Two-level hierarchy: transaction type + category code, with descriptive text for the category.

## Key Fields
- `TRAN-CAT-KEY` - Composite key containing:
  - `TRAN-TYPE-CD` - Transaction type code (2 characters)
  - `TRAN-CAT-CD` - Transaction category code (4 digits)
- `TRAN-CAT-TYPE-DESC` - Category description (50 characters)

## Notable Patterns
- Two-level categorization (type → category)
- Used in conjunction with CVTRA03Y for complete transaction classification
- Enables detailed transaction reporting and analysis

## Usage Context
Used in transaction reporting (CBTRN03C) to provide detailed category descriptions, transaction category balance processing (CBTRN03C), and anywhere detailed transaction classification is needed.

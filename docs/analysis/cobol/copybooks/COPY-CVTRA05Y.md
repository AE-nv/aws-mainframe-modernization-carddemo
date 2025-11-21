# Copybook Analysis: CVTRA05Y

## Overview
**Source File**: `app/cpy/CVTRA05Y.cpy`  
**Type**: Entity Data Structure  
**Used By**: All transaction processing programs (350-byte record length)

## Purpose
Defines the complete transaction master record structure containing all transaction details including merchant information, amounts, timestamps, and categorization.

## Structure Overview
Comprehensive transaction record with identification, classification (type + category), source, description, amount, merchant details (ID, name, city, zip), card number, and dual timestamps (origination + processing).

## Key Fields
- `TRAN-ID` - Transaction identifier (16 characters, primary key)
- `TRAN-TYPE-CD` - Transaction type code (2 characters)
- `TRAN-CAT-CD` - Transaction category code (4 digits)
- `TRAN-SOURCE` - Transaction source (10 characters: online, POS, ATM, etc.)
- `TRAN-DESC` - Transaction description (100 characters)
- `TRAN-AMT` - Transaction amount (signed, 9 digits + 2 decimal places)
- `TRAN-MERCHANT-ID` - Merchant identifier (9 digits)
- `TRAN-MERCHANT-NAME` - Merchant name (50 characters)
- `TRAN-MERCHANT-CITY` - Merchant city (50 characters)
- `TRAN-MERCHANT-ZIP` - Merchant zip code (10 characters)
- `TRAN-CARD-NUM` - Card number (16 characters)
- `TRAN-ORIG-TS` - Transaction origination timestamp (26 characters)
- `TRAN-PROC-TS` - Transaction processing timestamp (26 characters)

## Notable Patterns
- Dual timestamps (origination vs. processing)
- Complete merchant information for fraud detection
- Signed amount field (supports debits and credits)
- Linked to card number for cross-reference

## Usage Context
Core transaction record used in all transaction processing: online transaction add (COTRN02C), transaction inquiry (COTRN00C, COTRN01C), transaction posting (CBTRN02C), transaction validation (CBTRN01C), reporting (CBTRN03C), and data migration (CBIMPORT, CBEXPORT).

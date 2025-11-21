# Copybook Analysis: CVACT03Y

## Overview
**Source File**: `app/cpy/CVACT03Y.cpy`  
**Type**: Cross-Reference Data Structure  
**Used By**: All programs requiring card-to-account-to-customer mapping

## Purpose
Defines the card cross-reference record (50-byte fixed length) that links cards to accounts and customers. Critical for maintaining referential integrity across the three main entities.

## Structure Overview
Simple flat structure with three key fields linking card number to both customer ID and account ID.

## Key Fields
- `XREF-CARD-NUM` - 16-character card number (primary key)
- `XREF-CUST-ID` - Customer ID (9 digits)
- `XREF-ACCT-ID` - Account ID (11 digits)

## Notable Patterns
- Composite relationship (card → customer + account)
- Used for lookups in transaction processing
- Essential for data integrity validation

## Usage Context
Used extensively in transaction validation (CBTRN01C, CBTRN03C), online inquiries, batch utilities (CBACT03C), and data migration (CBIMPORT, CBEXPORT).

# Copybook Analysis: CVEXPORT

## Overview
**Source File**: `app/cpy/CVEXPORT.cpy`  
**Type**: Multi-Record Export File Layout  
**Used By**: Data migration programs (CBIMPORT, CBEXPORT)

## Purpose
Defines the comprehensive multi-record export file structure (500-byte fixed length) that consolidates all CardDemo entities into a single export file for branch migration. Uses REDEFINES to support multiple record types in one file.

## Structure Overview
Base record with common header fields (record type, timestamp, sequence number, branch ID, region code) plus 460-byte data area that REDEFINES for different entity types.

## Base Record Fields
- `EXPORT-REC-TYPE` - 1-character record type indicator ('C', 'A', 'X', 'T', 'D')
- `EXPORT-TIMESTAMP` - 26-character timestamp (YYYY-MM-DD HH:MM:SS.FF)
- `EXPORT-SEQUENCE-NUM` - 9-digit sequence number (COMP for efficiency)
- `EXPORT-BRANCH-ID` - 4-character branch identifier
- `EXPORT-REGION-CODE` - 5-character region code
- `EXPORT-RECORD-DATA` - 460-byte data area (base)

## REDEFINES Structures
Each REDEFINES `EXPORT-RECORD-DATA` for specific entity:

1. **EXPORT-CUSTOMER-DATA** ('C' records) - From CVCUS01Y:
   - Customer ID, name fields, addresses, phones, SSN, DOB, credit score, etc.
   
2. **EXPORT-ACCOUNT-DATA** ('A' records) - From CVACT01Y:
   - Account ID, status, balances, limits, dates, zip, group ID
   
3. **EXPORT-TRANSACTION-DATA** ('T' records) - From CVTRA05Y:
   - Transaction ID, type, category, amount, merchant info, timestamps
   
4. **EXPORT-CARD-XREF-DATA** ('X' records) - From CVACT03Y:
   - Card number, customer ID, account ID
   
5. **EXPORT-CARD-DATA** ('D' records) - From CVACT02Y:
   - Card number, account ID, CVV, embossed name, expiration, status

## Notable Patterns
- Single file format for multiple entity types
- Record type discrimination via 1-character flag
- Extensive use of REDEFINES for polymorphic record structure
- COMP and COMP-3 fields for numeric data efficiency
- Consistent 500-byte record length across all types
- Filler padding ensures alignment

## Usage Context
Core structure for data migration between CardDemo systems. CBEXPORT reads normalized files and creates export file. CBIMPORT reads export file and populates normalized files. Enables branch-to-branch or system-to-system data transfer.

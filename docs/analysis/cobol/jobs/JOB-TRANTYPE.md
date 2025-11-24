# Job Analysis: TRANTYPE

## Overview
**Source File**: `app/jcl/TRANTYPE.jcl`
**Frequency**: Initial setup / Reference data update
**Type**: Reference data file definition and load

## Purpose
Defines and loads Transaction Type reference file containing transaction type codes and descriptions (e.g., "01" = Purchase, "02" = Payment, etc.).

## Processing Steps
1. **STEP05**: Delete existing TRANTYPE VSAM file
   - Deletes: AWS.M2.CARDDEMO.TRANTYPE.VSAM.KSDS
   - Sets MAXCC=0 (error suppression)
2. **STEP10**: Define TRANTYPE VSAM KSDS
   - Key: 2 bytes at offset 0 (Transaction Type Code)
   - Record: 60 bytes (code + description)
   - Space: 1 CYL primary, 5 CYL secondary
   - SHAREOPTIONS(1,4) - Read-mostly reference data
3. **STEP15**: Load transaction type data
   - Source: AWS.M2.CARDDEMO.TRANTYPE.PS (flat file)
   - Target: TRANTYPE VSAM

## VSAM File Specifications
- **Type**: KSDS (Key-Sequenced Data Set)
- **Key**: 2 bytes at offset 0 (Type Code: "01", "02", "03", etc.)
- **Record**: 60 bytes (includes description text)
- **Share Options**: (1,4) - Single updater, multiple readers (reference data pattern)
- **Purpose**: Lookup table for transaction type descriptions

## Data Characteristics
- Small file (reference data, probably < 50 records)
- Rarely updated (static reference data)
- Heavily read (lookup on every transaction display/report)
- Key examples: "01" = Purchase, "02" = Payment, "03" = Cash Advance

## Share Options Analysis
- SHAREOPTIONS(1,4) indicates:
  - Only one region can update at a time
  - Multiple regions can read simultaneously
  - Appropriate for reference data rarely updated

## Dependencies
- Source flat file: AWS.M2.CARDDEMO.TRANTYPE.PS
- Must exist before online/batch programs access it
- Referenced by: COTRN00C, COTRN01C, CBTRN03C, CORPT00C

## Usage in Application
- Online transaction list displays (COTRN00C)
- Transaction detail screens (COTRN01C)
- Transaction reports (CBTRN03C, CORPT00C)
- Lookup: Read TRANTYPE by type code, display description

## Modernization Notes
- VSAM reference file → SQL lookup table
  ```sql
  CREATE TABLE TransactionTypes (
      TypeCode CHAR(2) PRIMARY KEY,
      Description VARCHAR(58),
      IsActive BIT DEFAULT 1
  );
  ```
- Rarely updated → Excellent candidate for caching
- Consider:
  - Application-level cache (MemoryCache, Redis)
  - Database view or table
  - Configuration file or embedded resource
- SHAREOPTIONS(1,4) → Read-only cache, refresh on update
- Small dataset → Load once at startup, cache in memory
- API: GET /api/reference/transaction-types
- Modern pattern: Enum or constant with database backup
- Seed data: Include in database migration scripts

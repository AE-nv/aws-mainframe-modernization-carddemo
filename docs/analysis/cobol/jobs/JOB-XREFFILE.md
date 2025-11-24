# Job Analysis: XREFFILE

## Overview
**Source File**: `app/jcl/XREFFILE.jcl`
**Frequency**: Initial setup / On-demand
**Type**: VSAM file definition with alternate index

## Purpose
Defines Card Cross-Reference VSAM file linking cards to accounts with bidirectional access (by card number or account ID).

## Processing Steps
1. **STEP05**: Delete existing CARDXREF VSAM and AIX
   - Base: AWS.M2.CARDDEMO.CARDXREF.VSAM.KSDS
   - AIX: AWS.M2.CARDDEMO.CARDXREF.VSAM.AIX
2. **STEP10**: Define CARDXREF base cluster
   - Primary key: 16 bytes at offset 0 (Card Number)
   - Record: 50 bytes (small lookup record)
   - Space: 1 primary, 5 secondary cylinders
3. **STEP15**: Load cross-reference data
   - Source: AWS.M2.CARDDEMO.CARDXREF.PS
4. **STEP20**: Define alternate index on Account ID
   - AIX key: 11 bytes at offset 25 (Account ID)
   - NONUNIQUEKEY (multiple cards per account)
5. **STEP25**: Define PATH for AIX access
6. **STEP30**: Build alternate index

## Data Flow
**Input**: AWS.M2.CARDDEMO.CARDXREF.PS  
**Output**:
- AWS.M2.CARDDEMO.CARDXREF.VSAM.KSDS (base)
- AWS.M2.CARDDEMO.CARDXREF.VSAM.AIX (account index)
- AWS.M2.CARDDEMO.CARDXREF.VSAM.AIX.PATH

## VSAM File Specifications
**Base Cluster**:
- Type: KSDS
- Primary Key: 16 bytes at offset 0 (Card Number)
- Record: 50 bytes (smallest record - pure cross-reference)
- Share Options: (2,3)

**Alternate Index**:
- Key: 11 bytes at offset 25 (Account ID)
- NONUNIQUEKEY (one-to-many: account → cards)
- FREESPACE(10,20) for efficient insert/update

## Purpose in Application
Cross-reference file enables:
- Forward lookup: Card Number → Account ID
- Reverse lookup: Account ID → All Card Numbers
- Critical for account/card relationship management
- Used by card listing programs (COCRDLIC)

## Dependencies
- Source file AWS.M2.CARDDEMO.CARDXREF.PS
- No CICS file close/open (batch-only access initially)

## Modernization Notes
- VSAM → SQL: CardAccountXref table
- Columns: CardNumber CHAR(16) PK, AccountId CHAR(11)
- Alternate index → Non-clustered index on AccountId
- Small record size → efficient lookup table
- Consider denormalizing into Card table with AccountId FK
- Or maintain as separate junction table for flexibility
- NONUNIQUEKEY → SQL allows multiple cards per account (expected)

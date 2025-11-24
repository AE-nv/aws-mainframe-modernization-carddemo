# Job Analysis: TRANFILE

## Overview
**Source File**: `app/jcl/TRANFILE.jcl`
**Frequency**: Initial setup / On-demand
**Type**: VSAM file definition with alternate index

## Purpose
Defines Transaction Master VSAM file with alternate index on processed timestamp for chronological access. Highest volume file in CardDemo system.

## Processing Steps
1. **CLCIFIL**: Close CICS files TRANSACT and CXACAIX
2. **STEP05**: Delete existing transaction VSAM and AIX
   - Base cluster: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS
   - Alternate index: AWS.M2.CARDDEMO.TRANSACT.VSAM.AIX
3. **STEP10**: Define TRANSACT base cluster (KSDS)
   - Primary key: 16 bytes at offset 0 (Transaction ID)
   - Record: 350 bytes
   - Space: 1 primary, 5 secondary cylinders
4. **STEP15**: Load initial transaction data
   - Source: AWS.M2.CARDDEMO.DALYTRAN.PS.INIT
5. **STEP20**: Define alternate index on processed timestamp
   - AIX key: 26 bytes at position 304 (Processed Timestamp)
   - NONUNIQUEKEY (multiple transactions per timestamp)
6. **STEP25**: Define PATH for AIX access
7. **STEP30**: Build alternate index from base cluster
8. **OPCIFIL**: Reopen CICS files TRANSACT and CXACAIX

## Data Flow
**Input**: AWS.M2.CARDDEMO.DALYTRAN.PS.INIT (initial transaction file)  
**Output**:
- AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS (base)
- AWS.M2.CARDDEMO.TRANSACT.VSAM.AIX (timestamp index)
- AWS.M2.CARDDEMO.TRANSACT.VSAM.AIX.PATH

## VSAM File Specifications
**Base Cluster**:
- Type: KSDS
- Primary Key: 16 bytes at offset 0 (Transaction ID)
- Record: 350 bytes (largest of all VSAM files)
- Share Options: (2,3)

**Alternate Index**:
- Key: 26 bytes at offset 304 (Processed Timestamp)
- Purpose: Chronological transaction access, reporting
- NONUNIQUEKEY + UPGRADE

## CICS Integration
- CICS Files: TRANSACT (base), CXACAIX (alternate index)
- Critical for online transaction entry (COTRN02C)
- High-volume concurrent access expected

## Dependencies
- Initial data file must exist
- CICS region active for file operations
- Sufficient DASD space (high volume)

## Modernization Notes
- Base KSDS → SQL Transactions table
- Primary key: TransactionId CHAR(16)
- AIX on processed timestamp → SQL index on ProcessedTimestamp
- High volume requires partitioning strategy in SQL
- Consider time-based partitioning (monthly/yearly)
- Archival strategy needed for historical transactions
- Share options → SNAPSHOT or READ COMMITTED isolation

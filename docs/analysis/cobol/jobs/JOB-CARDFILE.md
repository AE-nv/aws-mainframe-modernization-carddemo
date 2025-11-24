# Job Analysis: CARDFILE

## Overview
**Source File**: `app/jcl/CARDFILE.jcl`
**Frequency**: Initial setup / On-demand
**Type**: VSAM file definition with alternate index

## Purpose
Defines and initializes the Card Master VSAM KSDS file with alternate index on Account ID. Coordinates CICS file closure/reopening to prevent file access conflicts during definition.

## Processing Steps
1. **CLCIFIL**: Close CICS files CARDDAT and CARDAIX
   - Uses SDSF to issue CEMT commands to CICS region
   - Prevents file-in-use errors during definition
2. **STEP05**: Delete existing CARDDATA VSAM cluster and AIX
   - Deletes base cluster and alternate index if they exist
3. **STEP10**: Define CARDDATA base cluster (KSDS)
   - Primary key: 16 bytes at position 0 (Card Number)
   - Record size: 150 bytes
   - Space: 1 cylinder primary, 5 secondary
4. **STEP15**: Load initial data from flat file
   - Source: AWS.M2.CARDDEMO.CARDDATA.PS
5. **STEP40**: Define alternate index on Account ID
   - AIX key: 11 bytes at position 16 (Account ID)
   - NONUNIQUEKEY (multiple cards per account)
   - UPGRADE (automatically maintains AIX)
6. **STEP50**: Define PATH for alternate index access
7. **STEP60**: Build alternate index from base cluster
8. **OPCIFIL**: Reopen CICS files CARDDAT and CARDAIX

## Data Flow
**Input**: AWS.M2.CARDDEMO.CARDDATA.PS  
**Output**: 
- AWS.M2.CARDDEMO.CARDDATA.VSAM.KSDS (base cluster)
- AWS.M2.CARDDEMO.CARDDATA.VSAM.AIX (alternate index)
- AWS.M2.CARDDEMO.CARDDATA.VSAM.AIX.PATH (access path)

## VSAM File Specifications
**Base Cluster**:
- Type: KSDS
- Primary Key: 16 bytes at offset 0 (Card Number)
- Record: 150 bytes
- Share Options: (2,3)

**Alternate Index**:
- Key: 11 bytes at offset 16 (Account ID)
- NONUNIQUEKEY (one-to-many relationship)
- UPGRADE enabled (automatic maintenance)

## CICS Integration
- Requires CICS file close before definition
- Requires CICS file open after completion
- CICS file names: CARDDAT, CARDAIX

## Dependencies
- CICS region CICSAWSA must be active
- Flat file AWS.M2.CARDDEMO.CARDDATA.PS must exist
- SDSF authorized for CEMT commands

## Modernization Notes
- Base KSDS → SQL table with primary key on CardNumber
- Alternate index → SQL non-clustered index on AccountId
- CICS file operations → Connection pool management
- NONUNIQUEKEY → SQL allows duplicate AccountId values
- Path definition → Standard SQL index usage (transparent to application)

# Job Analysis: ACCTFILE

## Overview
**Source File**: `app/jcl/ACCTFILE.jcl`
**Frequency**: Initial setup / On-demand
**Type**: VSAM file definition and data load

## Purpose
Defines and initializes the Account Master VSAM KSDS file with initial data from flat file source. This is a foundational setup job required before online or batch processing can begin.

## Processing Steps
1. **STEP05**: Delete existing ACCTDATA VSAM cluster (if exists)
   - Uses IDCAMS DELETE command
   - Sets MAXCC=0 if file doesn't exist (error suppression)
2. **STEP10**: Define new ACCTDATA VSAM KSDS cluster
   - Key: 11 bytes at position 0 (Account ID)
   - Record size: 300 bytes (fixed)
   - Space: 1 cylinder primary, 5 cylinders secondary
   - SHAREOPTIONS(2 3) for CICS multi-region access
3. **STEP15**: Load initial data using IDCAMS REPRO
   - Source: AWS.M2.CARDDEMO.ACCTDATA.PS (flat file)
   - Target: AWS.M2.CARDDEMO.ACCTDATA.VSAM.KSDS

## Data Flow
**Input**: AWS.M2.CARDDEMO.ACCTDATA.PS (flat file with account records)  
**Output**: AWS.M2.CARDDEMO.ACCTDATA.VSAM.KSDS (indexed VSAM file)

## VSAM File Specifications
- **Type**: KSDS (Key-Sequenced Data Set)
- **Key**: 11 bytes starting at offset 0
- **Record Length**: 300 bytes
- **Organization**: INDEXED
- **Share Options**: (2,3) - multiple region read/write with transactional integrity

## Dependencies
- Requires source flat file: AWS.M2.CARDDEMO.ACCTDATA.PS
- Must run before any online/batch programs accessing ACCTDAT

## Modernization Notes
- VSAM file → Azure SQL Database table
- IDCAMS operations → SQL DDL and bulk insert
- Key structure maps to SQL primary key (AccountId CHAR(11))
- Share options indicate need for transaction isolation in SQL

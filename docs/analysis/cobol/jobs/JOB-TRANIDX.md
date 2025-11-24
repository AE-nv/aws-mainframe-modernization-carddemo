# Job Analysis: TRANIDX

## Overview
**Source File**: `app/jcl/TRANIDX.jcl`
**Frequency**: After TRANBKP / On-demand
**Type**: Alternate index creation

## Purpose
Defines alternate index on Transaction Master VSAM file based on processed timestamp. Enables chronological transaction access for reporting and queries.

## Processing Steps
1. **STEP20**: Define alternate index on processed timestamp
   - AIX key: 26 bytes at position 304 (Processed Timestamp)
   - NONUNIQUEKEY (multiple transactions per timestamp)
   - UPGRADE enabled (automatic maintenance)
   - Space: 5 CYL primary, 1 CYL secondary
2. **STEP25**: Define PATH for AIX access
   - Links AIX to base cluster
   - Enables transparent access via timestamp
3. **STEP30**: Build alternate index from base cluster
   - Scans base cluster
   - Populates AIX with timestamp keys

## VSAM AIX Specifications
- **Base**: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS
- **AIX**: AWS.M2.CARDDEMO.TRANSACT.VSAM.AIX
- **Path**: AWS.M2.CARDDEMO.TRANSACT.VSAM.AIX.PATH
- **Key**: 26 bytes at offset 304 (Processed Timestamp)
- **Type**: NONUNIQUEKEY
- **Upgrade**: Automatic
- **Record**: 350 bytes

## Key Field Details
- **Position**: 304 (near end of 350-byte record)
- **Length**: 26 bytes (likely format: YYYY-MM-DD-HH.MM.SS.MMMMMM)
- **Purpose**: Chronological transaction access for reports

## Usage
- Transaction reports sorted by date/time
- Date range queries in CORPT00C
- Time-based transaction analysis
- CBTRN03C transaction reporting

## Dependencies
- Transaction VSAM base cluster must exist
- Typically runs after TRANBKP recreates empty VSAM
- Or after TRANFILE initial setup

## Execution Context
- Runs after Transaction Master recreation
- Must complete before online access resumes
- Usually part of daily EOD cycle

## Modernization Notes
- Alternate index → SQL non-clustered index on ProcessedTimestamp
- NONUNIQUEKEY → Allow duplicate timestamps (expected in SQL)
- UPGRADE → Automatic index maintenance (SQL does automatically)
- PATH → Standard SQL index usage (transparent)
- Key structure: 26 bytes → SQL DATETIME2(6) with microsecond precision
- Consider:
  ```sql
  CREATE NONCLUSTERED INDEX IX_Transaction_ProcessedTimestamp 
  ON Transactions(ProcessedTimestamp);
  ```
- BLDINDEX → CREATE INDEX statement (one-time)
- No need for separate PATH definition in SQL
- Index maintenance automatic in SQL Server

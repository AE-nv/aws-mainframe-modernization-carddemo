# Job Analysis: COMBTRAN

## Overview
**Source File**: `app/jcl/COMBTRAN.jcl`
**Frequency**: Daily (before transaction posting)
**Type**: Transaction file consolidation

## Purpose
Combines backed-up transaction file with system-generated transactions, sorts them, and loads into Transaction Master VSAM file. Prepares consolidated transaction data for daily posting.

## Processing Steps
1. **STEP05R**: Sort and combine transactions
   - **Input 1**: AWS.M2.CARDDEMO.TRANSACT.BKUP(0) - Current GDG generation
   - **Input 2**: AWS.M2.CARDDEMO.SYSTRAN(0) - System-generated transactions
   - **Sort**: Ascending by Transaction ID (16 bytes)
   - **Output**: AWS.M2.CARDDEMO.TRANSACT.COMBINED(+1) - New GDG generation
2. **STEP10**: Load combined file to VSAM
   - Uses IDCAMS REPRO
   - Source: Combined flat file
   - Target: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS

## Data Flow
**Inputs**:
- AWS.M2.CARDDEMO.TRANSACT.BKUP(0) - Backed up transactions
- AWS.M2.CARDDEMO.SYSTRAN(0) - System-generated (interest, fees)

**Output**: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS (refreshed Transaction Master)

## Sort Specifications
- Sort field: TRAN-ID (1,16,CH) - Character field, 16 bytes
- Sort order: Ascending
- Record length: 350 bytes
- Output: New GDG generation (+1)

## GDG Usage
- Input GDGs: (0) = current generation
- Output GDG: (+1) = new generation
- Automatic version management
- Retention managed by GDG limits

## Dependencies
- Both input GDG files must exist
- Transaction VSAM file must be defined
- SORT utility available
- Sufficient temp space for sort work files

## Execution Sequence
Typically runs as part of daily batch cycle:
1. TRANBKP - Backup current transactions
2. System jobs generate SYSTRAN entries (interest, fees)
3. **COMBTRAN** - Combine and load
4. POSTTRAN - Post combined transactions to accounts

## Modernization Notes
- GDG files → Azure Blob Storage with versioning or dated folders
- SORT utility → LINQ OrderBy or SQL ORDER BY
- IDCAMS REPRO → SQL MERGE or INSERT
- Consider:
  - Azure Data Factory pipeline to merge sources
  - .NET service to combine and load transactions
  - SQL: MERGE statement to upsert transactions
- System-generated transactions → Background service or Azure Function
- Eliminate intermediate files: Stream directly to database
- Transaction atomicity: Use SQL transactions for all-or-nothing load
- Monitoring: Track transaction counts, duplicates, errors

# Job Analysis: TRANBKP

## Overview
**Source File**: `app/jcl/TRANBKP.jcl`
**Frequency**: Daily (end of day)
**Type**: Transaction backup and refresh

## Purpose
Backs up Transaction Master VSAM file to GDG, then deletes and redefines empty Transaction Master for next day's processing. Critical for daily transaction cycle management.

## Processing Steps
1. **STEP05R**: Backup transaction VSAM to flat file
   - Uses REPROC procedure from AWS.M2.CARDDEMO.PROC
   - Source: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS
   - Target: AWS.M2.CARDDEMO.TRANSACT.BKUP(+1) GDG
   - Format: FB, LRECL=350, space=(CYL,(1,1))
2. **STEP05**: Delete Transaction Master VSAM
   - Deletes base cluster
   - Deletes alternate index
   - Sets MAXCC=0 for error suppression
3. **STEP10**: Redefine empty Transaction Master
   - Same specifications as original (TRANFILE.jcl)
   - Ready for next day's transactions
   - Conditional execution: COND=(4,LT)

## Data Flow
**Input**: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS (populated with today's transactions)  
**Output**: 
- AWS.M2.CARDDEMO.TRANSACT.BKUP(+1) - Backup GDG generation
- AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS (recreated empty)

## VSAM File Specifications (Redefine)
- Key: 16 bytes at offset 0
- Record: 350 bytes
- Space: 1 CYL primary, 5 CYL secondary
- Share Options: (2,3)

## Execution Context
Part of end-of-day batch cycle:
1. POSTTRAN - Post all transactions to accounts
2. **TRANBKP** - Backup and clear Transaction Master
3. COMBTRAN - Combine for next cycle (next day)

## Conditional Execution
- STEP10 has COND=(4,LT)
- Only redefines if previous steps successful
- Prevents data loss if backup fails

## Dependencies
- REPROC procedure in AWS.M2.CARDDEMO.PROC library
- Transaction VSAM must exist
- GDG TRANSACT.BKUP must be defined (see DEFGDGB.jcl)

## Risk Considerations
- **Critical Job**: Deletes production Transaction Master
- **Timing**: Must run after all transaction posting complete
- **Verification**: Ensure backup successful before delete
- **Recovery**: Backup GDG enables reconstruction if needed

## Modernization Notes
- VSAM backup → Database backup or point-in-time restore capability
- Daily delete/recreate → Partition management or archival
- Consider:
  - SQL: Move transactions to archive table (partitioned by date)
  - Keep active transactions in hot table (last 30 days)
  - Archive older data to Azure Blob Storage (Parquet format)
- GDG backup → Azure SQL automated backups or Blob snapshots
- Recreate empty VSAM → TRUNCATE or DELETE with date filter
- Modern pattern: Don't delete, just mark as archived
- Retention: Use database retention policies, not manual deletion
- Recovery: Point-in-time restore instead of GDG backup

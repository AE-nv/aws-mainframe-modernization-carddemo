# Job Analysis: DEFGDGB

## Overview
**Source File**: `app/jcl/DEFGDGB.jcl`
**Frequency**: Initial setup / One-time
**Type**: GDG base definitions (master setup)

## Purpose
Defines all GDG (Generation Data Group) bases needed by CardDemo batch processing. Centralizes GDG setup for transaction backups, reports, and system files.

## Processing Steps
1. **STEP05**: Define 6 GDG bases using IDCAMS
   - Sets MAXCC=0 after each definition (error suppression for reruns)
   - Allows job to complete even if some GDGs already exist

## GDG Bases Defined

### 1. AWS.M2.CARDDEMO.TRANSACT.BKUP
- **Limit**: 5 generations
- **Scratch**: Enabled (automatic cleanup)
- **Purpose**: Transaction Master daily backups
- **Used By**: TRANBKP.jcl

### 2. AWS.M2.CARDDEMO.TRANSACT.DALY
- **Limit**: 5 generations
- **Scratch**: Enabled
- **Purpose**: Filtered daily transaction files
- **Used By**: TRANREPT.jcl

### 3. AWS.M2.CARDDEMO.TRANREPT
- **Limit**: 5 generations
- **Scratch**: Enabled
- **Purpose**: Generated transaction reports
- **Used By**: TRANREPT.jcl

### 4. AWS.M2.CARDDEMO.TCATBALF.BKUP
- **Limit**: 5 generations
- **Scratch**: Enabled
- **Purpose**: Transaction category balance backups
- **Used By**: PRTCATBL.jcl

### 5. AWS.M2.CARDDEMO.SYSTRAN
- **Limit**: 5 generations
- **Scratch**: Enabled
- **Purpose**: System-generated transactions (interest, fees)
- **Used By**: COMBTRAN.jcl

### 6. AWS.M2.CARDDEMO.TRANSACT.COMBINED
- **Limit**: 5 generations
- **Scratch**: Enabled
- **Purpose**: Combined transaction files (user + system)
- **Used By**: COMBTRAN.jcl

## GDG Strategy
- **Uniform Limit**: All GDGs use 5 generation limit (5 days retention)
- **Scratch**: All have automatic cleanup enabled
- **Retention**: 5-day rolling history for all files
- **Error Handling**: IF LASTCC=12 THEN SET MAXCC=0 (allow reruns)

## Dependencies
- **None** - This is typically the first setup job
- Must run before any jobs that create GDG generations
- Idempotent - can rerun safely (error suppression)

## Related Jobs
Jobs depending on these GDGs:
- TRANBKP.jcl - TRANSACT.BKUP
- COMBTRAN.jcl - TRANSACT.BKUP, SYSTRAN, TRANSACT.COMBINED
- TRANREPT.jcl - TRANSACT.DALY, TRANREPT
- PRTCATBL.jcl - TCATBALF.BKUP

## Modernization Notes
- GDG concept → Cloud storage with versioning and retention policies
- All 6 GDGs → Azure Blob Storage with lifecycle management
  ```
  Container: carddemo-backups
  ├── transact-backup/YYYY/MM/DD/backup-HHmmss.dat
  ├── transact-daily/YYYY/MM/DD/daily-HHmmss.dat
  ├── reports/YYYY/MM/DD/report-HHmmss.pdf
  ├── category-backup/YYYY/MM/DD/catbal-HHmmss.dat
  ├── system-trans/YYYY/MM/DD/systran-HHmmss.dat
  └── combined-trans/YYYY/MM/DD/combined-HHmmss.dat
  ```
- **Azure Blob Lifecycle Management**:
  - Delete blobs older than 5 days
  - Automatic tier management (Hot → Cool → Archive)
- **Retention Policy**:
  ```json
  {
    "rules": [{
      "name": "deleteOldBackups",
      "type": "Lifecycle",
      "definition": {
        "actions": {
          "baseBlob": {
            "delete": { "daysAfterModificationGreaterThan": 5 }
          }
        }
      }
    }]
  }
  ```
- **Modern Alternatives**:
  - Database: Temporal tables (system-versioned)
  - SQL: Automated backup retention policies
  - Azure SQL: Point-in-time restore (up to 35 days)
- **Scratch equivalent**: Automated cleanup via retention policies
- **Generation reference**: Use timestamps or version metadata
- No explicit GDG definition needed in cloud environments

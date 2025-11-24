# Job Analysis: DEFGDGD

## Overview
**Source File**: `app/jcl/DEFGDGD.jcl`
**Frequency**: Initial setup / One-time
**Type**: GDG base definition with initial data load

## Purpose
Defines GDG bases for transaction reference data (Transaction Type, Transaction Category, Disclosure Group) and creates first generation of each with initial data. Supports reference data versioning.

## Processing Steps

### Transaction Type GDG
1. **STEP10**: Define GDG base AWS.M2.CARDDEMO.TRANTYPE.BKUP
   - Limit: 5 generations
   - Scratch enabled
2. **STEP20**: Create first generation (+1)
   - Copies AWS.M2.CARDDEMO.TRANTYPE.PS to GDG(+1)
   - LRECL=60, RECFM=FB

### Transaction Category GDG  
3. **STEP30**: Define GDG base AWS.M2.CARDDEMO.TRANCATG.PS.BKUP
   - Limit: 5 generations
   - Scratch enabled
4. **STEP40**: Create first generation (+1)
   - Copies AWS.M2.CARDDEMO.TRANCATG.PS to GDG(+1)
   - LRECL=60, RECFM=FB

### Disclosure Group GDG
5. **STEP50**: Define GDG base AWS.M2.CARDDEMO.DISCGRP.BKUP
   - Limit: 5 generations
   - Scratch enabled
6. **STEP60**: Create first generation (+1)
   - Copies AWS.M2.CARDDEMO.DISCGRP.PS to GDG(+1)
   - LRECL=50, RECFM=FB

## GDG Bases Defined

### 1. TRANTYPE.BKUP
- **Purpose**: Transaction type reference data backups
- **Limit**: 5 generations
- **Record Length**: 60 bytes
- **Source**: AWS.M2.CARDDEMO.TRANTYPE.PS

### 2. TRANCATG.PS.BKUP
- **Purpose**: Transaction category reference data backups
- **Limit**: 5 generations
- **Record Length**: 60 bytes
- **Source**: AWS.M2.CARDDEMO.TRANCATG.PS

### 3. DISCGRP.BKUP
- **Purpose**: Disclosure group reference data backups
- **Limit**: 5 generations
- **Record Length**: 50 bytes
- **Source**: AWS.M2.CARDDEMO.DISCGRP.PS

## Conditional Execution
- All data load steps have COND=(0,NE)
- Loads only execute if GDG definition successful
- Prevents partial setup

## Data Sources
Requires three reference data flat files:
1. AWS.M2.CARDDEMO.TRANTYPE.PS
2. AWS.M2.CARDDEMO.TRANCATG.PS
3. AWS.M2.CARDDEMO.DISCGRP.PS

## Use Cases
- Version control for reference data
- Backup before reference data updates
- Rollback capability for reference data changes
- Change history tracking

## Dependencies
- Source PS files must exist
- IEBGENER utility
- Sufficient DASD space
- Must run before reference data update processes

## Modernization Notes
- Reference data GDGs → Database versioning with temporal tables
- Consider:
  ```sql
  CREATE TABLE TransactionTypes (
      TypeCode CHAR(2),
      Description VARCHAR(58),
      ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START,
      ValidTo DATETIME2 GENERATED ALWAYS AS ROW END,
      PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
  ) WITH (SYSTEM_VERSIONING = ON);
  ```
- GDG generations → SQL Server temporal tables (automatic versioning)
- Azure SQL: Built-in temporal table support
- Version history: Query with FOR SYSTEM_TIME AS OF timestamp
- Backup GDGs → Database snapshot or point-in-time restore
- Reference data updates: Use database transactions with versioning
- Rollback: Restore previous version from temporal history
- Change tracking: Audit trail via temporal table history
- No separate GDG files needed: Database handles versioning natively
- Modern approach: Immutable reference data with effective dates
  ```sql
  SELECT * FROM TransactionTypes 
  FOR SYSTEM_TIME AS OF '2024-01-01';
  ```

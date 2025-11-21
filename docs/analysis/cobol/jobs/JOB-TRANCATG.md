# Job Analysis: TRANCATG

## Overview
**Source File**: `app/jcl/TRANCATG.jcl`
**Frequency**: As Needed / Initialization (can be part of daily setup when category codes change)
**Category**: Reference Data Management

## Purpose
Manages the lifecycle of the Transaction Category VSAM file: deletes any existing cluster, defines a fresh VSAM KSDS, and loads category definitions from a flat sequential file. Ensures lookup data for transaction classification is current.

## Processing Steps
1. `STEP05` – `IDCAMS DELETE`
   - Deletes existing cluster `TRANCATG.VSAM.KSDS` if present (MAXCC forced to 0)
2. `STEP10` – `IDCAMS DEFINE`
   - Defines new VSAM KSDS cluster (keys length 6, fixed record size 60)
   - Allocates DATA and INDEX components
3. `STEP15` – `IDCAMS REPRO`
   - Copies records from sequential input `TRANCATG.PS` into newly defined VSAM file

## Data Flow
`TRANCATG.PS` (sequential source) -> REPRO -> `TRANCATG.VSAM.KSDS` (lookup cluster).

## Dependencies
- Utility: `IDCAMS`
- Input File: `TRANCATG.PS` (external category definitions)
- Output: `TRANCATG.VSAM.KSDS` (usable by transaction processing programs)

## Usage Context
- Provides category codes and metadata consumed by posting (`CBTRN02C`) and reporting (`CBTRN03C`) logic
- Supports interest and fee grouping logic and analytic reporting segmentation

## Notable Characteristics
- Fully recreates dataset; implies source file is authoritative
- Key length (6) suggests compact code values (e.g., category IDs)
- ShareOptions(2,3) allows read/write concurrency during online processing

## Error Handling
- DELETE step forces MAXCC=0 to avoid job failure if file absent
- REPRO failure would leave category data unavailable (operational alert required)

## Migration Considerations
- Replace with relational lookup table (`TransactionCategory`)
- Load implemented via idempotent SQL MERGE rather than destructive recreate
- Source file ingestion could shift to configuration management or API-based update
- Add audit trail of category changes for compliance.

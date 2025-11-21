# Job Analysis: INTCALC

## Overview
**Source File**: `app/jcl/INTCALC.jcl`
**Frequency**: Monthly (end-of-cycle interest and fee computation)
**Category**: Financial Calculation

## Purpose
Computes monthly interest and fees for accounts based on accumulated transaction category balances and disclosure group parameters. Produces a new generation transaction activity file for downstream statement generation and archival.

## Processing Steps
1. `STEP15` – Execute `CBACT04C` (Interest Calculation Program)
   - Parameter (`PARM='2022071800'`) passes processing date/time stamp
   - Reads category balance file (`TCATBALF`) to aggregate totals by category
   - Reads cross-reference (`XREFFILE` and `XREFFIL1` AIX path) for card/account relationships
   - Reads account master (`ACCTFILE`) and disclosure group (`DISCGRP`) for rate and fee rules
   - Produces new transaction output generation (`SYSTRAN` via `TRANSACT DD`)
   - Applies interest/fees to account balances (updates `ACCTFILE`)

## Data Flow
`TCATBALF` + `DISCGRP` + `ACCTFILE` + `XREFFILE` -> `CBACT04C` -> Updated `ACCTFILE` + New `SYSTRAN` generation.

```
Category Balances ─┐
Disclosure Rules ─┼─> CBACT04C ──> Account Balance Updates (ACCTFILE)
Account Master ───┤                   │
XREF (Card/Acct) ─┘                   └─> SYSTRAN(+1) (interest/fee detail)
```

## Dependencies
- Program: `CBACT04C`
- Input Files: `TCATBALF`, `XREFFILE`, `XREFFIL1` (AIX path), `ACCTFILE`, `DISCGRP`
- Output: New generation `SYSTRAN` (transaction activity snapshot), updated `ACCTFILE`
- Relies on prior completion of daily posting (POSTTRAN) to ensure balances current

## Notable Characteristics
- Single-step job encapsulating business financial logic
- Uses alternate index path (`XREFFIL1`) for optimized card/account access
- Generates a point-in-time transaction activity file for statement production
- Parameter-driven processing date allows reruns/backdated calculations if needed

## Error Handling / Outputs
- New generation dataset creation isolates monthly run artifacts
- Any failures would prevent statement generation; typically rerun after correction

## Migration Considerations
- Move interest logic into scheduled service or stored procedure
- Replace VSAM files with relational tables (`Account`, `CategoryBalance`, `DisclosureGroup`, `MonthlyActivity`)
- Parameter replaced by ISO date/time or configuration service
- Maintain audit log of interest/fee computation for compliance reporting.

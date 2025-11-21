# Job Analysis: POSTTRAN

## Overview
**Source File**: `app/jcl/POSTTRAN.jcl`
**Frequency**: Daily (end-of-day transaction processing)
**Category**: Critical Business Processing

## Purpose
Posts all validated daily transactions from the staging file into the transaction master VSAM file, updates account balances, and refreshes transaction category balance data. Captures rejected transactions for audit and rework.

## Processing Steps
1. `STEP15` – Execute `CBTRN02C` (Transaction Posting Program)
   - Reads daily transaction staging file (`DALYTRAN`)
   - Validates cross-reference relationships (`XREFFILE`)
   - Updates transaction master (`TRANFILE`) with successful posts
   - Updates account master balances (`ACCTFILE`)
   - Updates category balance file (`TCATBALF`) for interest/fee computation
   - Writes rejects to new generation of `DALYREJS` (VSAM generation dataset)

## Data Flow
`DALYTRAN` (staging) -> Validation (XREFFILE + ACCTFILE) -> Posted to `TRANFILE` + Balance adjustments to `TCATBALF` -> Rejects written to `DALYREJS`.

```
DALYTRAN ─┬─> CBTRN02C ──> TRANFILE (posted)
          │              ──> ACCTFILE (balances)
          │              ──> TCATBALF (category totals)
          └───────────────> DALYREJS (rejects)
```

## Dependencies
- Program: `CBTRN02C`
- Files (Input): `DALYTRAN`, `XREFFILE`, `ACCTFILE`, `TCATBALF`, `TRANFILE`
- Files (Output/Updated): `TRANFILE`, `ACCTFILE`, `TCATBALF`, `DALYREJS(+1)`
- Requires prior completion of daily transaction validation (`CBTRN01C`) to ensure staging file integrity.

## Error Handling / Outputs
- Rejects isolated in `DALYREJS` for subsequent review job (`DALYREJS.jcl` if scheduled)
- System output directed to standard SYSOUT/SYSPRINT for operational monitoring

## Notable Characteristics
- Single-step job emphasizing embedded COBOL business logic inside `CBTRN02C`
- Uses existing VSAM clusters with shared access (all DD DISP=SHR except new rejects generation)
- Category balance maintenance embedded rather than separate post-processing

## Migration Considerations
- Replace VSAM datasets with relational tables (`Transactions`, `Accounts`, `CategoryBalances`, `DailyRejects`)
- Staging ingestion could transition to a queue or staging table
- Reject handling becomes a structured error record table for operational dashboards
- Schedule remains daily; consider event-driven trigger post business day cut-off.

# Job Analysis: CREASTMT

## Overview
**Source File**: `app/jcl/CREASTMT.JCL`
**Frequency**: Monthly (post-interest cycle)
**Category**: Statement Generation & Reporting

## Purpose
Generates monthly account/card statements in both text and HTML formats. Reorganizes transaction data by card and transaction ID to support sequential statement assembly. Produces output artifacts consumed by distribution processes.

## Processing Steps
1. `DELDEF01` – `IDCAMS` delete & redefine working transaction file
   - Deletes prior sequential (`TRXFL.SEQ`) and VSAM cluster (`TRXFL.VSAM.KSDS`)
   - Defines fresh VSAM KSDS keyed by 32-byte composite (Card Number + Transaction ID)
2. `STEP010` – `SORT`
   - Inputs: `TRANSACT.VSAM.KSDS` (full transaction master)
   - Sorts by Card (offset 263 length 16) then Transaction ID (offset 1 length 16)
   - Builds sequential file `TRXFL.SEQ` with reordered records
3. `STEP020` – `IDCAMS REPRO`
   - Loads sorted sequential data into VSAM cluster (`TRXFL.VSAM.KSDS`)
4. `STEP030` – `IEFBR14` cleanup
   - Deletes previous run statement outputs (`STATEMNT.HTML`, `STATEMNT.PS`)
5. `STEP040` – Execute `CBSTM03A`
   - Inputs: `TRXFL.VSAM`, `XREFFILE`, `ACCTFILE`, `CUSTFILE`
   - Produces new `STATEMNT.PS` (text) and `STATEMNT.HTML` (HTML) outputs
   - Calls subroutine `CBSTM03B` internally for file I/O and formatting

## Data Flow
`TRANSACT.VSAM` -> SORT -> `TRXFL.SEQ` -> REPRO -> `TRXFL.VSAM` -> CBSTM03A -> `STATEMNT.PS` + `STATEMNT.HTML`.

```
Master Transactions ──> Sort (by Card,Txn) ──> Sequential ──> VSAM (Keyed) ──┐
                                                                           │
XREF + Account + Customer ──────────────────────────────────────────────────┤
                                                                           v
                                                                   Statement Outputs
                                                                (Text & HTML formats)
```

## Dependencies
- Programs: `CBSTM03A` (main), `CBSTM03B` (subroutine)
- Input Files: `TRANSACT.VSAM.KSDS`, `XREFFILE`, `ACCTFILE`, `CUSTFILE`
- Intermediate: `TRXFL.SEQ`, `TRXFL.VSAM.KSDS`
- Output: `STATEMNT.PS`, `STATEMNT.HTML`
- Requires prior interest calculation completion (INTCALC) for accurate balances

## Notable Characteristics
- Dual-format output enabling legacy print and web distribution
- Rebuilds keyed transaction view optimized for per-card statement traversal
- Full recreation of working datasets ensures clean state each run
- Composite key strategy improves sequential access locality for statements

## Error Handling / Outputs
- Conditional execution (`COND=(0,NE)`) prevents downstream steps if prior failures
- Recreates output artifacts each run—no incremental append

## Migration Considerations
- Replace SORT/REPRO pipeline with SQL ORDER BY + indexed views
- Statement assembly via application service generating PDF/HTML
- Working VSAM structures replaced by relational staging tables
- Consider event-driven statement generation per account rather than batch monolith
- Maintain audit metadata: generation timestamp, cycle identifiers, distribution status.

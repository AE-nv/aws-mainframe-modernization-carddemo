# Copybook Analysis: CVTRA07Y

## Overview
**Source File**: `app/cpy/CVTRA07Y.cpy`  
**Type**: Report Layout Definitions  
**Used By**: Transaction reporting programs

## Purpose
Defines formatted report structures for transaction reports including headers, detail lines, and various total lines (page, account, grand totals).

## Structure Overview
Collection of fixed-format report line structures with embedded formatting, labels, and data placeholders for producing formatted transaction reports.

## Key Report Structures
- `REPORT-NAME-HEADER` - Report title and date range header
  - Short name, long name, date range labels and fields
- `TRANSACTION-DETAIL-REPORT` - Individual transaction detail line
  - Transaction ID, account ID, type code + description, category code + description, source, formatted amount
- `TRANSACTION-HEADER-1` - Column headers
- `TRANSACTION-HEADER-2` - Separator line (dashes)
- `REPORT-PAGE-TOTALS` - Page subtotal line with formatted amount
- `REPORT-ACCOUNT-TOTALS` - Account subtotal line with formatted amount
- `REPORT-GRAND-TOTALS` - Report grand total line with formatted amount

## Notable Patterns
- Pre-formatted layout structures (fixed positions for alignment)
- Edited numeric fields with commas and decimal points (e.g., `-ZZZ,ZZZ,ZZZ.ZZ`)
- Filler fields for spacing and alignment
- Dot leaders (`ALL '.'`) for total lines
- Multi-level totaling structure (page → account → grand)

## Usage Context
Used exclusively in transaction reporting program (CBTRN03C) to generate formatted transaction detail reports with proper headers, subtotals, and grand totals.

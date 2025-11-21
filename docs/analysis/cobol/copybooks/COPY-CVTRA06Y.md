# Copybook Analysis: CVTRA06Y

## Overview
**Source File**: `app/cpy/CVTRA06Y.cpy`  
**Type**: Entity Data Structure  
**Used By**: Daily transaction processing programs (350-byte record length)

## Purpose
Defines the daily transaction file record structure. Identical structure to CVTRA05Y (TRAN-RECORD) but named DALYTRAN-RECORD for the daily batch input file.

## Structure Overview
Exactly mirrors the transaction master record structure (CVTRA05Y) but used specifically for daily transaction batch input files before posting to the master transaction file.

## Key Fields
All fields identical to CVTRA05Y but with `DALYTRAN-` prefix instead of `TRAN-`:
- `DALYTRAN-ID` - Transaction identifier
- `DALYTRAN-TYPE-CD`, `DALYTRAN-CAT-CD` - Classification codes
- `DALYTRAN-SOURCE`, `DALYTRAN-DESC` - Source and description
- `DALYTRAN-AMT` - Transaction amount
- `DALYTRAN-MERCHANT-*` - Merchant information (ID, name, city, zip)
- `DALYTRAN-CARD-NUM` - Card number
- `DALYTRAN-ORIG-TS`, `DALYTRAN-PROC-TS` - Timestamps

## Notable Patterns
- Separate copybook for staging/input vs. master file
- Enables differentiation between daily input and posted transactions
- Same structure facilitates direct mapping during posting

## Usage Context
Used specifically in daily transaction validation and posting programs (CBTRN01C - validation, CBTRN02C - posting) to process daily transaction batches before updating the master transaction file.

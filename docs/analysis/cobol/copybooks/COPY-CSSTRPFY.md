# Copybook Analysis: CSSTRPFY

## Overview
**Source File**: `app/cpy/CSSTRPFY.cpy`  
**Type**: PF Key Handling Procedure  
**Used By**: All online CICS programs

## Purpose
Provides reusable procedure code for mapping CICS Attention Identifier (AID) keys to program function key indicators in the common communication area.

## Structure Overview
Complete PERFORM-able paragraph (`YYYY-STORE-PFKEY`) that evaluates `EIBAID` (CICS execute interface block AID field) and sets corresponding flags in `CCARD-AID-*` fields of COCOM01Y.

## Key Logic
Maps CICS attention keys to application-level indicators:
- DFHENTER → CCARD-AID-ENTER
- DFHCLEAR → CCARD-AID-CLEAR
- DFHPA1/PA2 → CCARD-AID-PA1/PA2
- DFHPF1-PF24 → CCARD-AID-PFK01-PFK12
  - Note: PF13-PF24 map to PFK01-PFK12 (wraparound)

## Notable Patterns
- Large EVALUATE statement (30+ WHENs)
- Handles all standard CICS AID keys
- Uses 88-level condition names from COCOM01Y
- Wraparound mapping (PF13-24 → PFK01-12)
- Self-contained, no dependencies beyond EIBAID and COCOM01Y

## Usage Context
Included via COPY in nearly all online programs to standardize function key handling. Called early in program flow after each user interaction to determine which key was pressed.

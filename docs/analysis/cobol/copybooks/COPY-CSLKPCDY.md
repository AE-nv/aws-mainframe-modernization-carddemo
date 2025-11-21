# Copybook Analysis: CSLKPCDY

## Overview
**Source File**: `app/cpy/CSLKPCDY.cpy`  
**Type**: Validation Data / Lookup Tables  
**Used By**: Programs validating US addresses and phone numbers

## Purpose
Provides comprehensive validation lookup tables for US/North America phone area codes, US state codes, and state-zip code combinations. Used to validate customer and merchant address/phone data.

## Structure Overview
Three main validation structures with 88-level condition names:
1. Phone area code validation (700+ valid codes)
2. US state code validation (56 valid codes including territories)
3. State + first 2 digits of zip code validation (250+ combinations)

## Key Fields
- `WS-US-PHONE-AREA-CODE-TO-EDIT` - 3-digit area code
  - `VALID-PHONE-AREA-CODE` - All North America area codes
  - `VALID-GENERAL-PURP-CODE` - General purpose/real area codes
  - `VALID-EASY-RECOG-AREA-CODE` - Test/demo area codes (e.g., 555)
- `US-STATE-CODE-TO-EDIT` - 2-character state code
  - `VALID-US-STATE-CODE` - All US states + DC + territories
- `US-STATE-ZIPCODE-TO-EDIT` - State + first 2 zip digits
  - `US-STATE-AND-FIRST-ZIP2` - 4-character composite (e.g., 'CA90')
  - `VALID-US-STATE-ZIP-CD2-COMBO` - Valid state+zip combinations

## Notable Patterns
- Extensive validation lists (700+ area codes, 250+ state-zip combinations)
- Separates real from test area codes
- Based on NANPA (North American Numbering Plan Administrator) data
- State-zip validation ensures geographic consistency

## Usage Context
Used in customer and account update programs (COACTUPC, COACTVWC) for validating addresses and phone numbers. Ensures data quality by rejecting invalid geographic codes.

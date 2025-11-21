# Copybook Analysis: CSUTLDPY

## Overview
**Source File**: `app/cpy/CSUTLDPY.cpy`  
**Type**: Date Validation Procedure Code  
**Used By**: Programs validating date fields

## Purpose
Provides comprehensive reusable date validation procedure code for CCYYMMDD format dates. Validates year, month, day individually and in combination (leap years, month-day combinations).

## Structure Overview
Collection of PERFORM-able paragraphs for modular date validation:
- `EDIT-DATE-CCYYMMDD` - Main validation entry point
- `EDIT-YEAR-CCYY` - Year and century validation
- `EDIT-MONTH` - Month validation (1-12)
- `EDIT-DAY` - Day validation (1-31)
- `EDIT-DAY-MONTH-YEAR` - Combined validation (leap years, month-specific day limits)
- `EDIT-DATE-LE` - Final validation using LE (Language Environment) services
- `EDIT-DATE-OF-BIRTH` - Ensures date is not in the future

## Key Logic
- Century validation (19 or 20 only)
- Month reasonableness (1-12)
- Day reasonableness based on month (28/29/30/31)
- Leap year calculation (divisible by 4, or 400 for year 00)
- Final verification using CSUTLDTC (LE date validation)
- Date of birth future date check

## Notable Patterns
- Uses GO TO for early exit (non-structured programming for efficiency)
- Comprehensive error messages via WS-RETURN-MSG
- Calls CSUTLDTC for final LE-based validation
- Uses FUNCTION INTEGER-OF-DATE for date comparison
- Works with CSUTLDWY (working storage definitions)

## Usage Context
Included in programs that validate date fields: account update, customer update, card update, transaction add. Provides bullet-proof date validation with detailed error messages.

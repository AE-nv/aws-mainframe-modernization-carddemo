# Copybook Analysis: CSUTLDWY

## Overview
**Source File**: `app/cpy/CSUTLDWY.cpy`  
**Type**: Date Validation Working Storage  
**Used By**: Programs using CSUTLDPY date validation procedures

## Purpose
Defines all working storage variables required by the date validation procedures in CSUTLDPY. Must be included alongside CSUTLDPY for date validation to function.

## Structure Overview
Hierarchical date structure with multiple REDEFINES for different data types and views, validation flags, and LE service call structures.

## Key Fields
- `WS-EDIT-DATE-CCYYMMDD` - Main date field with sub-fields:
  - `WS-EDIT-DATE-CCYY` - 4-digit year (with century/year breakout)
  - `WS-EDIT-DATE-MM` - 2-digit month (with validation flags)
  - `WS-EDIT-DATE-DD` - 2-digit day (with validation flags)
- `WS-EDIT-DATE-CCYYMMDD-N` - Numeric REDEFINES of date (9(8))
- `WS-EDIT-DATE-BINARY` - Binary date for comparisons
- `WS-CURRENT-DATE` - Current date fields (for date of birth validation)
- `WS-EDIT-DATE-FLGS` - Validation flag structure:
  - `WS-EDIT-YEAR-FLG` - Year validation status (valid/not OK/blank)
  - `WS-EDIT-MONTH` - Month validation status
  - `WS-EDIT-DAY` - Day validation status
- `WS-DATE-FORMAT` - Format string for LE services ('YYYYMMDD')
- `WS-DATE-VALIDATION-RESULT` - LE service return structure (severity, message code, result)

## Notable Patterns
- Extensive use of 88-level condition names for readable code
- Multiple REDEFINES for character/numeric views
- Binary fields for efficient date arithmetic
- Structured flags for each validation component
- LE service interface structure

## Usage Context
Always included with CSUTLDPY. Provides all storage needed for date validation logic. Used in account, customer, and card update programs.

# Copybook Analysis: CODATECN

## Overview
**Source File**: `app/cpy/CODATECN.cpy`  
**Type**: Date Conversion Interface  
**Used By**: Programs requiring date format conversion

## Purpose
Defines input/output structure for date format conversion between YYYYMMDD and YYYY-MM-DD formats (with and without separators). Appears to be interface for a date conversion utility or service.

## Structure Overview
Interface structure with input record specifying format type and date, output record specifying desired format and converted date, plus error message field.

## Key Fields

**Input Record** (`CODATECN-IN-REC`):
- `CODATECN-TYPE` - Input format indicator:
  - `YYYYMMDD-IN` (value '1') - Compact format without separators
  - `YYYY-MM-DD-IN` (value '2') - Format with dash separators
- `CODATECN-INP-DATE` - Input date (20 characters, with REDEFINES for both formats)

**Output Record** (`CODATECN-OUT-REC`):
- `CODATECN-OUTTYPE` - Output format indicator:
  - `YYYY-MM-DD-OP` (value '1') - Format with dash separators
  - `YYYYMMDD-OP` (value '2') - Compact format without separators
- `CODATECN-0UT-DATE` - Output date (20 characters, with REDEFINES for both formats)

**Error Handling**:
- `CODATECN-ERROR-MSG` - Error message (38 characters)

## Notable Patterns
- Multiple REDEFINES for flexible format access
- 88-level condition names for format selection
- Bidirectional conversion (can convert either direction)
- Error message field for validation failures
- Oversize fields (20 chars) for various date formats

## Usage Context
Utility interface for date format conversion. May be used in programs that need to convert between display formats (YYYY-MM-DD) and storage formats (YYYYMMDD), though specific usage in CardDemo codebase is unclear.

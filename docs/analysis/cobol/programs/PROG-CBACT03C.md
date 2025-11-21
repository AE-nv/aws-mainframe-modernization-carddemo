# Program Analysis: CBACT03C

## Overview
**Source File**: `app/cbl/CBACT03C.cbl`  
**Type**: Batch Utility  
**Module**: Account Management / Utilities

## Business Purpose
Utility program that reads and displays the card-to-account cross-reference file (XREFFILE) for verification and debugging. Used to validate the relationships between cards, accounts, and customers in the system.

## Key Logic
- Opens cross-reference file for sequential read
- Reads each cross-reference record sequentially
- Displays each record to output for inspection
- Handles end-of-file and error conditions
- Standard file processing loop

## Data Dependencies

**Key Copybooks**:
- `CVACT03Y` - Card cross-reference record structure

**Files Accessed**:
- `XREFFILE` - Card-to-account-to-customer cross-reference file (KSDS/VSAM, indexed, sequential access)

## Program Relationships
**Calls**: None  
**Called By**: JCL jobs (READXREF.jcl or similar administrative jobs)

## Notable Patterns
- Identical structure to CBACT02C (sequential read utility pattern)
- Standard VSAM error handling
- Uses CEE3ABD for abnormal termination
- Display-oriented (console output only)
- File status display utility (9910-DISPLAY-IO-STATUS)

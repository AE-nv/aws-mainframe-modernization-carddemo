# Program Analysis: CBCUS01C

## Overview
**Source File**: `app/cbl/CBCUS01C.cbl`  
**Type**: Batch Utility  
**Module**: Customer Management / Utilities

## Business Purpose
Utility program that reads and displays the customer master file (CUSTFILE) for verification, debugging, or data inspection. Used for operational tasks like file validation or investigating customer data issues.

## Key Logic
- Opens customer file for sequential read
- Reads each customer record sequentially
- Displays each record to output
- Handles end-of-file and error conditions
- Standard file processing loop with error handling

## Data Dependencies

**Key Copybooks**:
- `CVCUS01Y` - Customer record structure

**Files Accessed**:
- `CUSTFILE` - Customer master file (KSDS/VSAM, indexed, sequential access)

## Program Relationships
**Calls**: None  
**Called By**: JCL jobs (READCUST.jcl or similar administrative jobs)

## Notable Patterns
- Identical structure to CBACT02C and CBACT03C (sequential read utility pattern)
- Standard VSAM error handling
- Uses CEE3ABD for abnormal termination
- Display-oriented (console output only)
- File status display utility (Z-DISPLAY-IO-STATUS)
- Note: Uses 'Z-' prefix for utility paragraphs instead of '9910-' prefix

# Job Analysis: READCUST

## Overview
**Source File**: `app/jcl/READCUST.jcl`
**Frequency**: On-demand / Testing
**Type**: File browse utility

## Purpose
Executes CBCUS01C program to browse Customer Master VSAM file for verification and reporting.

## Processing Steps
1. **STEP05**: Execute CBCUS01C
   - Reads: AWS.M2.CARDDEMO.CUSTDATA.VSAM.KSDS
   - Outputs to SYSOUT

## Data Flow
**Input**: AWS.M2.CARDDEMO.CUSTDATA.VSAM.KSDS  
**Output**: SYSOUT (formatted customer listing)

## Program Details
- **Program**: CBCUS01C (Customer File Browse)
- **Purpose**: Customer record display
- **Output**: Report format

## Use Cases
- Customer data verification
- Post-load validation
- Customer file audit
- Support and debugging

## Dependencies
- CBCUS01C program compiled
- Customer VSAM file accessible

## Modernization Notes
- Browse utility → SQL query with formatted output
- SYSOUT → Modern logging or web interface
- Consider: Customer search API endpoint
- Replace with .NET utility or Azure Function
- Output options: JSON, CSV, or dashboard

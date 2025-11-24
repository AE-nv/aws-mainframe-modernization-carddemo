# Job Analysis: READCARD

## Overview
**Source File**: `app/jcl/READCARD.jcl`
**Frequency**: On-demand / Testing
**Type**: File browse utility

## Purpose
Executes CBACT02C program to browse and display Card Master VSAM file contents for verification or reporting.

## Processing Steps
1. **STEP05**: Execute CBACT02C
   - Reads: AWS.M2.CARDDEMO.CARDDATA.VSAM.KSDS
   - Outputs to SYSOUT for display/review

## Data Flow
**Input**: AWS.M2.CARDDEMO.CARDDATA.VSAM.KSDS  
**Output**: SYSOUT (display output for verification)

## Program Details
- **Program**: CBACT02C (Card File Browse)
- **Purpose**: Card record display and verification
- **Output**: Report format to SYSOUT

## Use Cases
- Card file content verification
- Testing after card data loads
- Ad-hoc card data review
- Debugging card-related issues

## Dependencies
- CBACT02C program in LOADLIB
- Card VSAM file must exist

## Modernization Notes
- Simple browse utility → SQL SELECT query
- SYSOUT display → Web dashboard or API endpoint
- Consider: RESTful API to retrieve card data (with appropriate security)
- Replace with .NET console app or PowerShell script
- Output: Console, log file, or web interface

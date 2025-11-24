# Job Analysis: READXREF

## Overview
**Source File**: `app/jcl/READXREF.jcl`
**Frequency**: On-demand / Testing
**Type**: File browse utility

## Purpose
Executes CBACT03C to browse Card Cross-Reference file and verify card-to-account relationships.

## Processing Steps
1. **STEP05**: Execute CBACT03C
   - Reads: AWS.M2.CARDDEMO.CARDXREF.VSAM.KSDS
   - Displays card/account relationships

## Data Flow
**Input**: AWS.M2.CARDDEMO.CARDXREF.VSAM.KSDS  
**Output**: SYSOUT (cross-reference listing)

## Program Details
- **Program**: CBACT03C (Card X-Ref Browse)
- **Purpose**: Verify card-account relationships
- **Output**: Cross-reference report

## Use Cases
- Validate card-account linkages
- Troubleshoot relationship issues
- Audit account/card associations
- Test data verification

## Dependencies
- CBACT03C in LOADLIB
- XREF VSAM file accessible

## Modernization Notes
- VSAM browse → SQL JOIN query (Card JOIN Account)
- Simple lookup → RESTful API endpoint
- Consider GraphQL for flexible relationship queries
- Replace with .NET console app or web service

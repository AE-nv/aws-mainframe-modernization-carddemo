# Job Analysis: CUSTFILE

## Overview
**Source File**: `app/jcl/CUSTFILE.jcl`
**Frequency**: Initial setup / On-demand
**Type**: VSAM file definition and data load

## Purpose
Defines and initializes the Customer Master VSAM KSDS file with CICS integration for file access control.

## Processing Steps
1. **CLCIFIL**: Close CICS file CUSTDAT
   - Issues CEMT command to CICS region CICSAWSA
2. **STEP05**: Delete existing CUSTDATA VSAM cluster
   - Error suppression (MAXCC=0)
3. **STEP10**: Define CUSTDATA VSAM KSDS cluster
   - Key: 9 bytes at position 0 (Customer ID)
   - Record size: 500 bytes
   - Space: 1 cylinder primary, 5 secondary
4. **STEP15**: Load data from flat file
   - Source: AWS.M2.CARDDEMO.CUSTDATA.PS
   - Target: CUSTDATA VSAM cluster
5. **OPCIFIL**: Reopen CICS file CUSTDAT

## Data Flow
**Input**: AWS.M2.CARDDEMO.CUSTDATA.PS (customer flat file)  
**Output**: AWS.M2.CARDDEMO.CUSTDATA.VSAM.KSDS

## VSAM File Specifications
- **Type**: KSDS (Key-Sequenced Data Set)
- **Key**: 9 bytes at offset 0 (Customer ID format: 9 numeric digits)
- **Record Length**: 500 bytes (largest record in CardDemo system)
- **Share Options**: (2,3) - Multi-region CICS access with integrity

## CICS Integration
- CICS File Name: CUSTDAT
- Requires close before definition, open after completion
- CICS Region: CICSAWSA

## Dependencies
- Source file: AWS.M2.CARDDEMO.CUSTDATA.PS
- CICS region must be active for file commands
- SDSF authority for CEMT operations

## Modernization Notes
- VSAM → SQL table: Customers
- Primary key: CustomerId CHAR(9)
- Large record size (500 bytes) indicates rich customer profile
- CICS coordination → Database connection management
- Share options → SQL transaction isolation (READ COMMITTED)

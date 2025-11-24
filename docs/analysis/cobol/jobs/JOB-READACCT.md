# Job Analysis: READACCT

## Overview
**Source File**: `app/jcl/READACCT.jcl`
**Frequency**: On-demand / Testing
**Type**: File browse and export utility

## Purpose
Executes CBACT01C program to read Account Master VSAM file and produce three different output formats for testing, reporting, or data extraction purposes.

## Processing Steps
1. **PREDEL**: Pre-delete existing output files
   - Removes prior versions of three output datasets
2. **STEP05**: Execute CBACT01C program
   - Reads: AWS.M2.CARDDEMO.ACCTDATA.VSAM.KSDS
   - Produces three output formats simultaneously

## Data Flow
**Input**: AWS.M2.CARDDEMO.ACCTDATA.VSAM.KSDS (Account VSAM file)  
**Outputs**:
1. **OUTFILE**: AWS.M2.CARDDEMO.ACCTDATA.PSCOMP (LRECL=107, FB)
   - Compressed format with selected fields
2. **ARRYFILE**: AWS.M2.CARDDEMO.ACCTDATA.ARRYPS (LRECL=110, FB)
   - Array format for batch processing
3. **VBRCFILE**: AWS.M2.CARDDEMO.ACCTDATA.VBPS (LRECL=84, VB)
   - Variable-blocked format

## Program Details
- **Program**: CBACT01C (Account File Browse utility)
- **Purpose**: Multi-format account data export
- **Library**: AWS.M2.CARDDEMO.LOADLIB

## Output File Characteristics
- All files cataloged with space=(CYL,(1,2),RLSE)
- BLKSIZE=0 (system-determined blocking)
- Different record formats for different use cases

## Use Cases
- Account data extraction for reporting
- Test data generation
- Data validation and verification
- Account file backup in flat file format

## Dependencies
- Program CBACT01C must be compiled and in LOADLIB
- Account VSAM file must exist and be accessible

## Modernization Notes
- VSAM read → SQL SELECT statement
- Three output formats → Single query with multiple exporters
- Consider: CSV, JSON, or Parquet for modern data extraction
- CBACT01C logic → .NET console application or Azure Function
- Replace JCL with Azure Data Factory or .NET batch job
- Output to Azure Blob Storage instead of MVS datasets

# Job Analysis: CBEXPORT

## Overview
**Source File**: `app/jcl/CBEXPORT.jcl`
**Frequency**: On-demand / Data migration
**Type**: Data export and consolidation

## Purpose
Exports customer data from multiple VSAM files into single multi-record export file for branch migration, data transfer, or backup purposes. Companion to CBIMPORT.

## Processing Steps
1. **STEP01**: Define VSAM export file cluster
   - Creates indexed VSAM file for export data
   - Key: 4 bytes at offset 28
   - Record size: 500 bytes
   - Space: 10 cylinders primary, 5 secondary
2. **STEP02**: Execute CBEXPORT program
   - Reads 5 source VSAM files
   - Consolidates into single multi-record export file

## Data Flow
**Inputs** (5 VSAM files):
1. **CUSTFILE**: AWS.M2.CARDDEMO.CUSTDATA.VSAM.KSDS
2. **ACCTFILE**: AWS.M2.CARDDEMO.ACCTDATA.VSAM.KSDS
3. **XREFFILE**: AWS.M2.CARDDEMO.CARDXREF.VSAM.KSDS
4. **TRANSACT**: AWS.M2.CARDDEMO.TRANSACT.VSAM.KSDS
5. **CARDFILE**: AWS.M2.CARDDEMO.CARDDATA.VSAM.KSDS

**Output**: AWS.M2.CARDDEMO.EXPORT.DATA (VSAM KSDS)

## Export File Specifications
- **Type**: VSAM KSDS (indexed)
- **Key**: 4 bytes at position 28 (likely record type identifier)
- **Record Size**: 500 bytes (largest record accommodated)
- **Space**: 10 CYL primary, 5 CYL secondary (large allocation)
- **FREESPACE**: (10,10) - efficient for batch loads

## Program Details
- **Program**: CBEXPORT
- **Function**: Multi-file consolidation into export format
- **Processing**: Reads all source files, formats records, writes to export file

## Use Cases
- Branch office data export for migration
- System-to-system data transfer
- Backup before major changes
- Test data extraction
- Data warehouse loading

## VSAM Delete/Define Pattern
- Deletes existing export file (if any)
- Defines new export cluster
- Ensures clean export file for each run

## Dependencies
- CBEXPORT program in LOADLIB
- All 5 source VSAM files must exist
- Sufficient DASD for export file (potential for large volume)

## Modernization Notes
- Multi-file export → Modern data pipeline or ETL
- VSAM consolidation → Database export to file or API
- Consider:
  - Azure Data Factory export pipeline
  - .NET bulk export service
  - SQL Server BCP or SSIS for export
- Export format → JSON, Parquet, or CSV for modern systems
- Multi-record format → Separate files by entity or streaming API
- Output destination: Azure Blob Storage, SFTP, or API endpoint
- Incremental export: Add timestamp filters for delta exports
- Compression: Use gzip or Azure Blob compression
- Monitoring: Track export metrics (record counts, file size, duration)

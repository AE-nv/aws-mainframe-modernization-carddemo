# Job Analysis: CBIMPORT

## Overview
**Source File**: `app/jcl/CBIMPORT.jcl`
**Frequency**: On-demand / Data migration
**Type**: Data import and normalization

## Purpose
Imports customer data from multi-record export file and splits into separate normalized files for Customer, Account, Cross-Reference, and Transaction entities. Critical for data migration and system integration.

## Processing Steps
1. **STEP01**: Execute CBIMPORT program
   - Reads multi-record export file
   - Parses and splits into 4 normalized output files
   - Generates error file for invalid records

## Data Flow
**Input**: AWS.M2.CARDDEMO.EXPORT.DATA (multi-record VSAM file)  
**Outputs**:
1. **CUSTOUT**: AWS.M2.CARDDEMO.CUSTDATA.IMPORT (LRECL=500)
   - Customer records
2. **ACCTOUT**: AWS.M2.CARDDEMO.ACCTDATA.IMPORT (LRECL=300)
   - Account records
3. **XREFOUT**: AWS.M2.CARDDEMO.CARDXREF.IMPORT (LRECL=50)
   - Card cross-reference records
4. **TRNXOUT**: AWS.M2.CARDDEMO.TRANSACT.IMPORT (LRECL=350)
   - Transaction records
5. **ERROUT**: AWS.M2.CARDDEMO.IMPORT.ERRORS (LRECL=132)
   - Import error log

## Program Details
- **Program**: CBIMPORT
- **Function**: Parse multi-record export format into normalized files
- **Error Handling**: Captures invalid records to error file

## Space Allocation
- CUSTOUT: 50 tracks primary, 25 secondary
- ACCTOUT: 50 tracks primary, 25 secondary
- XREFOUT: 25 tracks primary, 10 secondary
- TRNXOUT: 100 tracks primary, 50 secondary (largest)
- ERROUT: 10 tracks primary, 5 secondary

## Use Cases
- Data migration from other systems
- Branch consolidation
- System integration imports
- Test data loading
- Disaster recovery data restoration

## Dependencies
- CBIMPORT program compiled
- Export file must exist and be properly formatted
- Sufficient DASD space for all output files

## Modernization Notes
- Multi-record import → Modern ETL process
- COBOL parsing → .NET data transformation or Azure Data Factory
- Consider:
  - Azure Data Factory with mapping data flows
  - .NET ETL service using CsvHelper or custom parser
  - SSIS package for SQL Server integration
- Export file format → JSON or CSV for modern systems
- Error handling → Structured logging (Application Insights)
- Validation rules → FluentValidation or Data Annotations
- Output: Direct SQL inserts instead of intermediate files
- Transactional integrity: All-or-nothing import within transaction

# Program Analysis: CBEXPORT

## Overview
**Source File**: `app/cbl/CBEXPORT.cbl`  
**Type**: Batch Data Migration Export  
**Module**: Data Management / Migration

## Business Purpose
Exports complete customer profiles from CardDemo normalized files into a multi-record export file for branch migration. Creates a single file containing different record types (customer, account, card, cross-reference, transaction) for data migration to other branches or systems.

## Key Logic
1. Opens all CardDemo master files for sequential read
2. Processes each file type:
   - Customer file → creates 'C' type export records
   - Account file → creates 'A' type export records
   - Cross-reference file → creates 'X' type export records
   - Transaction file → creates 'T' type export records
   - Card file → creates 'D' type export records
3. For each source record:
   - Maps fields to export record layout
   - Assigns record type indicator
   - Adds timestamp and sequence number
   - Adds branch ID and region code
4. Tracks export statistics by record type
5. Produces summary report with counts

## Data Dependencies

**Key Copybooks**:
- `CVCUS01Y` - Customer input record
- `CVACT01Y` - Account input record
- `CVACT03Y` - Cross-reference input record
- `CVTRA05Y` - Transaction input record
- `CVACT02Y` - Card input record
- `CVEXPORT` - Export file multi-record layout

**Files Accessed**:
- `CUSTFILE` - Customer input file (indexed, sequential read)
- `ACCTFILE` - Account input file (indexed, sequential read)
- `XREFFILE` - Cross-reference input file (indexed, sequential read)
- `TRANSACT` - Transaction input file (indexed, sequential read)
- `CARDFILE` - Card input file (indexed, sequential read)
- `EXPFILE` - Export output file (indexed, sequential write)

## Program Relationships
**Calls**: None  
**Called By**: Data migration jobs (CBEXPORT.jcl)

## Notable Patterns
- Multi-file input (reads 5 different files)
- Data consolidation (multiple inputs → one output)
- Record type tagging ('C', 'A', 'X', 'T', 'D')
- Sequence number generation (incremental counter)
- Timestamp formatting (26-character format)
- Metadata enrichment (branch ID, region code)
- Field-by-field mapping between different layouts
- Statistics tracking (counters for each record type)
- Comprehensive export reporting
- Opposite of CBIMPORT (consolidate vs. split)

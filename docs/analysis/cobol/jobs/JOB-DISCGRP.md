# Job Analysis: DISCGRP

## Overview
**Source File**: `app/jcl/DISCGRP.jcl`
**Frequency**: Initial setup / Reference data update
**Type**: VSAM reference file definition and load

## Purpose
Defines and loads Disclosure Group VSAM file containing disclosure statement groups and codes for transaction disclosures and regulatory compliance.

## Processing Steps
1. **STEP05**: Delete existing DISCGRP VSAM file
   - Deletes: AWS.M2.CARDDEMO.DISCGRP.VSAM.KSDS
   - Sets MAXCC=0 (error suppression)
2. **STEP10**: Define DISCGRP VSAM KSDS
   - Key: 16 bytes at offset 0 (Disclosure Group ID)
   - Record: 50 bytes (small reference record)
   - Space: 1 CYL primary, 5 CYL secondary
   - SHAREOPTIONS(2,3) - Multi-region access
3. **STEP15**: Load disclosure group data
   - Source: AWS.M2.CARDDEMO.DISCGRP.PS
   - Target: DISCGRP VSAM
   - Uses IDCAMS REPRO

## VSAM File Specifications
- **Type**: KSDS
- **Key**: 16 bytes at offset 0 (Disclosure Group ID)
- **Record**: 50 bytes (small reference/lookup record)
- **Share Options**: (2,3) - Multi-region update with integrity

## Data Characteristics
- Reference data file (relatively static)
- Small record size (50 bytes)
- Likely contains disclosure text group identifiers
- Used for regulatory compliance and customer disclosures

## Purpose in Application
Disclosure groups support:
- Transaction disclosure statements
- Regulatory compliance text
- Terms and conditions grouping
- Fee disclosure associations
- Privacy policy references

## Share Options (2,3)
- Allows multi-region updates (batch and online)
- Transactional integrity for cross-region access
- More permissive than typical reference data (1,4)
- Suggests disclosure text may be updated by applications

## Dependencies
- Source file: AWS.M2.CARDDEMO.DISCGRP.PS
- Must complete before applications reference disclosure groups
- Related to: Transaction processing, statement generation

## Modernization Notes
- VSAM reference file → SQL lookup table
  ```sql
  CREATE TABLE DisclosureGroups (
      DisclosureGroupId CHAR(16) PRIMARY KEY,
      GroupName NVARCHAR(100),
      Description NVARCHAR(500),
      DisclosureText NVARCHAR(MAX),
      EffectiveDate DATE,
      ExpirationDate DATE,
      IsActive BIT DEFAULT 1
  );
  ```
- Small dataset → Cache at application startup
- Consider: 
  - Azure Blob Storage for full disclosure text documents
  - Database for metadata and references
  - Content Management System (CMS) for disclosure management
- Reference data pattern: Load once, cache in memory
- Updates: Clear cache on change, reload from database
- API: GET /api/reference/disclosure-groups/{id}
- Regulatory compliance:
  - Version control for disclosure text changes
  - Audit trail of modifications (temporal table)
  - Effective date management for legal compliance
- Modern content storage:
  - Azure Cosmos DB for global distribution
  - Azure SQL for relational integrity
  - Redis cache for high-performance lookups

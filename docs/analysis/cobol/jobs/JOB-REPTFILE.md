# Job Analysis: REPTFILE

## Overview
**Source File**: `app/jcl/REPTFILE.jcl`
**Frequency**: Initial setup / One-time
**Type**: GDG base definition

## Purpose
Defines Generation Data Group base for transaction report files (TRANREPT) to support versioned report retention.

## Processing Steps
1. **STEP05**: Define GDG base using IDCAMS
   - Name: AWS.M2.CARDDEMO.TRANREPT
   - Limit: 10 generations
   - No SCRATCH specified (manual deletion required)

## GDG Specifications
- **Name**: AWS.M2.CARDDEMO.TRANREPT
- **Limit**: 10 generations maximum
- **Scratch**: Not specified (generations must be manually deleted)
- **Purpose**: Store historical transaction reports

## GDG Characteristics
- Retains last 10 report generations
- Larger limit than most GDGs (daily reports = 10 days history)
- No automatic scratch (reports preserved until manual cleanup)

## Usage
Referenced by:
- TRANREPT.jcl - Creates report generations
- Ad-hoc report jobs
- Batch reporting processes

## Dependencies
- One-time setup before any report generation
- Must exist before TRANREPT.jcl runs

## Modernization Notes
- GDG → Azure Blob Storage with dated folder structure
  ```
  reports/transactions/YYYY/MM/DD/transaction-report-HHmmss.pdf
  ```
- Retention: Azure Blob lifecycle management (delete after 10 days)
- Consider: Database table to track report metadata
- Report storage: Azure Blob, SharePoint, or document management system
- Versioning: Blob versioning or timestamp-based naming
- Access: Generate download URL, serve via web portal
- No manual deletion needed with automated retention policies

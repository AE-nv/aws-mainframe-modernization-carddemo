# Job Analysis: DALYREJS

## Overview
**Source File**: `app/jcl/DALYREJS.jcl`
**Frequency**: Initial setup / One-time
**Type**: GDG base definition

## Purpose
Defines Generation Data Group (GDG) base for daily reject file to support versioned reject file management across batch cycles.

## Processing Steps
1. **STEP05**: Define GDG base using IDCAMS
   - Name: AWS.M2.CARDDEMO.DALYREJS
   - Limit: 5 generations
   - SCRATCH option enabled (automatic deletion of expired generations)

## GDG Specifications
- **Name**: AWS.M2.CARDDEMO.DALYREJS
- **Limit**: 5 generations maximum
- **Scratch**: Automatically delete oldest generation when limit exceeded
- **Purpose**: Store daily transaction reject records

## GDG Benefits
- Automatic version management
- Historical reject file retention (last 5 days)
- Simplified JCL (reference by relative generation)
- Automatic cleanup of old generations

## Usage Pattern
Jobs reference this GDG as:
- `AWS.M2.CARDDEMO.DALYREJS(+1)` - Create new generation
- `AWS.M2.CARDDEMO.DALYREJS(0)` - Current generation
- `AWS.M2.CARDDEMO.DALYREJS(-1)` - Previous generation

## Dependencies
- Must run before any job that creates reject files
- One-time setup job (unless GDG needs redefinition)

## Related Jobs
- Transaction validation jobs that produce reject files
- Likely used by CBTRN01C (transaction validation)

## Modernization Notes
- GDG concept → Azure Blob Storage with blob versioning or dated folders
- Consider:
  - Blob container: `rejects/daily/YYYY/MM/DD/rejects.csv`
  - Automated retention policy (delete after 5 days)
  - Azure Blob lifecycle management
- Reject records → Database table with timestamp
- Generation management → Partition by date
- SCRATCH equivalent → Azure Blob retention policy
- Modern approach: Store in database with created_date, query last 5 days

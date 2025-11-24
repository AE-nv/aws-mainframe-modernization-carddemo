# Job Analysis: INTRDRJ2

## Overview
**Source File**: `app/jcl/INTRDRJ2.JCL`
**Frequency**: Triggered by INTRDRJ1
**Type**: Chained job (submitted via internal reader)

## Purpose
Second job in internal reader demonstration chain. Executes IDCAMS file copy operation after being dynamically submitted by INTRDRJ1.

## Processing Steps
1. **IDCAMS**: Copy backup file to INTRDR-specific backup
   - Source: AWS.M2.CARDEMO.FTP.TEST.BKUP
   - Target: AWS.M2.CARDEMO.FTP.TEST.BKUP.INTRDR
   - Uses IDCAMS REPRO

## Job Chain Position
```
INTRDRJ1
   └─> Submits INTRDRJ2 (this job) via internal reader
         └─> Copies backup file to final location
```

## Data Flow
1. INTRDRJ1: Original → Backup
2. INTRDRJ2: Backup → INTRDR-specific backup
3. Result: Three copies of test file

## File Progression
- **Original**: AWS.M2.CARDEMO.FTP.TEST
- **Backup 1**: AWS.M2.CARDEMO.FTP.TEST.BKUP (created by INTRDRJ1)
- **Backup 2**: AWS.M2.CARDEMO.FTP.TEST.BKUP.INTRDR (created by INTRDRJ2)

## Purpose in Demonstration
- Shows job can submit another job
- Demonstrates job chaining without scheduler
- Second job operates on first job's output
- Tests internal reader functionality end-to-end

## Technical Notes
- Submitted dynamically, not scheduled
- Separate job ID from INTRDRJ1
- Can run concurrently if resources available
- No explicit synchronization between jobs

## Dependencies
- Must be submitted by INTRDRJ1 (or manually)
- Source file from INTRDRJ1 must exist
- IDCAMS utility available

## Modernization Notes
- Chained job → Workflow step in orchestration
- File copy → Azure Blob copy or database operation
- Consider:
  - **Azure Durable Functions**: Activity function
    ```csharp
    [FunctionName("CopyFileActivity")]
    public async Task CopyFile([ActivityTrigger] string input)
    {
        var sourceBlob = new BlobClient(sourceUri);
        var destBlob = new BlobClient(destUri);
        await destBlob.StartCopyFromUriAsync(sourceBlob.Uri);
    }
    ```
  - **Azure Logic Apps**: Step in workflow
  - **Azure Data Factory**: Copy activity in pipeline
- Job chain → Orchestration:
  ```yaml
  # Logic Apps workflow
  steps:
    - id: backup_original
      type: CopyFile
      source: original
      destination: backup1
    - id: backup_to_intrdr
      type: CopyFile
      depends_on: backup_original
      source: backup1
      destination: backup2
  ```
- Modern patterns:
  - Single orchestration, not separate jobs
  - Explicit dependencies (not implicit via submission)
  - Built-in retry and error handling
  - Visual monitoring and debugging
- File operations:
  - Azure Blob storage copy (server-side)
  - No data transfer through compute
  - Atomic operations with versioning
- No need for separate job submission:
  - Steps execute in sequence
  - Orchestrator manages state
  - Automatic failure handling

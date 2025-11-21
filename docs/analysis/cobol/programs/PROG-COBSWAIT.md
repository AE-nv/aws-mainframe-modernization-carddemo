# Program Analysis: COBSWAIT

## Overview
**Source File**: `app/cbl/COBSWAIT.cbl`  
**Type**: Batch Utility  
**Module**: System Utilities

## Business Purpose
Simple utility program that introduces a configurable delay in job step processing. Used in JCL workflows to control timing between job steps, wait for file availability, or implement throttling. The wait time is specified in centiseconds (1/100th of a second).

## Key Logic
1. Accepts parameter value from SYSIN (wait time in centiseconds)
2. Converts parameter to binary format
3. Calls MVSWAIT assembler routine with wait time
4. Stops execution

## Data Dependencies

**Key Copybooks**: None

**Files Accessed**: None

## Program Relationships
**Calls**: `MVSWAIT` - Assembler routine that performs actual wait operation  
**Called By**: JCL jobs that require delays (WAITSTEP.jcl)

## Notable Patterns
- Extremely simple program (< 30 lines)
- Parameter-driven (reads from SYSIN)
- Calls assembler routine (MVSWAIT) for actual timing
- No file I/O
- No error handling (relies on MVSWAIT routine)
- Common mainframe pattern for inter-step delays
- Time specified in centiseconds (100 = 1 second)

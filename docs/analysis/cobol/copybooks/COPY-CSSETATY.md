# Copybook Analysis: CSSETATY

## Overview
**Source File**: `app/cpy/CSSETATY.cpy`  
**Type**: Screen Attribute Setting Template  
**Used By**: Online CICS programs with screen validation

## Purpose
Provides reusable code template for setting screen field attributes (color) based on validation status. Used to highlight invalid fields in red and mark blank required fields with asterisks.

## Structure Overview
Template code snippet (not a data structure) that sets field color to red if validation fails and optionally adds an asterisk for blank required fields.

## Key Logic
```cobol
IF (field-NOT-OK OR field-BLANK) AND program-reentry
   MOVE DFHRED TO field-color-attribute
   IF field-BLANK
      MOVE '*' TO field-output
   END-IF
END-IF
```

## Notable Patterns
- Uses CICS field attributes (DFHRED for red highlighting)
- Conditional on program re-entry (only highlights on validation failure, not first display)
- Template requires token substitution: `TESTVAR1`, `SCRNVAR2`, `MAPNAME3`
- Requires FLG flags to be set by validation logic

## Usage Context
Included via COPY REPLACING mechanism in online programs that perform field validation (account update, card update, transaction add). Template tokens are replaced with actual field names during compilation.

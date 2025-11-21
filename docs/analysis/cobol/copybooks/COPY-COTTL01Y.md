# Copybook Analysis: COTTL01Y

## Overview
**Source File**: `app/cpy/COTTL01Y.cpy`  
**Type**: Screen Title Constants  
**Used By**: All online CICS programs

## Purpose
Provides standardized screen header/title text used across all online programs to maintain consistent branding and user experience.

## Structure Overview
Simple structure with three 40-character title constants for screen headers and closing messages.

## Key Fields
- `CCDA-SCREEN-TITLE` - Parent structure containing:
  - `CCDA-TITLE01` - Line 1: 'AWS Mainframe Modernization' (40 chars)
  - `CCDA-TITLE02` - Line 2: 'CardDemo' (40 chars)
  - `CCDA-THANK-YOU` - Closing message: 'Thank you for using CCDA application...' (40 chars)

## Notable Patterns
- Fixed 40-character width for screen alignment
- Centered text with padding spaces
- Historical note: Previously named "Credit Card Demo Application (CCDA)", simplified to "CardDemo"

## Usage Context
Included in all online programs to display consistent application branding on screen headers. Typically displayed at top of every BMS map/screen.

# Copybook Analysis: CVACT02Y

## Overview
**Source File**: `app/cpy/CVACT02Y.cpy`  
**Type**: Entity Data Structure  
**Used By**: Card management programs (15 programs)

## Purpose
Defines the card master record structure with 150-byte fixed record length. Contains card identification, security details, and status information.

## Structure Overview
Card entity with 16-character card number key, associated account ID, CVV code, cardholder name, expiration date, and active status indicator.

## Key Fields
- `CARD-NUM` - 16-character card number (primary key)
- `CARD-ACCT-ID` - Associated account ID (11 digits)
- `CARD-CVV-CD` - 3-digit CVV security code
- `CARD-EMBOSSED-NAME` - Name printed on card (50 characters)
- `CARD-EXPIRAION-DATE` - Card expiration date (YYYY-MM-DD format)
- `CARD-ACTIVE-STATUS` - Card status flag (1 character: 'Y'/'N')

## Usage Context
Used by card inquiry programs (COCRDLIC, COCRDSLC), card update programs (COCRDUPC), batch utilities (CBACT02C), and data migration programs (CBIMPORT, CBEXPORT).

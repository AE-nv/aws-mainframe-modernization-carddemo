# Copybook Analysis: CVCUS01Y

## Overview
**Source File**: `app/cpy/CVCUS01Y.cpy`  
**Type**: Entity Data Structure  
**Used By**: Customer management programs (500-byte record length)

## Purpose
Defines the customer master record structure containing complete customer profile including personal information, contact details, address, and credit score.

## Structure Overview
Comprehensive customer entity with demographic data, multi-line address, multiple phone numbers, government identifiers, date of birth, EFT account, cardholder indicator, and FICO credit score.

## Key Fields
- `CUST-ID` - Customer ID (9 digits, primary key)
- `CUST-FIRST-NAME`, `CUST-MIDDLE-NAME`, `CUST-LAST-NAME` - Name fields (25 characters each)
- `CUST-ADDR-LINE-1/2/3` - Three-line address (50 characters each)
- `CUST-ADDR-STATE-CD`, `CUST-ADDR-COUNTRY-CD`, `CUST-ADDR-ZIP` - Address components
- `CUST-PHONE-NUM-1/2` - Two phone numbers (15 characters each)
- `CUST-SSN` - Social Security Number (9 digits)
- `CUST-GOVT-ISSUED-ID` - Government ID (20 characters)
- `CUST-DOB-YYYY-MM-DD` - Date of birth (YYYY-MM-DD format)
- `CUST-EFT-ACCOUNT-ID` - Electronic funds transfer account (10 characters)
- `CUST-PRI-CARD-HOLDER-IND` - Primary cardholder indicator
- `CUST-FICO-CREDIT-SCORE` - Credit score (3 digits)

## Notable Patterns
- Multi-line address support (3 lines)
- Multiple phone numbers (2)
- PII data (SSN, DOB, government ID)
- FICO credit scoring

## Usage Context
Used by customer batch utilities (CBCUS01C), account/customer update programs (COACTUPC), transaction validation (CBTRN01C), and data migration programs (CBIMPORT, CBEXPORT).

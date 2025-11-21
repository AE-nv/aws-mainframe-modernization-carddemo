# CardDemo Data Dictionary

**Last Updated**: 2025-11-21  
**Purpose**: Comprehensive listing of all data structures from COBOL copybooks

## Entity Data Structures

### Account Record (CVACT01Y)
**File**: ACCTFILE  
**Record Length**: 300 bytes  
**Key**: ACCT-ID

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| ACCT-ID | 9(11) | 11 | Account identifier (primary key) |
| ACCT-ACTIVE-STATUS | X(01) | 1 | Account status ('Y'/'N') |
| ACCT-CURR-BAL | S9(10)V99 | 12 | Current balance (signed decimal) |
| ACCT-CREDIT-LIMIT | S9(10)V99 | 12 | Credit limit |
| ACCT-CASH-CREDIT-LIMIT | S9(10)V99 | 12 | Cash advance limit |
| ACCT-OPEN-DATE | X(10) | 10 | Account open date (YYYY-MM-DD) |
| ACCT-EXPIRAION-DATE | X(10) | 10 | Expiration date |
| ACCT-REISSUE-DATE | X(10) | 10 | Reissue date |
| ACCT-CURR-CYC-CREDIT | S9(10)V99 | 12 | Current cycle credits |
| ACCT-CURR-CYC-DEBIT | S9(10)V99 | 12 | Current cycle debits |
| ACCT-ADDR-ZIP | X(10) | 10 | ZIP code |
| ACCT-GROUP-ID | X(10) | 10 | Account group identifier |

### Card Record (CVACT02Y)
**File**: CARDFILE  
**Record Length**: 150 bytes  
**Key**: CARD-NUM

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CARD-NUM | X(16) | 16 | Card number (primary key) |
| CARD-ACCT-ID | 9(11) | 11 | Associated account ID |
| CARD-CVV-CD | 9(03) | 3 | CVV security code |
| CARD-EMBOSSED-NAME | X(50) | 50 | Name on card |
| CARD-EXPIRAION-DATE | X(10) | 10 | Expiration date (YYYY-MM-DD) |
| CARD-ACTIVE-STATUS | X(01) | 1 | Card status ('Y'/'N') |

### Card Cross-Reference (CVACT03Y)
**File**: XREFFILE  
**Record Length**: 50 bytes  
**Key**: XREF-CARD-NUM

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| XREF-CARD-NUM | X(16) | 16 | Card number (primary key) |
| XREF-CUST-ID | 9(09) | 9 | Customer ID |
| XREF-ACCT-ID | 9(11) | 11 | Account ID |

### Customer Record (CVCUS01Y)
**File**: CUSTFILE  
**Record Length**: 500 bytes  
**Key**: CUST-ID

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CUST-ID | 9(09) | 9 | Customer identifier (primary key) |
| CUST-FIRST-NAME | X(25) | 25 | First name |
| CUST-MIDDLE-NAME | X(25) | 25 | Middle name |
| CUST-LAST-NAME | X(25) | 25 | Last name |
| CUST-ADDR-LINE-1 | X(50) | 50 | Address line 1 |
| CUST-ADDR-LINE-2 | X(50) | 50 | Address line 2 |
| CUST-ADDR-LINE-3 | X(50) | 50 | Address line 3 |
| CUST-ADDR-STATE-CD | X(02) | 2 | State code (US/territories) |
| CUST-ADDR-COUNTRY-CD | X(03) | 3 | Country code |
| CUST-ADDR-ZIP | X(10) | 10 | ZIP/postal code |
| CUST-PHONE-NUM-1 | X(15) | 15 | Primary phone number |
| CUST-PHONE-NUM-2 | X(15) | 15 | Secondary phone number |
| CUST-SSN | 9(09) | 9 | Social Security Number |
| CUST-GOVT-ISSUED-ID | X(20) | 20 | Government ID |
| CUST-DOB-YYYY-MM-DD | X(10) | 10 | Date of birth |
| CUST-EFT-ACCOUNT-ID | X(10) | 10 | EFT account |
| CUST-PRI-CARD-HOLDER-IND | X(01) | 1 | Primary cardholder indicator |
| CUST-FICO-CREDIT-SCORE | 9(03) | 3 | FICO credit score |

### Transaction Record (CVTRA05Y)
**File**: TRANFILE  
**Record Length**: 350 bytes  
**Key**: TRAN-ID

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| TRAN-ID | X(16) | 16 | Transaction identifier (primary key) |
| TRAN-TYPE-CD | X(02) | 2 | Transaction type code |
| TRAN-CAT-CD | 9(04) | 4 | Transaction category code |
| TRAN-SOURCE | X(10) | 10 | Source (Online, POS, ATM, etc.) |
| TRAN-DESC | X(100) | 100 | Transaction description |
| TRAN-AMT | S9(09)V99 | 11 | Transaction amount (signed) |
| TRAN-MERCHANT-ID | 9(09) | 9 | Merchant identifier |
| TRAN-MERCHANT-NAME | X(50) | 50 | Merchant name |
| TRAN-MERCHANT-CITY | X(50) | 50 | Merchant city |
| TRAN-MERCHANT-ZIP | X(10) | 10 | Merchant ZIP |
| TRAN-CARD-NUM | X(16) | 16 | Card number |
| TRAN-ORIG-TS | X(26) | 26 | Origination timestamp |
| TRAN-PROC-TS | X(26) | 26 | Processing timestamp |

### Statement Record (COSTM01)
**File**: STMTFILE  
**Record Length**: Variable  
**Key**: Statement sequence

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| STMT-ACCT-ID | 9(11) | 11 | Account ID |
| STMT-FORMAT | X(01) | 1 | Format type ('T'=text, 'H'=HTML) |
| STMT-PERIOD-START | X(10) | 10 | Statement period start date |
| STMT-PERIOD-END | X(10) | 10 | Statement period end date |
| STMT-LINE-DATA | X(132) | 132 | Statement line content |

### User Record (CSUSR01Y)
**File**: USRSEC  
**Record Length**: Variable  
**Key**: User ID

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| SEC-USR-ID | X(08) | 8 | User identifier (primary key) |
| SEC-USR-FNAME | X(20) | 20 | First name |
| SEC-USR-LNAME | X(20) | 20 | Last name |
| SEC-USR-PWD | X(08) | 8 | Password (encrypted) |
| SEC-USR-TYPE | X(01) | 1 | User type ('U'=user, 'A'=admin) |

---

## Reference Data Structures

### Transaction Type (CVTRA03Y)
**File**: TRANTYPE  
**Record Length**: 60 bytes  
**Key**: TRAN-TYPE

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| TRAN-TYPE | X(02) | 2 | Transaction type code (primary key) |
| TRAN-TYPE-DESC | X(50) | 50 | Transaction type description |

### Transaction Category (CVTRA04Y)
**File**: TRANCATG  
**Record Length**: 60 bytes  
**Key**: TRAN-CAT-KEY (composite)

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| TRAN-CAT-KEY | - | 6 | Composite key |
| ├─ TRAN-TYPE-CD | X(02) | 2 | Transaction type code |
| └─ TRAN-CAT-CD | 9(04) | 4 | Category code |
| TRAN-CAT-TYPE-DESC | X(50) | 50 | Category description |

### Disclosure Group (CVTRA02Y)
**File**: DISCLOSURES  
**Record Length**: 50 bytes  
**Key**: DIS-GROUP-KEY (composite)

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| DIS-GROUP-KEY | - | 16 | Composite key |
| ├─ DIS-ACCT-GROUP-ID | X(10) | 10 | Account group |
| ├─ DIS-TRAN-TYPE-CD | X(02) | 2 | Transaction type |
| └─ DIS-TRAN-CAT-CD | 9(04) | 4 | Transaction category |
| DIS-INT-RATE | S9(04)V99 COMP-3 | 3 | Interest rate (packed decimal) |

### Transaction Category Balance (CVTRA01Y)
**File**: TCATBALF  
**Record Length**: Variable  
**Key**: Composite

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| TRAN-CAT-KEY | - | 6 | Composite key |
| TRAN-CAT-BAL | S9(09)V99 | 11 | Category balance |

---

## Communication Areas

### Common Communication Area (COCOM01Y)
**Usage**: Passed between all online programs  
**Purpose**: Session state, navigation context

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CDEMO-FROM-TRANID | X(04) | 4 | Calling transaction ID |
| CDEMO-FROM-PROGRAM | X(08) | 8 | Calling program name |
| CDEMO-TO-TRANID | X(04) | 4 | Target transaction ID |
| CDEMO-TO-PROGRAM | X(08) | 8 | Target program name |
| CDEMO-USER-ID | X(08) | 8 | Current user ID |
| CDEMO-USER-TYPE | X(01) | 1 | User type ('U'/'A') |
| CDEMO-PGM-CONTEXT | X(01) | 1 | Program context flags |
| CDEMO-PGM-REENTER | X(01) | 1 | Re-entry indicator |
| CCARD-AID-ENTER | - | - | 88-level: ENTER key pressed |
| CCARD-AID-PFK01-12 | - | - | 88-levels: Function keys PF1-PF12 |

### Admin Communication Area (COADM02Y)
**Usage**: Admin menu programs  
**Purpose**: Admin-specific context

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| COADM-OPTION | X(02) | 2 | Selected admin option |
| COADM-USER-ID | X(08) | 8 | Admin user ID |

### Menu Options (COMEN02Y)
**Usage**: Menu programs  
**Purpose**: Menu option table

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CCARD-MENU-OPT-COUNT | 9(02) | 2 | Number of menu options |
| CCARD-MENU-OPT (occurs) | X(02) | 2 | Option codes |
| CCARD-MENU-OPT-NAME (occurs) | X(35) | 35 | Option descriptions |
| CCARD-MENU-OPT-PGMNAME (occurs) | X(08) | 8 | Target program names |
| CCARD-MENU-OPT-TRXNID (occurs) | X(04) | 4 | Target transaction IDs |

---

## Export/Import Structures

### Multi-Record Export Layout (CVEXPORT)
**File**: EXPFILE  
**Record Length**: 500 bytes  
**Key**: EXPORT-SEQUENCE-NUM

**Base Structure**:

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| EXPORT-REC-TYPE | X(01) | 1 | Record type ('C','A','X','T','D') |
| EXPORT-TIMESTAMP | X(26) | 26 | Export timestamp |
| EXPORT-SEQUENCE-NUM | 9(09) COMP | 4 | Sequence number |
| EXPORT-BRANCH-ID | X(04) | 4 | Branch identifier |
| EXPORT-REGION-CODE | X(05) | 5 | Region code |
| EXPORT-RECORD-DATA | X(460) | 460 | Variable data (REDEFINES) |

**Record Types** (via REDEFINES):
- **'C'** = Customer record (`EXPORT-CUSTOMER-DATA`)
- **'A'** = Account record (`EXPORT-ACCOUNT-DATA`)
- **'X'** = Cross-reference record (`EXPORT-CARD-XREF-DATA`)
- **'T'** = Transaction record (`EXPORT-TRANSACTION-DATA`)
- **'D'** = Card record (`EXPORT-CARD-DATA`)

---

## Message Structures

### Message Definitions (CSMSG01Y)
**Usage**: All programs  
**Purpose**: Standard error/info messages

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CCDA-MSG-ID-001-999 | X(07) | 7 | Message identifier |
| CCDA-MSG-TEXT (for each ID) | X(75) | 75 | Message text |

### Screen Titles (COTTL01Y)
**Usage**: All online programs  
**Purpose**: Standard screen headers

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| CCDA-TITLE01 | X(40) | 40 | 'AWS Mainframe Modernization' |
| CCDA-TITLE02 | X(40) | 40 | 'CardDemo' |
| CCDA-THANK-YOU | X(40) | 40 | Closing message |

---

## Validation Structures

### Date Validation (CSUTLDWY)
**Usage**: Programs validating dates  
**Purpose**: Date editing work areas

| Field | Type | Length | Description |
|-------|------|--------|-------------|
| WS-EDIT-DATE-CCYYMMDD | X(08) | 8 | Date being validated |
| ├─ WS-EDIT-DATE-CCYY | X(04) | 4 | Year |
| ├─ WS-EDIT-DATE-MM | X(02) | 2 | Month (1-12) |
| └─ WS-EDIT-DATE-DD | X(02) | 2 | Day (1-31) |
| WS-EDIT-DATE-FLGS | - | 3 | Validation flags |
| ├─ WS-EDIT-YEAR-FLG | X(01) | 1 | Year valid/not OK/blank |
| ├─ WS-EDIT-MONTH | X(01) | 1 | Month valid/not OK/blank |
| └─ WS-EDIT-DAY | X(01) | 1 | Day valid/not OK/blank |

### Lookup Codes (CSLKPCDY)
**Usage**: Address/phone validation  
**Purpose**: Validation lists

**US Phone Area Codes**:
- `WS-US-PHONE-AREA-CODE-TO-EDIT` (XXX)
- `VALID-PHONE-AREA-CODE` (700+ valid codes)
- `VALID-EASY-RECOG-AREA-CODE` (test codes like 555)

**US State Codes**:
- `US-STATE-CODE-TO-EDIT` (XX)
- `VALID-US-STATE-CODE` (56 states + territories)

**State-ZIP Combinations**:
- `US-STATE-ZIPCODE-TO-EDIT` (XXXX)
- `VALID-US-STATE-ZIP-CD2-COMBO` (250+ valid combinations)

---

## Report Structures (CVTRA07Y)

### Report Headers

| Structure | Description |
|-----------|-------------|
| REPORT-NAME-HEADER | Report title + date range |
| TRANSACTION-HEADER-1 | Column headers |
| TRANSACTION-HEADER-2 | Separator line (dashes) |

### Report Detail Lines

| Structure | Description |
|-----------|-------------|
| TRANSACTION-DETAIL-REPORT | Transaction detail line (formatted) |

### Report Totals

| Structure | Description |
|-----------|-------------|
| REPORT-PAGE-TOTALS | Page subtotal line |
| REPORT-ACCOUNT-TOTALS | Account subtotal line |
| REPORT-GRAND-TOTALS | Grand total line |

---

## Data Type Reference

| COBOL Type | Description | Storage | Range/Notes |
|------------|-------------|---------|-------------|
| X(n) | Alphanumeric | n bytes | Any characters |
| 9(n) | Numeric display | n bytes | Digits 0-9 |
| S9(n)V99 | Signed decimal | n+3 bytes | With 2 decimal places |
| 9(n) COMP | Binary | 2 or 4 bytes | Efficient numeric storage |
| 9(n) COMP-3 | Packed decimal | (n+1)/2 bytes | Efficient decimal storage |
| PIC X(n) VALUE | Constant | n bytes | Compile-time constant |

---

## Key Relationships

### Primary Keys
- **ACCTFILE**: ACCT-ID (11 digits)
- **CARDFILE**: CARD-NUM (16 characters)
- **CUSTFILE**: CUST-ID (9 digits)
- **TRANFILE**: TRAN-ID (16 characters)
- **XREFFILE**: XREF-CARD-NUM (16 characters)
- **USRSEC**: SEC-USR-ID (8 characters)

### Foreign Key Relationships
- `CARD-ACCT-ID` → `ACCT-ID`
- `XREF-CARD-NUM` → `CARD-NUM`
- `XREF-ACCT-ID` → `ACCT-ID`
- `XREF-CUST-ID` → `CUST-ID`
- `TRAN-CARD-NUM` → `CARD-NUM`

### Composite Keys
- Disclosure Group: GROUP-ID + TRAN-TYPE-CD + TRAN-CAT-CD
- Transaction Category: TRAN-TYPE-CD + TRAN-CAT-CD

---

## Summary

- **5 Primary Entities**: Customer, Account, Card, Transaction, User
- **3 Cross-Reference/Lookup**: Card X-Ref, Transaction Type, Transaction Category
- **2 Communication Areas**: COCOM01Y, COADM02Y
- **1 Export Format**: Multi-record export layout
- **10+ Utility Structures**: Dates, messages, titles, validation lists
- **Variable Record Lengths**: 50-500 bytes
- **Key Fields**: Mostly numeric IDs, alphanumeric codes

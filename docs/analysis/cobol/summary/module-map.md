# CardDemo COBOL Module Map

**Last Updated**: 2025-11-21  
**Purpose**: High-level overview of program relationships, data flows, and module boundaries

## Module Overview

CardDemo consists of 7 primary business modules:

1. **Authentication** - User login/logout
2. **Account Management** - Account inquiry, update, interest calculation
3. **Card Management** - Card inquiry, update, cross-reference
4. **Transaction Processing** - Transaction add, inquiry, posting, reporting
5. **User Management** - User administration (add, update, delete)
6. **Reporting** - Statements, transaction reports, bill payment
7. **Batch Processing** - Daily/monthly batch jobs
8. **Utilities** - Date validation, data migration, system utilities

---

## 1. Authentication Module

**Online Programs**:
- `COSGN00C` - Sign-on/authentication entry point

**Screens**:
- `COSGN00` - Login screen (user ID + password)

**Key Copybooks**:
- `COCOM01Y` - Communication area (stores user session)
- `CSMSG01Y` - Error/info messages

**Data Files**:
- `USRSEC` - User security file (credentials)

**Relationships**:
- Entry point → Main Menu (`COMEN01C`)
- Validates credentials against `USRSEC` file
- Populates `COCOM01Y` with user context

---

## 2. Account Management Module

**Online Programs**:
- `COACTVWC` - Account view/inquiry
- `COACTUPC` - Account and customer update (most complex, 4237 lines)

**Batch Programs**:
- `CBACT01C` - Account file browse utility
- `CBACT02C` - Card file browse utility
- `CBACT03C` - Cross-reference file browse utility
- `CBACT04C` - Monthly interest calculation

**Key Copybooks**:
- `CVACT01Y` - Account record structure
- `CVACT02Y` - Card record structure
- `CVACT03Y` - Card cross-reference structure
- `CUSTREC` / `CVCUS01Y` - Customer record structure
- `CVTRA01Y` - Transaction category balance

**Screens**:
- `COACTVW` - Account view screen
- `COACTUP` - Account update screen (40+ fields)

**Data Files**:
- `ACCTFILE` - Account master file (KSDS)
- `CUSTFILE` - Customer master file (KSDS)
- `CARDFILE` - Card master file (KSDS)
- `XREFFILE` - Card-account-customer cross-reference (KSDS)

**Relationships**:
- Called from: Main Menu
- Reads/Updates: Accounts, Customers, Cards
- Interest calculation uses transaction category balances

---

## 3. Card Management Module

**Online Programs**:
- `COCRDLIC` - Card list inquiry (browse)
- `COCRDSLC` - Card detail view
- `COCRDUPC` - Card update

**Key Copybooks**:
- `CVCRD01Y` - Card working storage structure
- `CVACT02Y` - Card file record
- `CVACT03Y` - Card cross-reference

**Screens**:
- `COCRDLI` - Card list screen
- `COCRDSL` - Card detail screen
- `COCRDUP` - Card update screen

**Data Files**:
- `CARDFILE` - Card master file
- `XREFFILE` - Cross-reference file
- `ACCTFILE` - Account file (for balance display)

**Relationships**:
- Called from: Main Menu
- Links to: Account Management (via cross-reference)

---

## 4. Transaction Processing Module

**Online Programs**:
- `COTRN00C` - Transaction list/menu
- `COTRN01C` - Transaction detail inquiry
- `COTRN02C` - Transaction add (new transaction entry)

**Batch Programs**:
- `CBTRN01C` - Daily transaction validation
- `CBTRN02C` - Daily transaction posting (critical)
- `CBTRN03C` - Transaction detail report generation

**Key Copybooks**:
- `CVTRA05Y` - Transaction master record (350 bytes)
- `CVTRA06Y` - Daily transaction record
- `CVTRA02Y` - Disclosure group (interest rates)
- `CVTRA03Y` - Transaction type lookup
- `CVTRA04Y` - Transaction category lookup
- `CVTRA07Y` - Transaction report layouts

**Screens**:
- `COTRN00` - Transaction list screen
- `COTRN01` - Transaction detail screen
- `COTRN02` - Transaction add screen

**Data Files**:
- `TRANFILE` - Transaction master file (KSDS)
- `DALYTRAN` - Daily transaction file (sequential)
- `XREFFILE` - Card cross-reference
- `TRANTYPE` - Transaction type lookup
- `TRANCATG` - Transaction category lookup
- `ACCTFILE` - Account file (for balance updates)

**Relationships**:
- Online: Called from Main Menu
- Batch: Daily posting job runs `CBTRN01C` (validate) → `CBTRN02C` (post)
- Updates account balances during posting
- Enriches transactions with card/account/customer info

---

## 5. User Management Module

**Online Programs**:
- `COUSR00C` - User list (browse all users)
- `COUSR01C` - User add
- `COUSR02C` - User update
- `COUSR03C` - User delete

**Key Copybooks**:
- `CSUSR01Y` - User record structure

**Screens**:
- `COUSR00` - User list screen
- `COUSR01` - User add screen
- `COUSR02` - User update screen
- `COUSR03` - User delete screen

**Data Files**:
- `USRSEC` - User security file (credentials + roles)

**Relationships**:
- Called from: Admin Menu (`COADM01C`)
- Restricted to admin users only

---

## 6. Reporting Module

**Online Programs**:
- `CORPT00C` - Reports menu (submits JCL jobs dynamically)
- `COBIL00C` - Bill payment entry

**Batch Programs**:
- `CBSTM03A` - Statement generation main program
- `CBSTM03B` - Statement file I/O subroutine

**Key Copybooks**:
- `COSTM01` - Statement record structure
- `CVTRA07Y` - Transaction report layouts

**Screens**:
- `CORPT00` - Reports menu screen
- `COBIL00` - Bill payment screen

**Data Files**:
- `STMTFILE` - Statement output file
- `TRANFILE` - Transactions (for statements)
- `ACCTFILE` - Accounts (for bill payment)

**Relationships**:
- Reporting menu submits batch jobs via internal reader
- Statement generation runs monthly
- Bill payment updates account balances immediately

---

## 7. Batch Processing Module

**Daily Jobs**:
- `POSTTRAN.jcl` - Transaction posting (`CBTRN02C`)
- `TRANCATG.jcl` - Transaction category balance

**Monthly Jobs**:
- `INTCALC.jcl` - Interest calculation (`CBACT04C`)
- `CREASTMT.JCL` - Statement generation (`CBSTM03A`, `CBSTM03B`)

**Utility Jobs**:
- Various file management and admin jobs

**Relationships**:
- Daily batch: Validate → Post → Update Balances
- Monthly batch: Calculate Interest → Generate Statements
- Dependencies: Daily jobs must complete before monthly jobs

---

## 8. Utilities Module

**Utility Programs**:
- `CSUTLDTC` - Date/time validation utility
- `CBIMPORT` - Data import (branch migration)
- `CBEXPORT` - Data export (branch migration)
- `COBSWAIT` - Delay/wait utility (for JCL timing)

**Key Copybooks**:
- `CSUTLDPY` - Date validation procedures
- `CSUTLDWY` - Date validation working storage
- `CSSTRPFY` - PF key handling procedures
- `CSSETATY` - Screen attribute setting template
- `CSLKPCDY` - Lookup code repository (phone, state, zip validation)
- `CVEXPORT` - Multi-record export file layout

**Usage**:
- Date utilities used by all programs validating dates
- Import/export used for branch-to-branch data migration
- PF key handling used by all online programs

---

## Common Components (Used by All Programs)

**Communication**:
- `COCOM01Y` - Common communication area (session state, navigation)

**Messages**:
- `CSMSG01Y` - Primary message definitions
- `CSMSG02Y` - Extended messages / abend data

**Screens**:
- `COTTL01Y` - Screen title constants (branding)

**Navigation**:
- `COMEN01C` + `COMEN01` - Main menu (hub for all modules)
- `COADM01C` + `COADM01` - Admin menu (for privileged functions)

---

## Data Flow Diagram

```
┌─────────────┐
│  COSGN00C   │  Authentication
│  (Sign-on)  │
└──────┬──────┘
       │
       v
┌─────────────────────────────────────────┐
│           COMEN01C (Main Menu)           │
└───┬───────┬────────┬────────┬──────┬────┘
    │       │        │        │      │
    v       v        v        v      v
┌────────┐ ┌────────┐ ┌────────┐ ┌──────┐ ┌──────────┐
│Account │ │  Card  │ │ Transac│ │Report│ │  Admin   │
│ Mgmt   │ │  Mgmt  │ │   tion │ │      │ │  Menu    │
│        │ │        │ │Process │ │      │ │          │
└────┬───┘ └───┬────┘ └───┬────┘ └──┬───┘ └────┬─────┘
     │         │           │         │          │
     v         v           v         v          v
┌──────────────────────────────────────────────────┐
│              Data Files (VSAM)                    │
│  ACCTFILE │ CARDFILE │ TRANFILE │ CUSTFILE │    │
│  XREFFILE │ USRSEC   │ STMTFILE │ ...      │    │
└──────────────────────────────────────────────────┘
                       │
                       v
┌──────────────────────────────────────────────────┐
│              Batch Processing                     │
│  Daily: CBTRN02C (posting) → CBACT04C (interest) │
│  Monthly: CBSTM03A/B (statements)                │
└──────────────────────────────────────────────────┘
```

---

## Module Dependencies

**Tier 1 - Foundation (No Dependencies)**:
- `CSUTLDTC` - Date validation utility
- `COBSWAIT` - Wait utility
- All copybooks (data structures)

**Tier 2 - Authentication & Menu (Foundation Only)**:
- `COSGN00C` - Authentication
- `COMEN01C` - Main menu
- `COADM01C` - Admin menu

**Tier 3 - Online Transactions (Menu + Auth)**:
- Account Management: `COACTVWC`, `COACTUPC`
- Card Management: `COCRDLIC`, `COCRDSLC`, `COCRDUPC`
- Transaction: `COTRN00C`, `COTRN01C`, `COTRN02C`
- User Management: `COUSR00C-03C`
- Reporting: `CORPT00C`, `COBIL00C`

**Tier 4 - Batch Processing (File-Level Dependencies)**:
- Transaction batch: `CBTRN01C` (validation), `CBTRN02C` (posting), `CBTRN03C` (reporting)
- Account batch: `CBACT01C-04C`
- Statement batch: `CBSTM03A`, `CBSTM03B`
- Utilities: `CBIMPORT`, `CBEXPORT`

---

## File Usage Matrix

| File | Programs | Purpose |
|------|----------|---------|
| ACCTFILE | 12+ | Account master - most heavily used |
| TRANFILE | 8+ | Transaction master - second most used |
| CARDFILE | 6+ | Card master |
| CUSTFILE | 4+ | Customer master |
| XREFFILE | 10+ | Cross-reference - critical for lookups |
| USRSEC | 5+ | User security/credentials |
| STMTFILE | 2 | Statement output |
| DALYTRAN | 2 | Daily transaction input (staging) |
| TRANTYPE | 2 | Transaction type lookup |
| TRANCATG | 2 | Transaction category lookup |

---

## Critical Processing Paths

### Daily Transaction Processing
1. Online entry: `COTRN02C` → `DALYTRAN` file
2. Batch validation: `CBTRN01C` reads `DALYTRAN`, validates via `XREFFILE` + `ACCTFILE`
3. Batch posting: `CBTRN02C` reads `DALYTRAN`, posts to `TRANFILE`, updates `ACCTFILE` balances
4. Reporting: `CBTRN03C` generates transaction reports

### Monthly Statement Generation
1. Interest calculation: `CBACT04C` reads `TRANFILE`, updates `ACCTFILE` with interest
2. Statement generation: `CBSTM03A` (main) calls `CBSTM03B` (I/O), writes `STMTFILE`
3. Output: Both text and HTML format statements

### Account/Customer Update
1. Online inquiry: `COACTVWC` reads `ACCTFILE` + `CUSTFILE` + `XREFFILE` (display only)
2. Online update: `COACTUPC` updates `ACCTFILE` + `CUSTFILE` with 30+ validations

---

## Summary

- **30 Programs** (17 online, 8 batch, 5 utilities)
- **30 Copybooks** (entities, lookups, utilities, procedures, report layouts)
- **17 Screens** (BMS maps for all online programs)
- **38 JCL Jobs** (daily, monthly, utilities - not yet documented)
- **10 Data Files** (VSAM KSDS primarily, some sequential)

**Key Characteristics**:
- Modular design with clear separation of concerns
- Shared copybooks for data structures and utilities
- Common communication area for session management
- Transaction-oriented architecture (CICS online, batch processing)
- Comprehensive validation (dates, addresses, business rules)

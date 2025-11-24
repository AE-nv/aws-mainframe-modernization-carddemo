# Job Analysis: CBADMCDJ

## Overview
**Source File**: `app/jcl/CBADMCDJ.jcl`
**Frequency**: Initial setup / CICS configuration
**Type**: CICS resource definition

## Purpose
Defines CICS resources for CardDemo application including programs, map sets, transactions, libraries, and file definitions. Critical for CICS region configuration.

## Processing Steps
1. **STEP1**: Execute DFHCSDUP (CICS System Definition Utility)
   - Reads CICS CSD (System Definition file)
   - Defines resources in group CARDDEMO
   - Lists all defined resources

## CICS Resources Defined

### Library (1)
- **COM2DOLL**: Load library (AWS.M2.CARDDEMO.LOADLIB)

### Map Sets (12+)
- COSGN00M - Login screen
- COACT00S - Account menu
- COACTVWS - View account
- COACTUPS - Update account
- COACTDES - Deactivate account
- COTRN00S - Transaction menu
- COTRNVWS - Transaction report
- COTRNVDS - Transaction details
- COTRNATS - Add transactions
- COBIL00S - Bill pay setup
- COADM00S - Admin menu
- Test map sets (COTSTP1S-4S)

### Programs (15+)
- COSGN00C - Login (TRANSID=CC00)
- COACT00C - Account main menu
- COACTVWC - View account
- COACTUPC - Update account
- COACTDEC - Deactivate account
- COTRN00C - Transaction menu
- COTRNVWC - Transaction report
- COTRNVDC - Transaction details
- COTRNATC - Add transactions
- COBIL00C - Bill pay
- COADM00C - Admin menu (TRANSID=CCAD)
- Test programs (COTSTP1C-4C with CCT1-4 transaction IDs)

### Transactions (5+)
- **CC00**: Login transaction (COSGN00C)
- **CCAD** (CCDM): Admin menu (COADM00C)
- **CCT1-CCT4**: Test transactions

## CICS Group
- **Group Name**: CARDDEMO
- **Purpose**: Logical grouping of related resources
- **Installation**: INSTALL GROUP(CARDDEMO) or CEDA IN G(CARDDEMO)

## Resource Configuration
- All programs: DA(ANY) - Dynamic loading allowed
- All programs: Associated with map sets
- Initial transaction: CC00 (login)
- Load library: AWS.M2.CARDDEMO.LOADLIB

## Dependencies
- CICS TS V05R06M0
- CICS CSD file: OEM.CICSTS.DFHCSD
- Load library with compiled programs and map sets
- DFHCSDUP utility

## Delete/Redefine Pattern
- Comments indicate DELETE GROUP(CARDDEMO) available
- For rerunning, uncomment delete to replace definitions

## Modernization Notes
- CICS resource definitions → Modern application configuration
- Programs → Microservices or web API endpoints
- Transactions → HTTP routes or message queue handlers
- Map sets → Web UI components (React, Angular, Blazor)
- Consider:
  - ASP.NET Core routing configuration
  - API Gateway route definitions (Azure API Management)
  - Container orchestration (Kubernetes)
- TRANSID → HTTP endpoint paths:
  - CC00 → POST /api/auth/login
  - CCAD → GET /api/admin/menu
- Resource group → Kubernetes namespace or Azure Resource Group
- Load library → Docker container images or Azure App Service deployment
- No CICS-specific resources needed in cloud-native architecture

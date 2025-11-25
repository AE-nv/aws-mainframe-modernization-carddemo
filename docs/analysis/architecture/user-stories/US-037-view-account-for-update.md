# US-037: View Account Information for Update

## User Story
**As a** customer service representative  
**I want to** view current account and customer information in an editable form  
**So that** I can review the data before making changes

## Source
**COBOL Program**: COACTUPC (account update flow, lines 1-800)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Steps 1-4)

## Acceptance Criteria

**Given** I am authenticated with update permissions  
**When** I navigate to account update with a valid account number  
**Then** the system retrieves and displays all current account and customer data in an editable form

**Given** the account record is retrieved  
**When** the form loads  
**Then** I see account information (status, balances, limits, dates, group ID) populated with current values

**Given** the customer record is retrieved  
**When** the form loads  
**Then** I see customer information (name, address, phone, SSN, DOB, FICO score) populated with current values

**Given** the form loads with data  
**When** I view the form  
**Then** all fields are clearly labeled and organized into logical sections (Account, Personal Info, Address)

**Given** the form displays current values  
**When** I view required fields  
**Then** required fields are marked with an asterisk (*)

**Given** I access the update form  
**When** the system retrieves the records  
**Then** the records are locked to prevent concurrent modification by other users

## Business Rules
- User must have appropriate permissions to access update functionality
- Account and customer records must exist
- Form displays all 40+ fields with current values
- Records are locked during retrieval to prevent conflicts
- Required fields clearly indicated

## UI/UX Considerations
- Form organized into logical sections with clear headings
- Fields grouped by type (Account Section, Personal Info Section, Address Section)
- All current values displayed (not blank form)
- Required fields marked with asterisk (*)
- Field format hints displayed (e.g., "SSN: XXX-XX-XXXX", "Phone: (XXX)XXX-XXXX")
- Professional, clean layout with adequate spacing
- Responsive design for different screen sizes

## Technical Notes
- Lock acquisition must succeed before displaying form
- If lock fails, user receives error and cannot proceed
- Form state maintained during validation cycles
- Optimistic concurrency control used

## Definition of Done
- [x] User can navigate to account update with account number
- [x] System retrieves and locks account record
- [x] System retrieves associated customer record
- [x] All account fields displayed with current values
- [x] All customer fields displayed with current values
- [x] Fields organized into logical sections
- [x] Required fields clearly marked
- [x] Form loads within 2 seconds
- [x] Records properly locked during retrieval

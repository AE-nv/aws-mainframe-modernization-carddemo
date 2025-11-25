# US-039: Submit Changes for Validation

## User Story
**As a** customer service representative  
**I want to** submit my changes for validation  
**So that** the system can verify all modifications meet business rules before saving

## Source
**COBOL Program**: COACTUPC (validation framework, lines 1200-3500)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Steps 6-9)

## Acceptance Criteria

**Given** I have modified one or more fields  
**When** I submit the form  
**Then** the system validates all modified fields against business rules

**Given** I submit changes  
**When** validation runs  
**Then** the system checks account status values (Y/N only)

**Given** I submit changes  
**When** validation runs  
**Then** the system validates credit limit >= current balance

**Given** I submit changes  
**When** validation runs  
**Then** the system validates date logical relationships (opened <= expiration)

**Given** I submit changes  
**When** validation runs  
**Then** the system validates SSN format (XXX-XX-XXXX) and validity

**Given** I submit changes  
**When** validation runs  
**Then** the system validates phone number format (US format)

**Given** I submit changes  
**When** validation runs  
**Then** the system validates state code against valid US states

**Given** I submit changes  
**When** validation runs  
**Then** the system validates ZIP code format and state matching

**Given** I submit changes  
**When** validation runs  
**Then** the system validates FICO score range (300-850)

**Given** all validations pass  
**When** validation completes  
**Then** the Save and Cancel buttons become enabled/active

**Given** validation displays results  
**When** I view the form  
**Then** I see a validation summary indicating success or specific errors

## Business Rules
- BR-002 Rule 002-2: Credit limit constraint (credit limit >= current balance)
- BR-002 Rule 002-3: Date logical relationships (open date <= expiration date)
- BR-002 Rule 002-7: FICO score range (300-850)
- All field-level validation rules from COACTUPC validation framework
- Comprehensive validation before enabling Save button

## UI/UX Considerations
- Validation triggered on form submit or explicit "Validate" button
- Clear validation summary displayed at top or bottom of form
- Validation in progress indicator shown during processing
- Validation results clearly communicated (success message or error list)
- Save/Cancel buttons hidden or disabled until validation passes
- Form remains editable during validation cycle

## Technical Notes
- Server-side validation required for all business rules
- Client-side validation provides immediate feedback but not sufficient alone
- Validation results returned with specific field references
- Multiple validation errors returned in single response
- Validation does not modify data, only checks rules

## Definition of Done
- [x] User can submit form for validation
- [x] System validates all modified fields
- [x] All business rules enforced during validation
- [x] Validation results clearly displayed
- [x] Save button enabled only after successful validation
- [x] Validation completes within 2 seconds
- [x] Multiple errors handled gracefully
- [x] Form state preserved during validation

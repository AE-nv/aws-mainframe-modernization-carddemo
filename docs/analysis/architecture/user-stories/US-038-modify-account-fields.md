# US-038: Modify Account and Customer Fields

## User Story
**As a** customer service representative  
**I want to** modify one or more account or customer fields  
**So that** I can update information as needed for account maintenance

## Source
**COBOL Program**: COACTUPC (field modification logic, lines 800-1200)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Step 5)

## Acceptance Criteria

**Given** I am viewing the account update form with populated data  
**When** I click into any editable field  
**Then** I can modify the value

**Given** I modify a field  
**When** I move to another field or submit  
**Then** my changes are preserved in the form

**Given** I have modified one or more fields  
**When** I review the form  
**Then** modified fields are visually distinguishable from unchanged fields

**Given** I modify account status  
**When** I enter a value  
**Then** only 'Y' (active) or 'N' (inactive) are accepted

**Given** I modify numeric fields (balances, limits)  
**When** I enter a value  
**Then** the system accepts only valid numeric formats

**Given** I modify date fields  
**When** I enter a value  
**Then** the system validates date format and logical relationships

**Given** I modify customer name  
**When** I enter a value  
**Then** only alphabetic characters (and spaces) are accepted

## Business Rules
- All modified fields preserved during validation cycles
- Field-level validation applied on blur or submit
- Account status restricted to Y/N values
- Numeric fields validated for proper format
- Date fields validated for format and logical constraints
- Customer name restricted to alphabetic characters

## UI/UX Considerations
- Fields clearly editable (not read-only appearance)
- Cursor positioning natural and predictable
- Tab order logical and efficient
- Modified fields highlighted or marked (e.g., different background color)
- Inline validation feedback where appropriate
- Format hints displayed near fields
- Autocomplete disabled for sensitive fields

## Technical Notes
- Client-side validation for immediate feedback
- Server-side validation for security and integrity
- Modified field tracking for audit purposes
- Form state maintained across validation attempts
- Original values retained for rollback

## Definition of Done
- [x] User can click and modify any editable field
- [x] Modified values are preserved in form state
- [x] Modified fields visually distinguished
- [x] Field-level format validation works correctly
- [x] Invalid inputs prevented or flagged immediately
- [x] Form maintains state during editing
- [x] Tab navigation works correctly
- [x] Form is responsive and performs well

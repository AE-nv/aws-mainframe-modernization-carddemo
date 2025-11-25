# US-042: Handle Validation Failures

## User Story
**As a** customer service representative  
**I want to** receive clear feedback when validation fails  
**So that** I can correct errors and successfully complete the update

## Source
**COBOL Program**: COACTUPC (validation error handling, lines 1500-2000)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Alternative Flow: Validation Fails)

## Acceptance Criteria

**Given** I submit changes and one or more validations fail  
**When** validation completes  
**Then** I see an error message indicating which field failed and why

**Given** a validation error occurs  
**When** the error displays  
**Then** the error field is highlighted in red or with an error indicator

**Given** a validation error occurs  
**When** the form displays  
**Then** the cursor is positioned at the first error field

**Given** validation fails  
**When** I view the form  
**Then** all my other inputs are preserved (I don't lose my work)

**Given** I receive a validation error  
**When** I correct the error and resubmit  
**Then** the system validates again and shows either success or the next error

**Given** multiple fields have errors  
**When** I submit initially  
**Then** the system displays the first error encountered

**Given** I correct the first error and resubmit  
**When** validation runs  
**Then** the system displays the next error (if any)

**Given** validation fails  
**When** I view the error  
**Then** the error message is specific and actionable (not generic "invalid input")

## Business Rules
- Validation errors must be clear and specific
- User receives feedback on first error, then subsequent errors
- Invalid inputs preserved so user can see and correct them
- Cursor positioned at error field for easy correction
- Save button remains disabled while validation errors exist

## UI/UX Considerations
- Error messages displayed prominently (near field or in summary box)
- Error fields highlighted in red with error icon
- Error message includes field name and specific problem
- Error messages use plain language (not technical jargon)
- Examples: "Credit Limit must be greater than or equal to Current Balance ($5,000)"
- Error summary at top of form if multiple errors
- Cursor automatically focused on first error field
- Inline validation where possible for immediate feedback

## Error Message Examples
- "Account Status must be 'Y' or 'N'"
- "Credit Limit ($4,000) must be >= Current Balance ($5,000)"
- "Opened Date (2024-06-15) cannot be after Expiration Date (2024-01-01)"
- "SSN format invalid. Use XXX-XX-XXXX"
- "Phone number format invalid. Use (XXX)XXX-XXXX"
- "State code 'XX' is not valid"
- "ZIP code does not match state"
- "FICO score must be between 300 and 850"

## Technical Notes
- Server-side validation returns field-specific error messages
- Client-side validation for format checking (immediate feedback)
- Error response includes field name, error code, and user-friendly message
- Form state preserved across validation attempts
- Multiple validation cycles supported

## Definition of Done
- [x] Validation errors displayed clearly
- [x] Error fields highlighted in red
- [x] Cursor positioned at first error field
- [x] User inputs preserved during validation cycles
- [x] Error messages specific and actionable
- [x] Sequential error discovery works correctly
- [x] Save button remains disabled while errors exist
- [x] User can correct and resubmit successfully

# US-041: Cancel Update Operation

## User Story
**As a** customer service representative  
**I want to** cancel an update operation and discard my changes  
**So that** I can exit without saving when I change my mind or make a mistake

## Source
**COBOL Program**: COACTUPC (cancel logic, lines 3800-3900)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Alternative Flow: Cancel)

## Acceptance Criteria

**Given** I am viewing the account update form with or without modifications  
**When** I click the Cancel button  
**Then** the system releases all record locks immediately

**Given** I click Cancel  
**When** the operation completes  
**Then** all my changes are discarded and not saved to the database

**Given** I click Cancel  
**When** the operation completes  
**Then** I am returned to the previous page (account list or inquiry)

**Given** I cancel the update  
**When** I return to the previous page  
**Then** I see a brief message: "Update cancelled"

**Given** I have made significant changes  
**When** I click Cancel  
**Then** I receive a confirmation dialog: "You have unsaved changes. Cancel anyway?"

**Given** the confirmation dialog appears  
**When** I confirm cancellation  
**Then** changes are discarded and I return to the previous page

**Given** the confirmation dialog appears  
**When** I dismiss the dialog  
**Then** I remain on the update form with my changes intact

## Business Rules
- Cancel always available (even if validation hasn't passed)
- No data saved when Cancel clicked
- Record locks released immediately
- Confirmation required if changes made
- No confirmation needed if no fields modified

## UI/UX Considerations
- Cancel button clearly visible and labeled
- Cancel button positioned near Save button (typically: Save | Cancel)
- Cancel requires confirmation only if changes made
- Confirmation dialog clearly states consequences
- Confirmation dialog buttons: "Yes, Cancel" and "No, Continue Editing"
- Cancel action completes quickly (< 1 second)
- Accessible cancel option (keyboard: Escape key)

## Technical Notes
- Lock release in all cancel paths (with or without confirmation)
- Client-side change tracking to determine if confirmation needed
- Server-side cleanup of any temporary data
- No audit entry for cancel (no changes made)
- Navigation uses browser history or explicit page reference

## Definition of Done
- [x] User can click Cancel button at any time
- [x] Confirmation dialog appears if changes made
- [x] Confirmation dialog does not appear if no changes
- [x] Cancel discards all unsaved changes
- [x] Record locks released immediately
- [x] User returned to previous page
- [x] Brief cancellation message displayed
- [x] Cancel completes within 1 second
- [x] Escape key triggers cancel (with confirmation if needed)

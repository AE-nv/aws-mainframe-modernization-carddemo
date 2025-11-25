# US-040: Save Validated Changes

## User Story
**As a** customer service representative  
**I want to** save validated changes by clicking the Save button  
**So that** account and customer records are updated with my modifications

## Source
**COBOL Program**: COACTUPC (save logic, lines 3500-3800)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Steps 10-15)

## Acceptance Criteria

**Given** validation has passed and Save button is enabled  
**When** I click the Save button  
**Then** the system performs a final concurrency check to detect if records were changed by another user

**Given** concurrency check passes  
**When** save operation executes  
**Then** the system updates both account and customer records atomically (both succeed or both fail)

**Given** both records update successfully  
**When** save completes  
**Then** the system releases the record locks

**Given** save operation succeeds  
**When** operation completes  
**Then** I see a success message confirming the update

**Given** save succeeds  
**When** message displays  
**Then** I am returned to the account list or inquiry page

**Given** save operation executes  
**When** operation completes  
**Then** the update is audited with my user ID, timestamp, and which fields changed

**Given** save completes successfully  
**When** I navigate away  
**Then** subsequent views show the updated values I entered

## Business Rules
- BR-002 Rule 002-4: Transactional integrity (both files update or neither)
- BR-002 Rule 002-5: Optimistic concurrency control (detect concurrent changes)
- Save requires explicit confirmation (button click)
- All updates must be audited
- Record locks released after save

## UI/UX Considerations
- Save button clearly labeled and visually prominent
- Save operation shows progress indicator
- Success message displayed prominently (green banner or modal)
- Success message includes summary: "Account [number] updated successfully"
- Automatic redirect after brief success message (2-3 seconds)
- User can click to return immediately without waiting
- Accessible save confirmation

## Security Considerations
- Save action requires authenticated session
- User permissions verified before save
- Audit trail includes user ID, timestamp, modified fields
- Sensitive field changes logged appropriately

## Technical Notes
- Transactional update (database transaction)
- Optimistic concurrency check before commit
- Lock release in finally block (even if error)
- Audit log written synchronously before success message
- Return to previous page or configurable destination

## Definition of Done
- [x] User can click Save button after validation passes
- [x] System performs concurrency check before saving
- [x] Both account and customer records updated atomically
- [x] Record locks released after operation
- [x] Success message displayed to user
- [x] User returned to appropriate page
- [x] Update audited with full details
- [x] Save completes within 3 seconds
- [x] Transaction integrity maintained

# US-044: Handle Record Lock Failures

## User Story
**As a** customer service representative  
**I want to** receive clear feedback if a record is locked by another user  
**So that** I understand why I can't edit the account and can try again later

## Source
**COBOL Program**: COACTUPC (lock acquisition, lines 200-400)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Exception Flow: Lock Failure)

## Acceptance Criteria

**Given** I navigate to update an account that another user is currently editing  
**When** I attempt to open the update form  
**Then** the system cannot acquire the record lock

**Given** the system cannot acquire the lock  
**When** lock acquisition fails  
**Then** I see an error message: "Unable to lock record for update"

**Given** I see the lock failure message  
**When** the message displays  
**Then** it suggests I try again later

**Given** lock acquisition fails  
**When** the error occurs  
**Then** I am returned to the account inquiry page (not stuck on error screen)

**Given** I am returned to inquiry after lock failure  
**When** I view the page  
**Then** I can still view account details in read-only mode

**Given** the lock is held by another user  
**When** that user completes their update or times out  
**Then** the lock is released and I can try again

## Business Rules
- Only one user can edit a record at a time
- Lock acquisition required before displaying update form
- Lock failure prevents access to update functionality
- User can view data in read-only mode even when locked
- Locks automatically released on timeout or completion

## UI/UX Considerations
- Error message is friendly and informative
- Message explains: "Another user is currently editing this account"
- Message suggests: "Please try again in a few minutes"
- Option to "Retry Now" button (attempts lock again)
- Option to "View Read-Only" button (shows account without locking)
- No form displayed if lock fails (prevents confusion)
- Error doesn't feel like a system failure (expected behavior)

## Error Message Examples
- "This account is currently being edited by another user. Please try again later."
- "Unable to edit account [number] at this time. Another user may be making changes."
- "Account locked for editing. Would you like to view in read-only mode or try again?"

## Lock Management Details
- Lock timeout: 15-30 minutes (configurable)
- Lock released on save, cancel, or timeout
- Lock includes user ID who holds it (for admin visibility)
- Lock acquisition atomic operation
- Multiple retry attempts allowed

## Technical Notes
- Database-level locking or distributed lock service
- Lock acquisition checked before displaying update form
- If lock fails, no form rendered
- Lock status queryable (who holds lock, when acquired)
- Admin can force-release locks if needed (emergency)
- Lock release guaranteed even if browser crashes (timeout)

## Definition of Done
- [x] System attempts to acquire lock before update
- [x] Clear error message if lock acquisition fails
- [x] User returned to inquiry page on lock failure
- [x] User can retry lock acquisition
- [x] User can view data in read-only mode
- [x] Lock automatically released on timeout
- [x] Error message is clear and helpful
- [x] User experience handles failure gracefully

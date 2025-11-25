# US-043: Detect Concurrent Modification Conflicts

## User Story
**As a** customer service representative  
**I want to** be notified if someone else modifies a record while I'm editing it  
**So that** I don't accidentally overwrite their changes with my outdated data

## Source
**COBOL Program**: COACTUPC (optimistic locking, lines 3500-3700)  
**Business Requirement**: BR-002 (Account Management - BR-002 Rule 002-5: Optimistic Concurrency)  
**Use Case**: UC-006 (Update Account and Customer Information - Exception Flow: Concurrent Modification)

## Acceptance Criteria

**Given** I am editing an account and another user saves changes to the same account  
**When** I click Save  
**Then** the system detects the concurrent modification and prevents my save

**Given** concurrent modification is detected  
**When** the system responds  
**Then** I see an error message: "Data was changed by another user"

**Given** concurrent modification is detected  
**When** the error displays  
**Then** the system re-reads the current record values from the database

**Given** the system re-reads current values  
**When** the form refreshes  
**Then** I see the updated values as saved by the other user

**Given** I see the updated values  
**When** I review the form  
**Then** I can compare their changes to my intended changes

**Given** I see the conflict  
**When** I review the situation  
**Then** I can choose to re-apply my changes or cancel the update

**Given** concurrent modification is detected  
**When** the system responds  
**Then** my changes are NOT automatically committed (I must review and re-submit)

## Business Rules
- BR-002 Rule 002-5: Optimistic concurrency control prevents lost updates
- Concurrency check occurs immediately before save
- System never silently overwrites changes made by other users
- User must review concurrent changes and decide how to proceed
- Record locks refreshed after conflict detected

## UI/UX Considerations
- Conflict message clearly explains what happened
- Message includes: "Another user has modified this record since you started editing"
- User sees which fields were changed by other user (if possible)
- Current database values displayed with clear indication they're "latest"
- User's pending changes clearly distinguished from database values
- Options: "Re-apply My Changes" or "Cancel and Review"
- Side-by-side comparison view (optional enhancement)

## Concurrency Scenarios

**Scenario 1: Simple field conflict**
- User A loads account 1234 at 10:00 AM
- User B loads account 1234 at 10:01 AM
- User B changes credit limit to $10,000 and saves at 10:02 AM
- User A changes address and tries to save at 10:03 AM
- System detects conflict, shows User A the new credit limit
- User A can proceed with address change (non-conflicting field)

**Scenario 2: Same field conflict**
- User A loads account 1234
- User B loads account 1234
- User B changes credit limit to $10,000 and saves
- User A changes credit limit to $12,000 and tries to save
- System detects conflict, shows current limit is $10,000
- User A must decide: keep $10,000 or override with $12,000

## Technical Notes
- Optimistic concurrency using version number or timestamp
- Version checked immediately before database update
- On conflict: read current record, compare to user's version
- Return both current values and user's pending changes
- User can merge or override after review
- Audit log records both conflict detection and final outcome

## Definition of Done
- [x] System detects concurrent modifications before save
- [x] Clear error message displayed on conflict
- [x] Current database values re-read and displayed
- [x] User's pending changes preserved for review
- [x] User can choose to re-apply changes or cancel
- [x] System never silently overwrites other users' changes
- [x] Conflict detection works reliably (no false positives/negatives)
- [x] User experience is clear and non-confusing

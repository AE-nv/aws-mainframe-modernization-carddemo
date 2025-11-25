# US-045: Handle Partial Update Failures with Rollback

## User Story
**As a** customer service representative  
**I want to** be confident that updates either fully succeed or fully fail  
**So that** account and customer data remain synchronized and consistent

## Source
**COBOL Program**: COACTUPC (two-phase commit logic, lines 3600-3800)  
**Business Requirement**: BR-002 (Account Management - BR-002 Rule 002-4: Transactional Integrity)  
**Use Case**: UC-006 (Update Account and Customer Information - Exception Flow: Partial Failure)

## Acceptance Criteria

**Given** I save changes to account and customer records  
**When** the account update succeeds but customer update fails  
**Then** the system automatically rolls back the account update

**Given** a partial update failure occurs  
**When** rollback completes  
**Then** I see an error message: "Update failed - no changes were saved"

**Given** rollback occurs  
**When** the operation completes  
**Then** both account and customer records remain unchanged (as before my edit)

**Given** update fails and rolls back  
**When** I return to inquiry  
**Then** I see the original values, not my attempted changes

**Given** update fails  
**When** the error displays  
**Then** the system releases all record locks

**Given** update fails  
**When** I review the error  
**Then** I can retry the entire operation if desired

**Given** partial failure occurs  
**When** operations complete  
**Then** the failure and rollback are logged for troubleshooting

## Business Rules
- BR-002 Rule 002-4: Both files must update or neither updates (transactional integrity)
- No partial updates allowed
- Automatic rollback on any failure
- Record locks released after rollback
- All failures audited for investigation

## UI/UX Considerations
- Error message clearly states no changes were saved
- Message reassures user data integrity maintained
- Error provides general reason: "Database error" or "Validation failure during save"
- Option to retry the update
- Option to cancel and return to list
- User's input preserved so they can retry without re-entering

## Technical Notes
- Database transaction wraps both updates
- Transaction commit only if both succeed
- Automatic rollback if either fails
- Lock release in finally block (guaranteed)
- Error details logged server-side for troubleshooting
- User sees friendly message, not technical error
- Retry capability preserves user's changes

## Failure Scenarios

**Scenario 1: Customer update fails**
1. Account update succeeds
2. Customer update encounters database error
3. Transaction rolls back account update
4. Neither record modified
5. User sees error message

**Scenario 2: Validation failure during save**
1. Account update succeeds
2. Customer update triggers late validation failure
3. Transaction rolls back account update
4. Neither record modified
5. User sees error with validation details

**Scenario 3: Network timeout during save**
1. Account update sent
2. Network timeout before customer update
3. Transaction rolls back all changes
4. Neither record modified
5. User can retry entire operation

## Definition of Done
- [x] Both updates wrapped in transaction
- [x] Automatic rollback if either update fails
- [x] Clear error message on failure
- [x] No partial updates occur
- [x] Record locks released after failure
- [x] Failure logged for troubleshooting
- [x] User can retry after failure
- [x] Data integrity maintained in all scenarios

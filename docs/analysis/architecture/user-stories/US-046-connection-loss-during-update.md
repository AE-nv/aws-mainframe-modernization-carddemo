# US-046: Handle Connection Loss During Update

## User Story
**As a** customer service representative  
**I want to** be protected from data loss if I lose connection during an update  
**So that** I can safely complete my work even with network interruptions

## Source
**COBOL Program**: COACTUPC (timeout and session handling, lines 100-200)  
**Business Requirement**: BR-002 (Account Management - FR-002.2)  
**Use Case**: UC-006 (Update Account and Customer Information - Exception Flow: Connection Loss)

## Acceptance Criteria

**Given** I am editing an account and lose network connection  
**When** the connection is lost  
**Then** the system automatically releases record locks after a timeout period

**Given** my connection drops during editing (before save)  
**When** timeout occurs  
**Then** my changes are not saved (no partial data written)

**Given** I reconnect after connection loss  
**When** I attempt to access the application  
**Then** I must re-authenticate if my session expired

**Given** I reconnect after brief connection loss  
**When** my session is still valid  
**Then** I can return to the update form and restart my edit

**Given** I lose connection during a save operation  
**When** the operation times out  
**Then** either the full save completes or no changes are saved (transactional integrity)

**Given** connection is lost during save  
**When** I reconnect  
**Then** I can verify whether the save succeeded or failed by viewing the account

## Business Rules
- Record locks auto-release on timeout (prevents permanent locks)
- No data saved if connection lost before save completes
- Transaction either completes fully or rolls back fully
- Session timeout independent of lock timeout
- User must re-authenticate after session timeout

## UI/UX Considerations
- Connection loss detected and user notified
- Message: "Connection lost. Attempting to reconnect..."
- Automatic reconnection attempted
- On reconnect, user informed of session status
- If session expired: "Your session has expired. Please log in again."
- If session valid: "Connection restored. You can continue working."
- Clear guidance on what happened to in-progress work

## Timeout Scenarios

**Scenario 1: Connection lost while editing (before save)**
- User editing form, connection drops
- Lock timeout: 15 minutes
- Session timeout: 30 minutes
- After 15 min: lock released
- After 30 min: session expires
- User reconnects, must log in, restart edit

**Scenario 2: Connection lost during save**
- User clicks Save, operation starts
- Connection drops mid-save
- Transaction timeout: 30 seconds
- Transaction rolls back (no partial save)
- User reconnects, changes not saved
- User can retry save operation

**Scenario 3: Brief connection blip**
- User editing, 5-second connection loss
- Connection restores automatically
- Lock still held, session still valid
- User continues editing without interruption

## Technical Notes
- Lock timeout: 15-30 minutes (configurable)
- Session timeout: 30-60 minutes (configurable)
- Transaction timeout: 30-60 seconds
- Auto-reconnection logic in client
- Server-side cleanup on timeout
- Connection loss logged for monitoring
- Lock release guaranteed by timeout mechanism

## Definition of Done
- [x] Record locks released on timeout
- [x] No partial saves occur on connection loss
- [x] User notified of connection status
- [x] Auto-reconnection attempted
- [x] Clear guidance on session/work status
- [x] Transaction integrity maintained
- [x] User can verify save status after reconnect
- [x] Timeout values properly configured

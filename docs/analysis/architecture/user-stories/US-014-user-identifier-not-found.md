# US-014: User Identifier Not Found

## User Story
**As a** user attempting to login  
**I want to** be notified when my user identifier is not recognized  
**So that** I can correct the typo or verify my account exists

## Source
**COBOL Program**: COSGN00C (user validation logic)  
**Business Requirement**: BR-001 (User Authentication - FR-001.4)  
**Use Case**: UC-003 (Authentication Failure Recovery - Alternative Flow 3a)

## Acceptance Criteria

**Given** I enter a user identifier that does not exist in the system  
**When** I submit the login form  
**Then** I see the error message "User identifier not recognized. Please verify and try again."

**Given** user identifier is not found  
**When** the error is displayed  
**Then** both the user identifier and password fields are cleared for security

**Given** user identifier is not found  
**When** the error is displayed  
**Then** the cursor is positioned in the user identifier field

**Given** non-existent user identifier entered  
**When** authentication fails  
**Then** the failed attempt is logged without revealing whether user exists (security)

**Given** I correct my user identifier after the error  
**When** I submit the login form with correct identifier and password  
**Then** authentication succeeds

## Business Rules
- Error message does not explicitly confirm user non-existence (security)
- Both fields cleared when user not found (security best practice)
- Failed attempt logged with attempted identifier
- No attempt counter shown for non-existent users (prevents enumeration)
- Case-insensitive user identifier lookup

## UI/UX Considerations
- Error message worded to suggest verification without confirming non-existence
- User identifier field visually highlighted
- Password field also cleared to prevent security leak
- Automatic focus on user identifier field
- Error message accessible to screen readers
- Helpful but security-conscious messaging
- Mobile-friendly error display

## Technical Notes
- Security consideration: Don't distinguish between "wrong password" and "user not found" in timing
- Log attempted identifier for security monitoring
- Protect against user enumeration attacks
- Consider rate limiting for repeated non-existent user attempts
- Failed attempt not counted toward account lockout (no account to lock)

## Definition of Done
- [x] Non-existent user identifier displays appropriate error message
- [x] Both user identifier and password fields cleared
- [x] Cursor positioned in user identifier field
- [x] Failed attempt logged for security monitoring
- [x] Error message does not reveal account existence
- [x] Error message is accessible
- [x] User can immediately retry
- [x] Timing of response similar to password failure (security)

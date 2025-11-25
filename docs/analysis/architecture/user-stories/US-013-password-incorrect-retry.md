# US-013: Password Incorrect with Retry

## User Story
**As a** user attempting to login  
**I want to** see a clear error message when I enter the wrong password  
**So that** I understand the issue and can immediately retry with the correct password

## Source
**COBOL Program**: COSGN00C (error handling, password validation)  
**Business Requirement**: BR-001 (User Authentication - FR-001.4)  
**Use Case**: UC-003 (Authentication Failure Recovery - Main Flow Steps 2-10)

## Acceptance Criteria

**Given** I have a valid user identifier but enter an incorrect password  
**When** I submit the login form  
**Then** I see the error message "Password incorrect. Please try again."

**Given** password authentication fails  
**When** the error is displayed  
**Then** my password field is cleared for security but my user identifier remains in the form

**Given** password authentication fails  
**When** the error is displayed  
**Then** the cursor is automatically positioned in the password field

**Given** this is my first password failure  
**When** the error is displayed  
**Then** I see the attempt counter showing "Attempt 1 of 5"

**Given** any password authentication failure  
**When** the error occurs  
**Then** the failed attempt is logged with my user identifier, timestamp, IP address, and error type

**Given** I correct my password after the error  
**When** I submit the login form again  
**Then** authentication succeeds and I gain access to the application

## Business Rules
- Password field always cleared on authentication failure (security requirement)
- User identifier retained for user convenience
- Failed attempt counter increments with each password failure
- Maximum 5 consecutive failed attempts allowed before lockout
- Case-insensitive password comparison

## UI/UX Considerations
- Error message displayed prominently in red
- Error icon (⚠️) shown next to message
- Password field visually highlighted as having error
- Automatic focus on password field after error
- User identifier field not highlighted (correct value)
- Error message announced to screen readers
- Mobile-friendly error display

## Technical Notes
- Failed attempt counter stored server-side per user
- Counter cannot be manipulated by client
- Counter is user-specific, not session-specific
- Security audit log entry created for each failure
- Password never logged or stored in error messages

## Definition of Done
- [x] Incorrect password displays appropriate error message
- [x] Password field is cleared on error
- [x] User identifier field is retained with original value
- [x] Cursor automatically positioned in password field
- [x] Attempt counter displayed accurately
- [x] Failed attempt logged with required details
- [x] Error message is accessible to screen readers
- [x] User can immediately retry without re-entering identifier
- [x] Successful retry after error works correctly

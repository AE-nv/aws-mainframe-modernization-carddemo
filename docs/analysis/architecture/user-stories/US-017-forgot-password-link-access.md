# US-017: Forgot Password Link Access

## User Story
**As a** user who cannot remember my password  
**I want to** easily access the password reset functionality from the login page  
**So that** I can regain access to my account without contacting support

## Source
**COBOL Program**: COSGN00C (navigation to password reset)  
**Business Requirement**: BR-001 (User Authentication - FR-001.4)  
**Use Case**: UC-003 (Authentication Failure Recovery - Alternative Flow 8a)

## Acceptance Criteria

**Given** I have failed authentication one or more times  
**When** the error message is displayed  
**Then** I see a "Forgot Password?" link below the login form

**Given** I have failed authentication 2 or more times  
**When** the error and warning are displayed  
**Then** the "Forgot Password?" link is displayed more prominently (larger, highlighted)

**Given** I am viewing the login page with an error message  
**When** I click the "Forgot Password?" link  
**Then** I am navigated to the password reset workflow (UC-006)

**Given** I start the password reset workflow  
**When** I complete the reset successfully  
**Then** I am returned to the login page and can authenticate with my new password

**Given** I click "Forgot Password?" on any failed attempt  
**When** the navigation occurs  
**Then** the failed attempt counter for my account is NOT reset (security)

## Business Rules
- "Forgot Password?" link always available after first failed attempt
- Link becomes more prominent after 2+ failed attempts
- Clicking link does not reset failed attempt counter
- Password reset workflow is separate process (UC-006)
- User must complete full password reset process
- New password cannot be same as recently used passwords

## UI/UX Considerations
- Link positioned consistently below password field
- Initial state: Normal link styling
- After 2+ failures: Emphasized styling (bold, larger font, highlight)
- Clear, recognizable text: "Forgot Password?" or "Reset Password"
- Mobile-friendly touch target (minimum 44x44 pixels)
- Link accessible via keyboard navigation
- Link purpose announced to screen readers
- Visual transition to password reset smooth and clear

## Technical Notes
- Link navigates to password reset workflow (UC-006)
- Failed attempt counter preserved across navigation
- Session context maintained for return to login
- Password reset process independent of authentication
- Consider rate limiting password reset requests
- Track password reset initiations for analytics

## Definition of Done
- [x] "Forgot Password?" link visible after any failed attempt
- [x] Link becomes more prominent after 2+ failures
- [x] Link navigates to password reset workflow
- [x] Failed attempt counter not reset by clicking link
- [x] User can complete password reset and return to login
- [x] Successful reset allows authentication with new password
- [x] Link meets accessibility standards
- [x] Mobile-friendly touch target
- [x] Keyboard navigation functional

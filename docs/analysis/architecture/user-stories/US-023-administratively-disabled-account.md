# US-023: Administratively Disabled Account

## User Story
**As a** system administrator  
**I want to** prevent disabled user accounts from authenticating  
**So that** terminated or suspended users cannot access the system

## Source
**COBOL Program**: COSGN00C (account status validation)  
**Business Requirement**: BR-001 (User Authentication - FR-001.2)  
**Use Case**: UC-003 (Authentication Failure Recovery - Exception Flow 2b)

## Acceptance Criteria

**Given** my account has been disabled by an administrator  
**When** I attempt to login with correct credentials  
**Then** authentication is denied and I see the message "Account has been disabled. Please contact your administrator."

**Given** my account is disabled  
**When** authentication is denied  
**Then** no retry option is provided (account status must change to allow access)

**Given** a disabled account attempts login  
**When** authentication is denied  
**Then** the attempt is logged with user identifier, timestamp, and IP address

**Given** my account is disabled  
**When** I attempt to login  
**Then** my password is NOT validated (security: avoid revealing if password is correct)

**Given** a disabled account attempts login  
**When** multiple attempts occur  
**Then** the failed attempt counter is NOT incremented (account cannot be locked further)

**Given** my account is disabled  
**When** the error message is displayed  
**Then** I am directed to contact my administrator (no self-service option)

## Business Rules
- Disabled accounts cannot authenticate regardless of credentials
- Account status checked before password validation (security)
- Disabled status overrides all other authentication logic
- Failed attempt counter not affected by disabled account login attempts
- Only administrator can enable disabled accounts
- Disabled account login attempts logged for security monitoring
- Error message does not reveal whether credentials were correct

## UI/UX Considerations
- Error message clearly states account disabled (not locked)
- Distinct styling from temporary lockout (e.g., different icon)
- No "Forgot Password?" link shown (password not the issue)
- No retry countdown or unlock timer shown (permanent until admin action)
- Administrator contact information provided prominently
- Error message accessible to screen readers
- Mobile-friendly display

## Technical Notes
- Account status field: ACTIVE, DISABLED, LOCKED, SUSPENDED
- Status checked in authentication pipeline before password hash
- Disabled status takes precedence over locked status
- Login attempt logged with account status in security log
- Consider distinguishing between DISABLED (permanent) and SUSPENDED (temporary admin action)
- Disabled accounts may retain data but cannot authenticate
- Password reset disabled for disabled accounts

## Definition of Done
- [x] Disabled accounts cannot authenticate
- [x] Appropriate error message displayed for disabled accounts
- [x] Account status checked before password validation
- [x] Login attempts to disabled accounts logged
- [x] Failed attempt counter not incremented
- [x] No retry or self-service options shown
- [x] Administrator contact information provided
- [x] Error message distinguishable from lockout message
- [x] Password validation skipped for disabled accounts
- [x] Error message accessible to screen readers
- [x] Security monitoring alerted for disabled account attempts

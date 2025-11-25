# US-016: Account Lockout After Maximum Attempts

## User Story
**As a** security-conscious system  
**I want to** lock user accounts after maximum failed authentication attempts  
**So that** brute force attacks are prevented and account security is maintained

## Source
**COBOL Program**: COSGN00C (account lockout logic)  
**Business Requirement**: BR-001 (User Authentication - BR-001.5)  
**Use Case**: UC-003 (Authentication Failure Recovery - Exception Flow 2a)

## Acceptance Criteria

**Given** I have failed authentication 5 consecutive times  
**When** the 5th failure occurs  
**Then** my account is immediately locked

**Given** my account is locked due to failed attempts  
**When** the lockout occurs  
**Then** I see the message "Account temporarily locked due to multiple failed login attempts. Please try again in 30 minutes or contact your administrator."

**Given** my account is locked  
**When** I attempt to login again immediately  
**Then** I see the same lockout message and cannot attempt authentication

**Given** my account is locked  
**When** the lockout occurs  
**Then** a security event is logged with my user identifier, timestamp, IP address, and lockout reason

**Given** my account is locked  
**When** the lockout occurs  
**Then** an optional notification email is sent to my registered email address (if configured)

**Given** my account has been locked for 30 minutes  
**When** the lockout duration expires  
**Then** my account is automatically unlocked and I can attempt login again

**Given** my account is locked  
**When** I cannot wait 30 minutes  
**Then** I have the option to contact my administrator for manual unlock

## Business Rules
- Account locks automatically after 5 consecutive failed attempts (configurable)
- Lockout duration is 30 minutes (configurable)
- Locked accounts cannot authenticate until unlocked
- Failed attempt counter remains at 5 during lockout
- Lockout can be manually removed by administrator
- Automatic unlock occurs after lockout duration expires
- Failed attempt counter resets to 0 after successful authentication post-unlock

## UI/UX Considerations
- Lockout message clearly states temporary nature
- Lockout duration (30 minutes) explicitly mentioned
- Administrator contact option provided
- No login fields shown while account locked
- Lockout message in red with lock icon (🔒)
- Message accessible to screen readers
- Mobile-friendly display
- Optional countdown timer showing time remaining

## Technical Notes
- Lockout timestamp stored with user account
- Automatic unlock scheduled or checked on login attempt
- Lockout events trigger security monitoring alerts
- Consider progressive lockout (longer duration after repeated lockouts)
- IP address logged for security analysis
- Email notification optional (configurable)
- Lockout status checked before authentication attempt

## Definition of Done
- [x] Account locks after 5th consecutive failed attempt
- [x] Lockout message displays correctly
- [x] Locked account cannot authenticate
- [x] Lockout event logged with all required details
- [x] Optional email notification sent (if configured)
- [x] Account automatically unlocks after 30 minutes
- [x] Administrator can manually unlock account
- [x] Failed attempt counter persists during lockout
- [x] Failed attempt counter resets after successful post-unlock login
- [x] Security alerts triggered for lockout events

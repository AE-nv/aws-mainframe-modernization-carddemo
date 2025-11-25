# US-020: Automatic Account Unlock After Lockout Duration

## User Story
**As a** user whose account was temporarily locked  
**I want to** have my account automatically unlocked after the lockout period expires  
**So that** I can regain access without requiring administrator intervention

## Source
**COBOL Program**: COSGN00C (lockout expiration logic)  
**Business Requirement**: BR-001 (User Authentication - BR-001.5)  
**Use Case**: UC-003 (Authentication Failure Recovery - Main Flow Step 11, Exception Flow 2a)

## Acceptance Criteria

**Given** my account was locked 30 minutes ago due to failed attempts  
**When** the 30-minute lockout period has expired  
**Then** my account is automatically unlocked

**Given** my account has been automatically unlocked  
**When** I attempt to login with correct credentials  
**Then** authentication succeeds and I gain access

**Given** my account is automatically unlocked after lockout  
**When** successful authentication occurs  
**Then** the failed attempt counter is reset to 0

**Given** my account was locked and then auto-unlocked  
**When** the unlock occurs  
**Then** an audit log entry is created recording the automatic unlock event

**Given** my account is still within the 30-minute lockout period  
**When** I attempt to login  
**Then** I see the lockout message with time remaining (optional feature)

**Given** my account is locked  
**When** I check the lockout message  
**Then** I see how long until automatic unlock: "Please try again in X minutes" (optional)

## Business Rules
- Lockout duration is 30 minutes (configurable)
- Automatic unlock occurs exactly at lockout duration expiration
- Failed attempt counter remains at 5 during lockout
- Failed attempt counter resets to 0 only after successful authentication post-unlock
- Multiple lockouts may result in longer durations (optional progressive lockout)
- Manual administrator unlock bypasses automatic unlock timer
- Lockout timer based on server time (not client time)

## UI/UX Considerations
- Optional countdown timer showing time until unlock
- Clear indication of temporary lockout nature
- "Account will automatically unlock at [TIME]" message (optional)
- User-friendly time format (e.g., "in 15 minutes" vs. "14:32:17 remaining")
- No need to refresh page—automatic unlock checked on login attempt
- Mobile-friendly display
- Accessible time remaining information

## Technical Notes
- Lockout timestamp stored with user account record
- Unlock checked on each login attempt (lazily)
- Alternative: Scheduled job to unlock accounts (proactively)
- Lockout expiration calculated server-side
- Time remaining calculated dynamically
- Consider timezone handling for lockout timestamps
- Automatic unlock event logged for audit trail
- Failed attempt counter persists across unlock
- Counter only resets after successful authentication

## Definition of Done
- [x] Account automatically unlocks after 30 minutes
- [x] Unlocked account can authenticate successfully
- [x] Unlock check occurs on login attempt
- [x] Failed attempt counter remains at 5 during lockout
- [x] Failed attempt counter resets to 0 after successful post-unlock login
- [x] Automatic unlock event logged
- [x] Optional time remaining displayed in lockout message
- [x] Optional countdown timer functional
- [x] Lockout duration configurable
- [x] Manual unlock takes precedence over automatic
- [x] Server-side time used for lockout calculations

# US-015: Multiple Failed Attempts Warning

## User Story
**As a** user who has failed authentication multiple times  
**I want to** see a warning about impending account lockout  
**So that** I can either reset my password or contact support before being locked out

## Source
**COBOL Program**: COSGN00C (failed attempt tracking logic)  
**Business Requirement**: BR-001 (User Authentication - FR-001.4, BR-001.5)  
**Use Case**: UC-003 (Authentication Failure Recovery - Alternative Flow 3c)

## Acceptance Criteria

**Given** I have failed authentication 2, 3, or 4 times  
**When** I see the authentication error  
**Then** I see the warning message "Attempt X of 5. Account will be locked after 5 failed attempts."

**Given** I am on my 3rd or 4th failed attempt  
**When** the error and warning are displayed  
**Then** the "Forgot Password?" link is shown prominently below the warning

**Given** multiple failed attempts have occurred  
**When** the warning is displayed  
**Then** the warning uses a distinctive color (yellow/amber) to indicate urgency

**Given** I am on my 4th failed attempt  
**When** I see the warning  
**Then** the message emphasizes the severity: "WARNING: Last attempt before lockout"

**Given** I see the multiple attempts warning  
**When** I click "Forgot Password?"  
**Then** I am taken to the password reset workflow

**Given** multiple authentication failures  
**When** each failure occurs  
**Then** the warning level is logged for security analysis

## Business Rules
- Warning displayed starting at 2nd failed attempt
- Attempt counter shows current attempt and maximum (X of 5)
- "Forgot Password?" link becomes more prominent after 2+ failures
- Final attempt (5th) triggers account lockout
- Attempt counter specific to each user account
- Counter resets to 0 on successful authentication

## UI/UX Considerations
- Warning message in yellow/amber (not red like errors)
- Warning icon (⚠️ or 🛡️) to draw attention
- Progressively stronger language (2nd: "Note", 3rd: "Warning", 4th: "CRITICAL")
- "Forgot Password?" link in larger font after 2+ attempts
- Mobile-friendly warning display
- Warning announced to screen readers with appropriate urgency
- Clear visual distinction between error and warning

## Technical Notes
- Failed attempt counter stored server-side
- Counter persists across sessions
- Counter incremented before displaying warning
- Warning level (2nd, 3rd, 4th attempt) logged separately
- Consider sending email notification on 3rd+ attempt (optional)

## Definition of Done
- [x] Warning message displays on 2nd through 4th failed attempts
- [x] Attempt counter shows accurate count (X of 5)
- [x] "Forgot Password?" link prominently shown after 2+ failures
- [x] Warning styling distinct from error messages
- [x] Final attempt warning emphasizes severity
- [x] Link to password reset workflow functional
- [x] Warning accessible to screen readers
- [x] Mobile-friendly display
- [x] Each warning level logged for security

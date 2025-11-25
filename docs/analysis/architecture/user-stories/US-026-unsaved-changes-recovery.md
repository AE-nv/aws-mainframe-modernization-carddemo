# US-026: Unsaved Changes Recovery After Timeout

## User Story
**As a** customer service representative  
**I want** my work context (draft forms, searches, filters) to persist in the database regardless of authentication session status  
**So that** I can seamlessly continue my work after any interruption (timeout, network issue, browser crash, etc.)

## Source
**Business Requirement**: BR-001 (Non-Functional Requirements - Resilience and User Experience)
**Use Case**: UC-004 (Session Timeout - Application State Persistence - Default Behavior)
## Acceptance Criteria

**Given** I am filling out a form with multiple fields  
**When** I type into any field  
**Then** the system automatically saves my draft to the database (debounced after 2 seconds)

**Given** I am filling out a form  
**When** my authentication session times out  
**Then** my draft form data remains in the database unchanged

**Given** my authentication session timed out while I had a form open  
**When** I log back in  
**Then** I am automatically returned to the exact same form with all my data populated

**Given** I am viewing search results with filters applied  
**When** my authentication session times out and I log back in  
**Then** I see the exact same search results and filters without re-entering them

**Given** I am on page 3 of a paginated list  
**When** my authentication session times out and I log back in  
**Then** I am returned to page 3 of the same list

**Given** my draft data has been saved to the database  
**When** I successfully submit the form  
**Then** the draft is marked complete and archived

**Given** I have draft data in the database  
**When** 30 days pass without activity  
**Then** the draft data is automatically purged per retention policy

## Business Rules
- Application state continuously persisted to database as user works (not triggered by timeout)
- Draft data stored securely in database and associated with user account
- Maximum draft retention: 30 days of inactivity
- One draft per form/page per user (most recent)
- Draft data encrypted at rest in database
- Restoration is automatic and transparent (no user prompting)
- Search filters, sort order, pagination persisted per user
- Page/route context stored to enable exact return to previous location

## UI/UX Considerations
- Restoration is completely transparent (user may not notice except for seamless continuation)
- No notification or prompt required (just works)
- Optional: Subtle indicator "Draft auto-saved" while user works
- Warning shown only if record edited by another user (conflict detection)
- User can manually discard draft via "Clear Form" or "Reset" button if desired
- Automatic restoration feels natural (like the app never forgot their work)
- Works consistently across all forms and pages

## Security Considerations
- Draft data encrypted at rest
- Draft data accessible only to the user who created it
- Draft data purged after timeout period
- No sensitive data (like passwords) stored in drafts
- Draft storage complies with data retention policies
- User identifier verified before restoration

## Technical Notes
- Auto-save to database (not localStorage) for persistence across devices/browsers
- Debounce auto-save (2 seconds after last keystroke) to prevent excessive DB writes
- Store user ID + page/form identifier + draft data (JSON) + timestamp
- On login, server returns user's active draft contexts
- Client checks current page against available drafts and restores if match
- Optimistic conflict detection: compare record version before allowing edit
- Service Worker can queue saves during offline periods and sync when reconnected
- Draft table: user_id, form_id, page_context, draft_json, created_at, updated_at

## Data Requirements
**Draft Storage Includes**:
- User identifier (FK to user table)
- Page/form identifier (e.g., "account-update-form", "transaction-search")
- Draft data (JSON: field values, filters, sort, pagination)
- Created timestamp
- Last updated timestamp
- Record version (for conflict detection)
- Status (active, completed, discarded)

**Draft Retention**:
- Maximum age: 30 days of inactivity
- Status changed to "completed" on successful form submission
- Status changed to "discarded" on user explicit discard
- Automated cleanup job purges drafts older than retention period

## Definition of Done
- [x] Form data auto-saved continuously as user types (debounced)
- [x] Auto-save works independent of authentication session status
- [x] User automatically returned to previous context after re-authentication
- [x] Restoration is transparent (no prompting)
- [x] Search filters, sort, pagination persisted per user
- [x] Multiple forms/pages each have independent draft storage
- [x] Conflict detection warns user if record changed by others
- [x] Draft data encrypted at rest in database
- [x] Draft data purged after 30 days of inactivity
- [x] Works across all forms and searchable pages
- [x] Works across devices/browsers (server-side storage)


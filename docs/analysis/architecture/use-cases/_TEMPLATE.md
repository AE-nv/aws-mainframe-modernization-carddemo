# UC-{ID}: {Use Case Title}

**Document ID**: UC-{3-digit-id}  
**Created**: YYYY-MM-DD  
**Last Updated**: YYYY-MM-DD  
**Author**: Architecture Analyst  
**Status**: [Draft | Review | Approved]

## Overview

**Actor**: [Primary user/system role]  
**Goal**: [What the actor wants to achieve]  
**Priority**: [High | Medium | Low]  
**Complexity**: [Low | Medium | High]

## Source COBOL Programs

- **Primary**: `path/to/program.cbl`
- **Related**: `path/to/related1.cbl`, `path/to/related2.cbl`
- **Screens**: `path/to/screen.bms` (if applicable)

## Business Context

[2-3 paragraphs describing the business purpose and context of this use case]

## Preconditions

- [State or condition that must exist before this use case can execute]
- [Another precondition]

## Success Scenario (Main Flow)

1. **Actor Action**: [What the actor does]
   - **System Response**: [How the system responds]

2. **Actor Action**: [Next action]
   - **System Response**: [Response]

3. [Continue with numbered steps]

## Alternative Flows

### A1: [Alternative scenario name]

**Trigger**: [When this alternative occurs]

1. [Alternative step 1]
2. [Alternative step 2]
3. [Return to main flow at step X or End]

### A2: [Another alternative]

[Similar structure]

## Exception Flows

### E1: [Error condition]

**Trigger**: [What causes this error]

1. System displays error: "[Error message]"
2. [Error handling steps]
3. [Resolution or termination]

## Postconditions

**Success Postconditions**:
- [State of system after successful execution]
- [Data that has been created/modified]

**Failure Postconditions**:
- [State if use case fails]

## Data Entities Involved

| Entity | COBOL File | Operation | Fields |
|--------|-----------|-----------|--------|
| Account | ACCTDAT | Read/Write | ACCT-ID, ACCT-BAL, ... |
| Customer | CUSTDAT | Read | CUST-ID, CUST-NAME, ... |

## Business Rules

1. **BR-{id}**: [Rule description]
   - **COBOL Reference**: [Line numbers or section name]
   
2. **BR-{id}**: [Another rule]

## Non-Functional Requirements

- **Performance**: [Response time requirement]
- **Security**: [Authentication/authorization requirements]
- **Availability**: [Uptime requirement]
- **Scalability**: [Load requirements]

## Acceptance Criteria

- [ ] **AC1**: [Testable business outcome - specific and measurable]
- [ ] **AC2**: [Another criterion]
- [ ] **AC3**: [Another criterion]
- [ ] **AC4**: [Performance criterion if applicable]
- [ ] **AC5**: [Security criterion if applicable]

## Related Use Cases

- **Depends On**: [UC-xxx: Title] - [Reason for dependency]
- **Related To**: [UC-yyy: Title] - [Nature of relationship]
- **Triggers**: [UC-zzz: Title] - [How it triggers]

## Modernization Notes

### Current COBOL Implementation
[Brief description of how this is currently implemented in COBOL]

### Modernization Considerations
- [Consideration 1: e.g., "Consider async processing for batch operations"]
- [Consideration 2: e.g., "Potential for microservice boundary here"]
- [Consideration 3]

### Target Architecture Suggestions
- [Suggestion for modern implementation]
- [Another suggestion]

## Open Questions

- [ ] **Q1**: [Question that needs clarification]
- [ ] **Q2**: [Another question]

## Change Log

| Date | Change | Author |
|------|--------|--------|
| YYYY-MM-DD | Initial version | Architecture Analyst |

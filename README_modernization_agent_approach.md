# CardDemo Modernization Agent System

This directory contains specialized AI agent configurations for the CardDemo COBOL modernization project. These agents work together to analyze legacy COBOL code and guide the development of a modern .NET application.

## Agent Overview

The modernization workflow uses five specialized agents, each with distinct responsibilities:

```
┌─────────────────────────────────────────────────────────────────┐
│                   COBOL Legacy System                            │
│              (CardDemo Mainframe Application)                    │
└───────────────────────────┬─────────────────────────────────────┘
                            │
                            ▼
        ┌───────────────────────────────────────┐
        │   Architecture Analyst Agent          │
        │   • Use cases                         │
        │   • High-level modules                │
        │   • Data flows                        │
        │   • Business acceptance criteria      │
        └───────────────┬───────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────┐
        │   Detailed Analyst Agent              │
        │   • Concrete scenarios                │
        │   • Data models                       │
        │   • Detailed flows                    │
        │   • Technical test criteria           │
        └───────────────┬───────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────┐
        │   Architect Agent                     │
        │   • Target architecture               │
        │   • Technology stack                  │
        │   • Code structure                    │
        │   • Design patterns                   │
        └───────────────┬───────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────┐
        │   Developer Agent                     │
        │   • .NET implementation               │
        │   • Unit tests                        │
        │   • Clean architecture                │
        │   • SOLID principles                  │
        └───────────────┬───────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────┐
        │   Test Manager Agent                  │
        │   • Test strategy                     │
        │   • Test plans                        │
        │   • Quality gates                     │
        │   • Metrics & reporting               │
        └───────────────────────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────┐
        │      Modern .NET Application          │
        │   (CardDemo Cloud-Native System)      │
        └───────────────────────────────────────┘
```

## Agents

### 1. Architecture Analyst Agent
**File**: `architecture-analyst.md`

**Purpose**: Performs high-level analysis of the COBOL codebase to identify modernization opportunities.

**Responsibilities**:
- Identify business use cases from COBOL programs
- Define high-level functional modules
- Map data flows between components
- Generate business-level acceptance criteria

**Output**: Structured markdown documentation with:
- Use case descriptions
- High-level architecture diagrams
- Data flow diagrams
- Modernization opportunities

**When to Use**: Start of modernization project, when you need to understand the big picture and business capabilities.

### 2. Detailed Analyst Agent
**File**: `detailed-analyst.md`

**Purpose**: Performs deep technical analysis to create implementation-ready specifications.

**Responsibilities**:
- Break down use cases into detailed scenarios
- Document complete data models from COBOL copybooks
- Trace detailed program flows with line numbers
- Generate technical test criteria

**Output**: Structured markdown documentation with:
- Detailed use case specifications
- Complete data models with field mappings
- Step-by-step flow documentation
- Test scenarios with specific data

**When to Use**: After architectural analysis, when developers need detailed specifications to implement features.

### 3. Architect Agent
**File**: `architect.md`

**Purpose**: Defines and guards the target architecture for the modernized application.

**Responsibilities**:
- Design cloud-native architecture
- Select appropriate .NET technologies
- Define solution and code structure
- Establish architectural patterns and best practices
- Review implementations for compliance

**Output**: Structured markdown documentation with:
- Architecture overview and diagrams
- Technology stack specifications
- Solution structure templates
- Architecture decision records (ADRs)
- Development guidelines

**When to Use**: After analysis, before development; continuously for architecture reviews and governance.

### 4. Developer Agent
**File**: `developer.md`

**Purpose**: Translates requirements into production-quality .NET code.

**Responsibilities**:
- Implement features based on detailed specifications
- Write comprehensive unit tests (TDD approach)
- Follow Clean Architecture and SOLID principles
- Document code with XML comments
- Refactor for quality and maintainability

**Output**: Production-ready code including:
- Domain entities and value objects
- Application layer (CQRS commands/queries)
- Infrastructure implementations
- Comprehensive unit tests
- Integration tests

**When to Use**: During feature implementation; this agent produces actual code, unlike the analysis agents.

### 5. Test Manager Agent
**File**: `test-manager.md`

**Purpose**: Defines test strategy, guidelines, and comprehensive test plans.

**Responsibilities**:
- Create overall test strategy
- Establish testing standards and guidelines
- Develop detailed test plans
- Define quality gates and metrics
- Coordinate UAT and sign-offs

**Output**: Structured markdown documentation with:
- Test strategy documents
- Test guidelines and best practices
- Detailed test plans
- Quality metrics dashboards
- Test case templates

**When to Use**: Beginning of project for strategy; continuously for test planning and quality monitoring.

## Usage Workflow

### Phase 1: Analysis (No Code)
```
1. Architecture Analyst → High-level understanding
   Input: COBOL codebase
   Output: Use cases, modules, data flows

2. Detailed Analyst → Implementation specifications
   Input: Architecture analysis + COBOL programs
   Output: Detailed specs, data models, test criteria
```

### Phase 2: Architecture & Planning (No Code)
```
3. Architect → Target design
   Input: Analysis outputs
   Output: Architecture, tech stack, solution structure

4. Test Manager → Quality strategy
   Input: Analysis + architecture
   Output: Test strategy, test plans, quality gates
```

### Phase 3: Implementation (Code Generation)
```
5. Developer → Feature implementation
   Input: Detailed specs + architecture guidelines
   Output: .NET code + unit tests

6. Test Manager → Test execution
   Input: Implemented features
   Output: Test results, quality reports
```

## Agent Interaction Example

### Scenario: Modernize Transaction Posting (CBTRN02C)

**Step 1**: Architecture Analyst analyzes `cbl/CBTRN02C.cbl`
```markdown
Output:
## Use Case: Process Daily Transactions
**Actor**: Batch Processing System
**Goal**: Post daily transactions to customer accounts
**Acceptance Criteria**:
- AC1: All valid transactions posted to accounts
- AC2: Invalid transactions logged with error codes
- AC3: Account balances updated correctly
```

**Step 2**: Detailed Analyst provides specifics
```markdown
Output:
### UC-001: Post Valid Transaction
**Steps**:
1. Read transaction from TRANFILE
2. Validate account exists in ACCTDAT
3. Check credit limit not exceeded
4. Update account balance
5. Write audit record

**Test Criteria**:
- TC1: Given account balance $1000, transaction $100 → new balance $1100
- TC2: Given credit limit $5000, balance $4900, transaction $200 → error "Credit limit exceeded"
```

**Step 3**: Architect defines implementation
```markdown
Output:
## Service: Transaction Processing Service
**Pattern**: CQRS + Event-Driven
**Components**:
- PostTransactionCommand + Handler
- TransactionPostedEvent
- Account aggregate (domain)
- IAccountRepository (infrastructure)

**Technology**: .NET 8, MediatR, EF Core, Azure Service Bus
```

**Step 4**: Developer implements
```csharp
Output:
// PostTransactionCommandHandler.cs
public class PostTransactionCommandHandler : IRequestHandler<PostTransactionCommand>
{
    // Implementation with full domain logic, validation, error handling
    // Includes unit tests with 85% coverage
}
```

**Step 5**: Test Manager validates
```markdown
Output:
## Test Results: Transaction Processing
- Unit Tests: 45/45 passed ✅
- Integration Tests: 12/12 passed ✅
- Code Coverage: 87% ✅
- Performance: 150ms avg (target <200ms) ✅
```

## Agent Guidelines

### For Analysis Agents (Architecture & Detailed Analysts)
- **Output**: Markdown documentation ONLY
- **No code generation**: Focus on understanding and specification
- **Be thorough**: Include all details needed for implementation
- **Reference COBOL**: Cite specific programs, copybooks, line numbers
- **Structured format**: Use consistent markdown templates

### For Architect Agent
- **Output**: Markdown documentation ONLY
- **No code generation**: Define structure, don't implement
- **Be opinionated**: Make clear technology and pattern choices
- **Explain rationale**: Always explain the "why" behind decisions
- **Guard quality**: Review developer work against architectural standards

### For Developer Agent
- **Output**: Production-quality .NET code
- **TDD approach**: Tests first, then implementation
- **Follow architecture**: Adhere to architect's guidelines
- **Quality first**: Clean code, SOLID principles, 80%+ coverage
- **Document code**: XML comments for all public APIs

### For Test Manager Agent
- **Output**: Markdown documentation ONLY
- **Be comprehensive**: Cover all test levels and types
- **Risk-based**: Focus on high-risk areas
- **Metrics-driven**: Use metrics to drive quality decisions
- **Quality gates**: Define clear entry/exit criteria

## Best Practices

### 1. Sequential Flow
Follow the agent sequence for best results:
- Start with Architecture Analyst (big picture)
- Then Detailed Analyst (specifications)
- Then Architect (design)
- Then Developer (implementation)
- Finally Test Manager (validation)

### 2. Iterative Refinement
Agents can be re-invoked to refine outputs:
- Architecture Analyst can re-analyze after new requirements
- Architect can update design based on implementation learnings
- Developer can refactor based on test results

### 3. Cross-Agent References
Agents should reference each other's outputs:
- Detailed Analyst references Architecture Analyst's use cases
- Developer references Detailed Analyst's specifications
- Test Manager references Detailed Analyst's test criteria

### 4. Quality Focus
Every agent has quality responsibilities:
- Analysts define acceptance/test criteria
- Architect defines quality attributes
- Developer implements with tests
- Test Manager validates everything

## Project Structure

```
.github/
└── agents/
    ├── README.md                    # This file
    ├── architecture-analyst.md      # High-level analysis agent
    ├── detailed-analyst.md          # Technical analysis agent
    ├── architect.md                 # Architecture definition agent
    ├── developer.md                 # Code implementation agent
    └── test-manager.md              # Test strategy & planning agent
```

## Getting Started

### For a New Feature
1. **Analyze**: Ask Architecture Analyst to identify the use case
2. **Specify**: Ask Detailed Analyst for detailed specifications
3. **Design**: Ask Architect to define the solution structure
4. **Plan Testing**: Ask Test Manager to create test plan
5. **Implement**: Ask Developer to write the code
6. **Validate**: Ask Test Manager to review quality

### Example Prompts

**For Architecture Analyst**:
> "Analyze the COBOL program CBTRN02C.cbl and identify the high-level use cases, modules involved, and data flows for transaction processing."

**For Detailed Analyst**:
> "Create detailed specifications for the transaction posting use case, including data models from CVTRA05Y.cpy, step-by-step flow through CBTRN02C.cbl, and comprehensive test criteria."

**For Architect**:
> "Design the architecture for the transaction processing service, including technology stack, solution structure, and integration with other microservices."

**For Developer**:
> "Implement the PostTransactionCommand with handler, validator, unit tests, and integration tests based on the detailed specifications."

**For Test Manager**:
> "Create a comprehensive test plan for the transaction processing feature, including test strategy, test cases, quality gates, and acceptance criteria."

## Technology Stack

### Analysis & Architecture (Markdown Output)
- Architecture Analyst
- Detailed Analyst  
- Architect
- Test Manager

### Implementation (.NET Code Output)
- Developer Agent
- **Platform**: .NET 8+
- **Language**: C# 12
- **Patterns**: Clean Architecture, CQRS, DDD
- **Testing**: xUnit, Moq, FluentAssertions
- **Cloud**: Azure (SQL, Service Bus, App Service)

## Contributing

When adding or modifying agents:
1. Follow the established markdown structure
2. Include clear responsibilities and guidelines
3. Provide output format templates
4. Add usage examples
5. Update this README

## License

Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.

Licensed under the Apache License, Version 2.0. See the LICENSE file for details.

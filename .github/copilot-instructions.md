# GitHub Copilot Instructions - CardDemo Modernization

This file contains general instructions for GitHub Copilot when working on the CardDemo COBOL-to-.NET modernization project.

## Project Overview

This is an AI-assisted modernization of the AWS CardDemo mainframe application (COBOL/CICS/VSAM) to modern .NET microservices using Clean Architecture, CQRS, and DDD patterns.

## Core Principles

1. **State-Driven Context Management**: Always read state files first to understand project status
2. **Documentation-First Approach**: Analysis and architecture work produces markdown, not code
3. **Separation of Concerns**: Each agent has specific responsibilities and outputs
4. **Progressive Loading**: Load only what's needed for the current task
5. **Traceability**: Maintain clear links between COBOL source and modern implementation

## Context Management Strategy

**ALWAYS start by reading these files** (minimal context load):

1. `docs/state/modernization-state.md` - Overall project progress and current phase
2. `docs/state/component-status.md` - Per-component status and workflow tracking

This prevents needing to scan the entire codebase to understand project status.

**Then load specific documents** based on your agent role:
- Architecture Analyst → COBOL programs
- Detailed Analyst → Architecture analysis outputs
- Architect → All analysis outputs
- Developer → Specifications and architecture docs
- Test Manager → Specifications and implementation docs

## Agent System

This project uses 5 specialized agents. Each agent has specific input/output requirements documented in their respective files:

- **Architecture Analyst** (`.github/agents/architecture-analyst.md`) - High-level COBOL analysis
- **Detailed Analyst** (`.github/agents/detailed-analyst.md`) - Deep technical specifications
- **Architect** (`.github/agents/architect.md`) - Modern architecture design
- **Developer** (`.github/agents/developer.md`) - .NET implementation
- **Test Manager** (`.github/agents/test-manager.md`) - Testing strategy and execution

**Refer to individual agent files for specific I/O specifications.**

## Documentation Hierarchy

All documentation lives under `/docs`:

```
docs/
├── state/                  # Project state tracking
├── analysis/              # COBOL analysis (architecture + detailed)
├── architecture/          # Modern system architecture
├── implementation/        # Implementation documentation
└── testing/              # Test plans and reports
```

**See `/docs/README.md` for complete hierarchy.**

## File Naming Conventions

Use consistent 3-digit IDs with kebab-case:

- `UC-001-kebab-case-name.md` - Use cases
- `SPEC-001-kebab-case-name.md` - Specifications
- `MOD-001-kebab-case-name.md` - Modules
- `ADR-001-decision-summary.md` - Architecture decisions
- `FEAT-001-feature-name.md` - Features
- `TC-0001-test-scenario.md` - Test cases (4 digits)

## Workflow Pattern

```
1. Architecture Analyst → Analyzes COBOL → Writes use cases
2. Detailed Analyst → Reads use cases → Writes specifications
3. Architect → Reads analysis → Defines architecture
4. Developer → Reads specs → Implements .NET code
5. Test Manager → Reads specs → Plans and executes tests
```

Each agent updates `component-status.md` when their phase is complete.

## Key Guidelines

### For Analysis Work (Architecture + Detailed Analysts)
- Output is **markdown only** - no code generation
- Include COBOL line number references
- Use provided templates in `/docs/analysis/`
- Link related documents (use cases ↔ specs)
- Update component status after completion

### For Architecture Work (Architect)
- Produce markdown documentation and ADRs
- Define patterns, not implementations
- Update `decision-log.md` with summaries
- Ensure consistency across architecture docs

### For Implementation Work (Developer)
- **Only agent that generates code** (C#)
- Follow Clean Architecture + CQRS + DDD
- Write tests alongside implementation (TDD)
- Document features in markdown
- Reference specification line items

### For Testing Work (Test Manager)
- Define strategy and plans in markdown
- Execute tests and report results
- Track quality metrics
- Ensure test coverage matches specifications

## State Updates

Every agent must update state files:

- **All agents**: Update `component-status.md` when completing their phase
- **Architect**: Also update `decision-log.md` with ADR summaries
- **Architecture Analyst**: Initialize components in status tracker

## Templates

Use provided templates for consistency:
- `/docs/analysis/architecture/use-cases/_TEMPLATE.md`
- `/docs/analysis/detailed/specifications/_TEMPLATE.md`
- More templates available in respective directories

## Target Technology Stack

- **.NET 8+** with C#
- **Clean Architecture** (Domain, Application, Infrastructure, Presentation)
- **CQRS** with MediatR
- **Domain-Driven Design** patterns
- **Entity Framework Core** for data access
- **xUnit** for testing
- **Docker** for containerization
- **Azure** or **AWS** for cloud deployment

## Quality Standards

- All code must have unit tests
- Follow C# coding conventions
- Use async/await patterns
- Implement proper error handling
- Include XML documentation comments
- Follow SOLID principles

## Links to Detailed Documentation

- **Full Documentation Guide**: `/docs/README.md`
- **Setup Summary**: `/docs/SETUP-SUMMARY.md`
- **Agent Configurations**: `/.github/agents/*.md`
- **State Tracking**: `/docs/state/*.md`

---

**Remember**: Check individual agent files for specific input/output requirements and detailed responsibilities.

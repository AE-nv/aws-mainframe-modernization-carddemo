# Job Analysis: DUSRSECJ

## Overview
**Source File**: `app/jcl/DUSRSECJ.jcl`
**Frequency**: Initial setup / User data refresh
**Type**: User security file creation and load

## Purpose
Creates and loads User Security VSAM file with initial user credentials for authentication. Contains admin and regular user accounts for system access.

## Processing Steps
1. **PREDEL**: Delete existing user security PS file
   - Removes: AWS.M2.CARDDEMO.USRSEC.PS
2. **STEP01**: Create user security flat file from in-stream data
   - Uses IEBGENER to copy in-stream data
   - Output: AWS.M2.CARDDEMO.USRSEC.PS (LRECL=80, FB)
3. **STEP02**: Define USRSEC VSAM KSDS
   - Key: 8 bytes at offset 0 (User ID)
   - Record: 80 bytes
   - REUSE enabled (file can be overwritten)
4. **STEP03**: Copy PS data to VSAM file
   - Uses IDCAMS REPRO
   - Source: PS file
   - Target: VSAM KSDS

## User Data (In-Stream)
**Admin Users (5)**:
- ADMIN001 - MARGARET GOLD - PASSWORDA
- ADMIN002 - RUSSELL RUSSELL - PASSWORDA
- ADMIN003 - RAYMOND WHITMORE - PASSWORDA
- ADMIN004 - EMMANUEL CASGRAIN - PASSWORDA
- ADMIN005 - GRANVILLE LACHAPELLE - PASSWORDA

**Regular Users (5)**:
- USER0001 - LAWRENCE THOMAS - PASSWORDU
- USER0002 - AJITH KUMAR - PASSWORDU
- USER0003 - LAURITZ ALME - PASSWORDU
- USER0004 - AVERARDO MAZZI - PASSWORDU
- USER0005 - LEE TING - PASSWORDU

## Record Layout (80 bytes)
- User ID: 8 bytes (e.g., "ADMIN001")
- First Name: 20 bytes
- Last Name: 20 bytes
- Password: 9 bytes ("PASSWORDA" or "PASSWORDU")
- Filler/Reserved: 23 bytes

## VSAM File Specifications
- **Type**: KSDS
- **Key**: 8 bytes at offset 0 (User ID)
- **Record**: 80 bytes (fixed)
- **REUSE**: Enabled (allows file overwrite without delete)
- **Space**: 45 tracks primary, 15 tracks secondary
- **FREESPACE**: (10,15) for efficient insert/update

## Security Concerns
- **CRITICAL**: Plaintext passwords in JCL and VSAM file
- No password encryption or hashing
- Passwords visible in JCL source and output
- Default passwords: "PASSWORDA" (admin), "PASSWORDU" (user)

## Dependencies
- No dependencies (standalone file creation)
- Must complete before CICS online access
- Referenced by: COSGN00C (authentication program)

## Modernization Notes - CRITICAL SECURITY CHANGES REQUIRED
- **IMMEDIATE**: Remove plaintext passwords
- **Authentication**: Use ASP.NET Core Identity or Azure AD B2C
- **Password Storage**: Hash with bcrypt, Argon2, or PBKDF2
  ```csharp
  var hasher = new PasswordHasher<User>();
  var hash = hasher.HashPassword(user, password);
  ```
- **User Management**: 
  - Database table with hashed passwords
  - Multi-factor authentication (MFA)
  - Password policies (complexity, expiration, history)
  - Account lockout after failed attempts
- **SQL Table Structure**:
  ```sql
  CREATE TABLE Users (
      UserId VARCHAR(8) PRIMARY KEY,
      FirstName NVARCHAR(50),
      LastName NVARCHAR(50),
      PasswordHash NVARCHAR(255),
      Role VARCHAR(20), -- 'Admin' or 'User'
      IsActive BIT,
      LastLogin DATETIME2,
      FailedLoginAttempts INT,
      AccountLockedUntil DATETIME2
  );
  ```
- **Modern Authentication**:
  - OAuth 2.0 / OpenID Connect
  - Azure Active Directory integration
  - JWT tokens for API authentication
  - Refresh tokens for session management
- **Audit Logging**: Track all authentication events
- **In-stream data** → Database seed/migration script
- **VSAM REUSE** → Database TRUNCATE or DROP/CREATE table

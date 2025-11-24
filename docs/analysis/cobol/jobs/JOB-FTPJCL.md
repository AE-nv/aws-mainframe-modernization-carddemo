# Job Analysis: FTPJCL

## Overview
**Source File**: `app/jcl/FTPJCL.JCL`
**Frequency**: On-demand / Scheduled
**Type**: File Transfer Protocol (FTP) job

## Purpose
Transfers files between mainframe and remote FTP server for data exchange, backup, or integration purposes.

## Processing Steps
1. **STEP1**: Execute FTP client program
   - Connects to remote FTP server
   - Executes FTP commands from SYSIN
   - Transfers specified files

## FTP Configuration
**Server**: 172.31.21.124 (IP address)  
**User**: carddemousr  
**Password**: ftpdemo1 (plaintext - security concern)  
**Transfer Mode**: ASCII  

## FTP Commands Executed
1. **pwd** - Print working directory
2. **dir** - List directory contents
3. **cd /ftpfolder** - Change to target directory
4. **PUT** - Upload mainframe file
   - Source: 'AWS.M2.CARDEMO.FTP.TEST' (mainframe dataset)
   - Target: welcome.txt (Unix/Linux filename)
5. **QUIT** - End FTP session

## File Transfer
- **Direction**: Mainframe → FTP Server (PUT)
- **Source**: AWS.M2.CARDEMO.FTP.TEST
- **Destination**: /ftpfolder/welcome.txt
- **Mode**: ASCII (text file transfer)

## Security Concerns
- **CRITICAL**: Plaintext password in JCL
- **CRITICAL**: Plaintext FTP (not SFTP/FTPS)
- Credentials visible in job logs
- No encryption of data in transit
- IP address hardcoded

## Use Cases
- Send reports to external systems
- Transfer data for integration
- Backup to remote servers
- Data exchange with partners
- Send files for processing by external applications

## Configuration Notes
- Comments reference alternative server: MY.FTP.SERVER.COM
- Comments reference different IP: 10.81.148.4
- Region: 2048K memory allocation
- Timeout: 20 seconds (commented in PARM)

## Bidirectional Capability
- Current: PUT (upload from mainframe)
- Comments indicate: Change PUT to GET for download
- Supports both directions

## Dependencies
- FTP program available
- Network connectivity to 172.31.21.124
- Source file must exist for PUT operations
- Target directory must exist on FTP server
- FTP server must allow carddemousr access

## Modernization Notes - CRITICAL SECURITY CHANGES
- **Replace FTP with SFTP or FTPS** (encrypted)
- **Remove plaintext credentials**
- Consider:
  - **Azure Blob Storage**: Direct integration
    ```csharp
    var blobClient = new BlobClient(connectionString, "container", "blob");
    await blobClient.UploadAsync(stream);
    ```
  - **SFTP Server**: Use SSH keys for authentication
  - **Azure Files**: SMB 3.0 with encryption
  - **Azure Data Factory**: Managed file transfer pipeline
  - **Managed File Transfer (MFT)**: Enterprise solution
- **Authentication**:
  - Use Azure Key Vault for credentials
  - Certificate-based authentication
  - Managed Identity (Azure services)
  - SSH keys (SFTP)
- **Modern file transfer patterns**:
  - **REST API**: HTTPS with OAuth/JWT
  - **Azure Logic Apps**: Connector for FTP/SFTP
  - **Azure Functions**: HTTP trigger for file upload
  - **Event Grid + Blob Storage**: Event-driven transfer
- **Security best practices**:
  - TLS 1.2+ encryption
  - Certificate pinning
  - IP whitelisting
  - Audit logging (Application Insights)
  - Credential rotation policy
- **Configuration**:
  - Store in Azure App Configuration
  - Use ARM parameters or Bicep
  - Never hardcode credentials or IPs
- **Monitoring**: Track transfer status, failures, retry logic

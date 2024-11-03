# 📚 Loan Management Service

**Loan Management Service** is a SOAP-based web service built with **C#** and **ASP.NET WCF** (Windows Communication Foundation) for managing library loans. This project demonstrates my knowledge of C# and .NET, with a specific focus on building and consuming SOAP services using WCF. 

> This project is part of my journey in backend development, focusing on service-oriented architecture and SOAP web services.

---

## 🚀 Project Overview

The Loan Management Service is a backend system tailored to handle loan management for libraries. Using SOAP-based web services, it provides a reliable interface for operations such as creating, updating, and retrieving loan records. This project can easily integrate with other library management systems that support SOAP, making it a valuable asset for managing loans in an organized, secure, and scalable way.

### Key Features

- **SOAP-based Web Service** for consistent communication and integration with other systems.
- **Service-Oriented Architecture (SOA)** for modular and scalable design.
- **CRUD operations** for loan management, enabling users to create, update, retrieve, and delete loan records.

---

## 🛠️ Technologies Used

- **C#** - Core language for developing the service.
- **ASP.NET WCF** - Framework used to create SOAP-based services, providing interoperability with various clients.
- **MySQL** - Database for storing loan and user data with reliable persistence.
- **Visual Studio** - IDE for code development, debugging, and testing.

---

## 📁 Project Structure

```
LoanManagementService/
├── LoanManagementService.sln         # Solution file
├── Services/
│   └── LoanService.svc                # Main SOAP service for loan management
├── Web.config                         # Configuration file (database connection, WCF settings)
└── README.md                          # Project documentation
```

### Key Files

- **Services/LoanService.svc**: The primary SOAP service that exposes operations for loan management.
- **Web.config**: Contains configuration settings, including database connection details and WCF-specific options for hosting the SOAP service.

---

## 🖥️ Setup & Installation

Follow these steps to set up the Loan Management Service on your local machine.

### Prerequisites

- **Visual Studio 2019** or later with the .NET development workload installed.
- **MySQL** database installed and running.

### Step 1: Clone the Repository

```bash
git clone https://github.com/Yassinekrn/LoanManagementService.git
cd LoanManagementService
```

### Step 2: Set Up the Database

1. Open MySQL Workbench (or preferred client) and create a database named `LibraryManagement`.
2. Update the `Web.config` file with your MySQL credentials:
   ```xml
   <connectionStrings>
       <add name="LibraryDbConnection"
            connectionString="Server=localhost;Database=LibraryManagement;Uid=root;Pwd=root;"
            providerName="MySql.Data.MySqlClient" />
   </connectionStrings>
   ```

### Step 3: Build and Run the Project

1. Open `LoanManagementService.sln` in Visual Studio.
2. Build the project to restore dependencies.
3. Start debugging (F5) to host the service locally.

---

## 🧩 SOAP Operations

The Loan Management Service exposes several SOAP operations for managing loan records. Each operation can be consumed by a SOAP client and provides structured XML responses that can be easily integrated into other systems. The application has been thoroughly tested using **Postman** by generating a collection directly from the WSDL file.

### Available SOAP Operations

1. **IssueLoan** - Creates a new loan record.
   ```csharp
   public bool IssueLoan(int userId, int bookId)
   ```

2. **ReturnLoan** - Updates the status of an existing loan to mark it as returned.
   ```csharp
   public bool ReturnLoan(int loanId)
   ```

3. **GetLoanDetails** - Retrieves full details of a loan based on its ID.
   ```csharp
   public Loan GetLoanDetails(int loanId)
   ```

4. **GetAllActiveLoans** - Retrieves all active loan records.
   ```csharp
   public List<Loan> GetAllActiveLoans()
   ```

### Example SOAP Requests and Responses

#### Example Request for `IssueLoan`

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <IssueLoan xmlns="http://tempuri.org/">
      <userId>100</userId>
      <bookId>100</bookId>
    </IssueLoan>
  </soap:Body>
</soap:Envelope>
```

#### Example Response for `IssueLoan`

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
    <s:Body>
        <IssueLoanResponse xmlns="http://tempuri.org/">
            <IssueLoanResult>true</IssueLoanResult>
        </IssueLoanResponse>
    </s:Body>
</s:Envelope>
```
---

## ⚙️ Error Handling

Error handling in this service is currently limited, with basic error responses embedded within the SOAP response body. When an error occurs (e.g., if a loan cannot be created or retrieved), the service returns `false`. This approach provides simple feedback, and future improvements will include structured SOAP fault handling for a more robust error management approach.

---

## 🎯 Future Improvements

- **Enhanced Error Handling**: Implement detailed SOAP Fault responses to provide clearer error messages to clients.
- **Unit Testing**: Develop unit tests for each SOAP operation to ensure reliability and correctness.
- **Documentation with WSDL**: Provide a well-documented WSDL file to fully describe available operations, making it easier for clients to integrate with the service.

---

## 👤 Author

- **Yassine Krichen** - Aspiring Software Engineer with a focus on backend development and .NET applications.
- **LinkedIn**: [Yassine Krichen's LinkedIn](https://www.linkedin.com/in/krichenyassine/)
- **GitHub**: [Yassine Krichen's GitHub](https://github.com/Yassinekrn)

---


## 🙏 Acknowledgments

Special thanks to **Mrs. Tayechi** for her guidance and feedback throughout this project.

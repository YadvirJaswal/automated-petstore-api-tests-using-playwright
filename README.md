# Swagger Petstore API Testing

## 📌 Project Overview
This project tests the **Swagger Petstore API** using **Playwright, C#, and xUnit**. It includes automated test cases for the following models:

- **Pet Model**: Adding, retrieving, updating, and deleting pets.
- **User Model**: User creation, login, logout, and update operations.
- **Store Model**: Placing orders, retrieving orders, and managing inventory.

## 🚀 Technologies Used
- **C#** (Programming Language)
- **Playwright** (API Testing Framework)
- **xUnit** (Test Framework)
- **Newtonsoft.Json** (JSON Parsing)

## 🛠 Project Setup
### **1. Clone the Repository**
```sh
git clone https://github.com/YadvirJaswal/automated-petstore-api-tests-using-playwright
cd automated-petstore-api-tests-using-playwright
```

### **2. Install Dependencies**
```sh
dotnet add package Microsoft.Playwright
dotnet add package xunit
dotnet add package Newtonsoft.Json
```

### **3. Build the Project**
```sh
dotnet build
```

## 📜 Test Execution
### **Run All Tests**
```sh
dotnet test
```

## 🔍 API Test Cases
### **Pet Model**
- `POST /pet` - Add a new pet.
- `GET /pet/{petId}` - Retrieve pet details.
- `PUT /pet` - Update pet details.
- `DELETE /pet/{petId}` - Remove a pet.

### **User Model**
- `POST /user` - Create a user.
- `GET /user/login` - Log in a user.
- `GET /user/logout` - Log out a user.
- `PUT /user/{username}` - Update user details.

### **Store Model**
- `POST /store/order` - Place an order.
- `GET /store/order/{orderId}` - Retrieve an order.
- `DELETE /store/order/{orderId}` - Cancel an order.
- `GET /store/inventory` - Get inventory details.

## 🛡 Error Handling
- **500 Internal Server Error**: Logged with full response details.
- **404 Not Found**: Verified for deleted or invalid IDs.
- **Validation Errors**: Ensures request payload matches API requirements.

## 📝 Contribution
Feel free to fork this repository, make enhancements, and submit pull requests.



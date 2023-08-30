# Authentication and Authorization Project

This project aims to provide user authentication and authorization using .NET Core 6.0 and React JS.

## Features

- Secure user authentication
- Role-based authorization
- User login with token-based authentication
- React-based user interface

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- Visual Studio 2022 (for .Net 6.0 and higher support) or Visual Studio Code (optional)

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/Sadman-Arif-Wamim/AuthApp.git

2. Run the AuthAPI and AuthDirectory using Debug or dotnet run and use swagger for APIs.

3. Run the authwebapplication using npm run start

4. Open your browser and access http://localhost:3000 to use the app.


## Dummy Data

- An in-memory database is used to store dummy data.
  For Admin Login:
    username: admin@user.com
    password: password1234
  For Regular Login:
    username: regular@user.com
    password: passowrd1234

## Configurations
- For token generation, A dummy Issuer, Audience and Key is created and placed in the main directory for both the API and Identifier. **Please Do Store The Actual Credentials in the Code and Use a Key Vault**

## Libraries
- MUI Library is used for the front end. Use command in terminal 'npm install @mui/material @emotion/react @emotion/styled'. For more explanation, please visit: https://mui.com/material-ui/getting-started/
- Microsoft.AspNetCore.Authentication.JwtBearer: For JWT-based authentication.
- System.IdentityModel.Tokens.Jwt: For working with JWT tokens.
- Microsoft.AspNetCore.Identity: For managing user identities.

## Road Map
- Add more pages in the UI.
- Add more APIs for validation.
- Implement Cookies and refreshing Tokens.
- Add Auth 2.0  


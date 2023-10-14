# Authenticating Web API Using ASP .Net Identity and JSON Web Tokens (JWT)

In this project, we will see how to Authenticate Web API applications using Asp .Net Core Identity and validate the incoming requests using JSON Web Token (JWT).


## Identity Context

### Add Packages
```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools

Microsoft.AspNetCore.Authentication.JwtBearer
```

### Add Migrations
```
Add-Migration InitialCreate
Update-Database
```


## Register

- wrong password :

<img src="/pictures/register.png" title="register"  width="800">

- good password :

<img src="/pictures/register2.png" title="register"  width="800">
<img src="/pictures/register3.png" title="register"  width="800">


using Microsoft.EntityFrameworkCore;
using AuthProject.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthProject.Controllers;
using System;

var builder = WebApplication.CreateBuilder(args);

var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = "https://localhost:7225/",
             ValidAudience = "AuthProject",
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
         };
     });

builder.Services.AddDbContext<DBContext>
    (opt => opt.UseInMemoryDatabase("UserDB"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Text;
using Fit.Common;
using Fit.Domain.Entities;
using Fit.Infrastructure.Persistence.Context;
using Fit.Infrastructure.Persistence.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetRequiredSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(auth =>
{
    auth.RequireHttpsMetadata = true;
    auth.SaveToken = true;
    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings!.Jwt.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

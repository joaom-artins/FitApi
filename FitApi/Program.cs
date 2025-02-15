using System.Text;
using Fit.API.Filters;
using System.Text.Json.Serialization;
using System.Text.Json;
using Fit.Common;
using Fit.Domain.Entities;
using Fit.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Fit.Infrastructure.Persistence.Utils;
using Fit.Application.Utils;
using Fit.API.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Fit.Common.Utils;
using Fit.Application.DTOs.Requests.Login;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetRequiredSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCommons();

builder.Services.AddHttpContextAccessor();

builder.Services
.AddControllers(options =>
{
    options.ModelValidatorProviders.Clear();
    options.Filters.Add(new ConsumesAttribute("application/json"));
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add<NotificationFilter>();
    options.Filters.Add<ValidationFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequest>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddIdentity<UserModel, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

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

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
    dbContext!.Database.Migrate();
}

app.Run();

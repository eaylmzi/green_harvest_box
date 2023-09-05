
using greenharvestbox.Data.Repositories.LoginRepository;
using greenharvestbox.Data.Repositories.UserRepository;
using greenharvestbox.Data.Services.ConfigurationServices;
using greenharvestbox.Logic.Services.Cipher;
using greenharvestbox.Logic.Services.Jwt;
using greenharvestbox.Logic.Services.UserServices.LoginService;
using greenharvestbox.Logic.Services.UtilityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;





var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Create dependencies
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUtilityService, UtilityService>();


builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value ?? throw new ArgumentNullException())),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"bearer{token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();

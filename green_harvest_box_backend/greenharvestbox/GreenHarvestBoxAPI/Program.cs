
using greenharvestbox.Data.Repositories.LoginRepository;
using greenharvestbox.Data.Repositories.UserRepository;
using greenharvestbox.Logic.Services.UserServices.LoginService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Create dependencies

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

global using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;

using EMPIRIAN.Database;
using EMPIRIAN.Modules.Signalling;
using EMPIRIAN.Modules.Users;

var builder = WebApplication.CreateBuilder(args);

// Environment
DotNetEnv.Env.Load();

// Modules
builder.AddDatabaseModule();
builder.AddUsersModule();
builder.AddSignallingModule();

// Controllers
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.SetIsOriginAllowed(allowed => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.AllowTrailingCommas = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App
var app = builder.Build();
app.MigrateDatabase();

// Docs
app.UseSwagger();
app.UseSwaggerUI();

// Auth
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints, Controllers and Routing
app.UseRouting();
app.MapSignalRHubs();
app.MapControllers();

// Run
app.Run();
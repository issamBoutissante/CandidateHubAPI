using CandidateHubAPI.Data;
using CandidateHubAPI.Mappings;
using CandidateHubAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Add memory cache

// Configure DbContext with SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CandidateDbContext>(options =>
    options.UseSqlite(connectionString));

// Register the CandidateService
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CandidateHubAPI", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CandidateHubAPI v1"));
}

app.UseHttpsRedirection();

// Add Serilog request logging
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

// Ensure the database is created and apply any pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

using CandidateHubAPI.Data;
using CandidateHubAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure DbContext with SQLite
var connectionString = "Data Source=candidatehub.db";
builder.Services.AddDbContext<CandidateDbContext>(options =>
    options.UseSqlite(connectionString));

// Register the CandidateService
builder.Services.AddScoped<ICandidateService, CandidateService>();

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

// Ensure the database is created and apply any pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

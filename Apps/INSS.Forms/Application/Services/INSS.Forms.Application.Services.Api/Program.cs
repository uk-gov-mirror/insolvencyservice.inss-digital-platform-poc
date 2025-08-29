using INSS.Forms.Application.Services.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CosmosClient as a singleton
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var cosmosConnectionString = configuration.GetConnectionString("CosmosDb");

    if (string.IsNullOrEmpty(cosmosConnectionString))
    {
        throw new InvalidOperationException("Cosmos DB connection string is not configured.  Update appsettings.json or set the environment variable (ConnectionStrings__CosmosDb)");
    }

    var cosmosClientOptions = new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            IgnoreNullValues = true,
        }
    };

    return new CosmosClient(cosmosConnectionString, cosmosClientOptions);
});


// Register DbContext for Entity Framework
builder.Services.AddDbContext<ConfigurationDbContext>(options =>
{
    var configuration = builder.Configuration;
    var sqlConnectionString = configuration.GetConnectionString("SqlServer");

    if(string.IsNullOrEmpty(sqlConnectionString))
    {
        throw new InvalidOperationException("SQL Server connection string is not configured.  Update appsettings.json or set the environment variable (ConnectionStrings__SqlServer)");
    }

    options.UseSqlServer(sqlConnectionString);
});

builder.Services.AddScoped<IFormRepository, FormRepository>();

builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
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

app.MapControllers();

app.Run();

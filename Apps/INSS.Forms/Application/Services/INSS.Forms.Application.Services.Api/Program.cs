using INSS.Forms.Application.Services.Data;
using Microsoft.Azure.Cosmos;


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

// Register IFormRepository with injected Logger and CosmosClient
builder.Services.AddScoped<IFormRepository, FormRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<FormRepository>>();
    var cosmosClient = provider.GetRequiredService<CosmosClient>();
    return new FormRepository(logger, cosmosClient);
});


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

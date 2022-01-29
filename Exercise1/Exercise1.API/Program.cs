using Exercise1.DataLayer.Azure.StorageAccount;
using Exercise1.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IUserRepository userRepository = new UserRepository(configuration.GetValue<string>("ConnectionStrings:AzureSqlServer"));
builder.Services.AddSingleton(userRepository);

BlobContainerSettings blobContainerSettings = new();
configuration.Bind(nameof(BlobContainerSettings), blobContainerSettings);
IBlobContainerService blobContainerService = new BlobContainerService(blobContainerSettings);
builder.Services.AddSingleton(blobContainerService);

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

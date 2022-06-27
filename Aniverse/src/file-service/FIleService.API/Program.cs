using FileService.API.Services;
using FileService.API.Services.Abstractions.Storage;
using FileService.API.Services.Implementations.Storage;
using FileService.API.Services.Implementations.Storage.Azure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers();
builder.Services.AddScoped<IStorageService, StorageService>();

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.Run();

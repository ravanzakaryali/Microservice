using Notfication.API.DataAccessLayer.Entities;
using Notfication.API.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbService>();
var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
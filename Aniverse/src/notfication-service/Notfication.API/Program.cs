using Microsoft.IdentityModel.Tokens;
using Notfication.API.DataAccessLayer.Entities;
using Notfication.API.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
string authenticationProviderKey = "TestKey";
SymmetricSecurityKey signInKey = new(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Security"]));
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = authenticationProviderKey;
})
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signInKey,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            RequireExpirationTime = true
        };
    });
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
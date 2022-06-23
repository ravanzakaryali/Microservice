using Microsoft.EntityFrameworkCore;
using PostService.Persistence.DataContext;
using PostService.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

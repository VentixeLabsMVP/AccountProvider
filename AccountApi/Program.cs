using AccountApi.Data;
using AccountApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<AccountService>();
builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(x => {

//    x.User.RequireUniqueEmail = true;
//    x.Password.RequiredLength = 8;

//}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddCors(x =>
{
    x.AddPolicy("AllowAll", x =>
    {
        x.AllowAnyOrigin();
        x.AllowAnyHeader();
        x.AllowAnyMethod();
    });
});



var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

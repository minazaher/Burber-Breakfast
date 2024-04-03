using BuberBreakfast.Persistence;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

{
    // Add services for Swagger first
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    });

    // Other service registrations (controllers, DbContext, etc.)
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
    builder.Services.AddDbContext<mDbContext>(options =>
        options.UseSqlite("Data Source=BuberBreakfast.db"));
    builder.Services.AddAuthorization();
    builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        .AddEntityFrameworkStores<mDbContext>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Use Swagger middleware before other request handlers
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

{
    // Other app configuration (error handling, HTTPS redirection, etc.)
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    // Map controllers and Identity endpoints after Swagger middleware
    app.MapControllers();
    app.MapIdentityApi<IdentityUser>();

    app.Run();
}

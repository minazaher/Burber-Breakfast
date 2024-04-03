using BuberBreakfast.Persistence;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
    builder.Services.AddDbContext<mDbContext>(options => 
        options.UseSqlite("Data Source=BuberBreakfast.db"));
    builder.Services.AddAuthorization();
    builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        .AddEntityFrameworkStores<mDbContext>();
}

var app = builder.Build();

{
    app.MapIdentityApi<IdentityUser>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

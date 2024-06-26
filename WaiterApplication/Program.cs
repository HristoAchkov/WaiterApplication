using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WaiterApplication.Infrastructure.Data;
using WaiterApplication.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddApplicationServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Home/Error/500");
    app.UseExceptionHandler("/Home/Error/404");
    app.UseExceptionHandler("/Home/Error/400");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseExceptionHandler("/Home/Error/404");
    app.UseExceptionHandler("/Home/Error/400");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "Dish Details",
    pattern: "/Menu/Details/{id}/{information}",
    defaults: new { Controller = "Menu", Action = "Details" }
    );
    endpoints.MapControllerRoute(
        name: "All Dishes",
        "/Table/All",
        defaults: new { Controller = "Table", Action = "All" }
        );
    app.MapDefaultControllerRoute();
    app.MapRazorPages();
});

await app.CreateAdminRoleAsync();

app.Run();

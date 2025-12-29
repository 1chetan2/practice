using firstprogram.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IStudentService, StudentService>();


var app = builder.Build();

// Logging 
//builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// global exception handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        if (exceptionFeature != null)
        {
            logger.LogError(
                exceptionFeature.Error,
                "Unhandled exception occurred at path: {Path}",
                exceptionFeature.Path
            );
        }

        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    });
});


// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();  // Session must be enabled here
app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Register}/{action=Login}/{id?}"
//);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FeesDetail}/{action=Create}/{id?}"
);

//app.MapControllerRoute(
//    name: "ProductsRoute",
//    pattern: "our-products/{action=Details}/{Id?}",
//    defaults:new{Controller="Products"}
//);
app.Run();



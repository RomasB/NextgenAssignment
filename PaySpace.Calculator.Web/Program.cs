using PaySpace.Calculator.Web.Services;
using PaySpace.Calculator.Web.Services.AppSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CalculatorSettings>(builder.Configuration.GetSection("CalculatorSettings"));

builder.Services.AddControllersWithViews();
builder.Services.AddCalculatorHttpServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();
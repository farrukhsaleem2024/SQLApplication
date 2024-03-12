
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using testApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


///Allows to connect with Azure App configuration
var connectionString = "Endpoint=https://app-config-con.azconfig.io;Id=gmiL;Secret=EyXcWHsNgfo0Y9q03VVPJFQYukJSypowYGJrIPvgPxY=";


builder.Host.ConfigureAppConfiguration(builder =>
{
    //builder.AddAzureAppConfiguration(connectionString);

    // enabling feature flags

    builder.AddAzureAppConfiguration(Options =>
    Options.Connect(connectionString).UseFeatureFlags());
});

/// Allows to connect with Azure App configuration
/// 



builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

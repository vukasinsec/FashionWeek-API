using Blazored.LocalStorage;
using KulinariumUI.Client;
using KulinariumUI.Client.Services;
using KulinariumUI.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(provider =>
{
    var api = new UriBuilder
    {
        Scheme = Uri.UriSchemeHttps,
        Host = "localhost",
        Port = 7122
    }.Uri;

    return new HttpClient
    {
        BaseAddress = api,
        Timeout = TimeSpan.FromSeconds(60)
    };
});
builder.Services.AddRazorPages();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices(options => { options.PopoverOptions.ThrowOnDuplicateProvider = false; });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();

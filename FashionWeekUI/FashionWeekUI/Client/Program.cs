using Blazored.LocalStorage;
using KulinariumUI.Client;
using KulinariumUI.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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
        Timeout = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddMudServices(options =>
{
    options.PopoverOptions.ThrowOnDuplicateProvider = false;
    options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    options.SnackbarConfiguration.PreventDuplicates = true;
    options.SnackbarConfiguration.NewestOnTop = true;
    options.SnackbarConfiguration.ShowCloseIcon = true;
    options.SnackbarConfiguration.VisibleStateDuration = 5000;
    options.SnackbarConfiguration.HideTransitionDuration = 500;
    options.SnackbarConfiguration.ShowTransitionDuration = 500;
    options.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMenuService, MenuService>();

//// TODO: Add Serilog

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
await builder.Build().RunAsync();

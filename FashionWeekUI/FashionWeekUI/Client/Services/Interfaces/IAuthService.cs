namespace KulinariumUI.Client.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(string username, string password);

    Task<string> LogIn(string username, string password);

    Task<string> LogOut();
}

// TODO: Create Separate Project For Shared Models (To Use In Stuff Like The Above) And Import NuGet Packages

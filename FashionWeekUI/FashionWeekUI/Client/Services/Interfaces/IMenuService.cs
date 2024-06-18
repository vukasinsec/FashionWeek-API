namespace KulinariumUI.Client.Services.Interfaces
{
    public interface IMenuService
    {
        event Action RefreshRequested;
        Task CallRequestRefresh();
    }
}

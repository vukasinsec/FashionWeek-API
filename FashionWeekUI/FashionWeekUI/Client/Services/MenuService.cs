using KulinariumUI.Client.Services.Interfaces;

namespace KulinariumUI.Client.Services
{
    public class MenuService : IMenuService
    {
        public event Action? RefreshRequested;
        public async Task CallRequestRefresh()
        {
            if (RefreshRequested != null)
            {
                await Task.Run(RefreshRequested.Invoke);
            }
        }
    }
}

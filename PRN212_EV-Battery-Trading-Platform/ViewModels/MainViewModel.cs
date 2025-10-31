using EVBattery.Core.Models;

namespace EVBattery.UI.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Account CurrentUser { get; }
        public string AccessToken { get; }

        public MainViewModel(Account account, string token)
        {
            CurrentUser = account;
            AccessToken = token;
        }

        public string WelcomeMessage => $"Xin chào, {CurrentUser?.FullName ?? "Người dùng"}!";
    }
}

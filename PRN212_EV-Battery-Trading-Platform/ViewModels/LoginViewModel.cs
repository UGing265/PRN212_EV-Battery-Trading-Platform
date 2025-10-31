using EVBattery.Core.Helpers;
using EVBattery.Core.Models.Auth;
using EVBattery.UI.WPF.Commands;
using EVBattery.UI.WPF.Windows;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EVBattery.UI.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _identifier = string.Empty;
        public string Identifier
        {
            get => _identifier;
            set => SetProperty(ref _identifier, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await LoginAsync(), () => true);
        }

        private async Task LoginAsync()
        {
            // 🧩 Validation cơ bản
            if (string.IsNullOrWhiteSpace(Identifier))
            {
                ErrorMessage = "Vui lòng nhập Email hoặc Số điện thoại.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập mật khẩu.";
                return;
            }

            ErrorMessage = string.Empty;

            var dto = new LoginDto
            {
                Identifier = Identifier,
                Password = Password
            };

            var response = await ApiHelper.PostAsync<LoginResponse>("auth/login", dto);

            if (response == null || string.IsNullOrEmpty(response.AccessToken))
            {
                ErrorMessage = "Sai thông tin đăng nhập!";
                return;
            }

            // ✅ Hiển thị tên người dùng
            string userName = response.Account?.FullName ?? "Người dùng";
            MessageBox.Show($"🎉 Xin chào, {userName}!", "Đăng nhập thành công",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // ✅ Mở MainWindow, truyền dữ liệu người dùng
            var mainWindow = new MainWindow
            {
                DataContext = new MainViewModel(response.Account, response.AccessToken)
            };
            mainWindow.Show();

            // Đóng cửa sổ Login
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}

using DAL.Entities;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PRN_Group7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public User? CurrentUser { get; set; }
        public int? CurrentUserId { get; set; }
    }

}

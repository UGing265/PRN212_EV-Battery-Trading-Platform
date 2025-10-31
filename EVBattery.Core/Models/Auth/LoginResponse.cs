using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVBattery.Core.Models.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public Account Account { get; set; }
    }
}

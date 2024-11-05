using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Services
{
    public class AuthService
    {
        private bool _isLoggedIn = false;

        
        public bool IsLoggedIn()
        {
            return _isLoggedIn;
        }

     
        public void Login()
        {
            _isLoggedIn = true;
        }

      
        public void Logout()
        {
            _isLoggedIn = false;
        }
    }
}

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
            var accessToken = Preferences.Get("accesstoken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
               _isLoggedIn = false ;
            }
            else
            {
                _isLoggedIn = true ;
            }
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

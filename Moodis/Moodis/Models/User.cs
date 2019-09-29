using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Feature.Login
{
    [Serializable]
    class User
    {
        private String username;
        private String password;

        public User(String username, String password)
        {
            this.username = username;
            this.password = password;
        }

        public String getUsername()
        {
            return this.username;
        }

        public String getPassword()
        {
            return this.password;
        }
    }
}

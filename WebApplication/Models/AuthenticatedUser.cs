using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.DAL;
using Generic.Obfuscation.TripleDES;

namespace WebApplication.Models
{
    [Serializable]
    public class AuthenticatedUserInfo
    {
        private AuthenticateUser_Result _seed;

        public AuthenticatedUserInfo(AuthenticateUser_Result authenticateUser_Result)
        {
            _seed = authenticateUser_Result; 
        }

        public string FullName {
            get {
                return string.Format("{0}{1}{2}", _seed?.FirstName, " ", _seed?.Surname).Trim();
            }
        }

        public string UserId {
            get {
                return DataSecurityTripleDES.GetEncryptedText(_seed?.Id);
            }
        }
    }
}
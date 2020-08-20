using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryptions
{
  public  class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) //gönderilen keyi utf8'e çevir
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}

using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extentios
{
   public static class ClaimExtentions
    {
        public static void AddEmail(this ICollection<Claim> claims,string email)//method neyi extend edicek ve parametre yani email extend edicez
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));//jwtRegisteredClaimNames içindeki emaile gelen email eklenir
        }


        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddNameNameIdentifier(this ICollection<Claim> claims, string nameIdentityFier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentityFier));
        }
        public static void AddRoler(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Security.Principal;

namespace AnReshWebApp.JWT
{
    public class JsonWebTokenService
    {
		private readonly SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("secretJWT")));
		private readonly string myIssuer = "http://localhost:44305";
		private readonly string myAudience = "http://localhost:8080";

		public string GenerateToken(int id, string userLogin)
		{
			var login = userLogin.Trim();
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
            {
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, id.ToString()),
					new Claim(ClaimTypes.Name, login)

				}),
				Expires = DateTime.UtcNow.AddMinutes(1),
				Issuer = myIssuer,
				Audience = myAudience,
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
		}

		public HttpStatusCodeResult ValidateToken(string authToken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var validationParameters = GetValidationParameters();
			try
			{
				tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);
			}
			catch (Exception)
			{
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
			}
			return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
		}

		private TokenValidationParameters GetValidationParameters()
		{
			return new TokenValidationParameters()
			{
				ValidateLifetime = true, 
				ValidateAudience = true,
				ValidateIssuer = true,  
				ValidIssuer = this.myIssuer,
				ValidAudience = this.myAudience,
				IssuerSigningKey = mySecurityKey
			};
		}
	}
}
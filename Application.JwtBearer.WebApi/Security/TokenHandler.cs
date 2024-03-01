using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Application.JwtBearer.WebApi.Security
{

    public static class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration)
        {
            Token token = new ();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Token:SecurityKey"))); //simetrik güvenlik anahtarı oluşturdum

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //güvenlik anahtarı algoritmayla kullanılıyor

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration.GetValue<int>("Token:Expiration")));

            JwtSecurityToken securityToken = new( //jwt temel bilgiler oluşturdum

                issuer: configuration.GetValue<string>("Token:Issuer"),
                audience: configuration.GetValue<string>("Token:Audience"),
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials

                );

            JwtSecurityTokenHandler tokenHandler = new();

               token.AccessToken = tokenHandler.WriteToken(securityToken);


            byte[] numbers= new byte[32];   
            using RandomNumberGenerator random= RandomNumberGenerator.Create(); //random string dizisi base64l4 çevrilip refresh tokena atanıyor
            random.GetBytes(numbers);

            token.RefreshToken = Convert.ToBase64String(numbers);



            return token;
        }
    }

}
  


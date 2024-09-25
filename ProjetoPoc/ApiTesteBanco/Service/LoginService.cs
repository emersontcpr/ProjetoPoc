using ApiTesteBanco.Dto;
using ApiTesteBanco.Service.Inerfaces;
using ApiTesteBanco.Settings;
using Azure.Core;
using DataBaseEntity.Repository;
using DataBaseEntity.Repository.Interface;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTesteBanco.Service
{
    public class LoginService : ILoginService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenRepository _tokenRepository;
        public LoginService(IOptions<JwtSettings> jwtSettings, ILoginRepository loginRepository, ITokenRepository tokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _loginRepository = loginRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LogarApiAsync(Login login)
        {
            try
            {
                login.Password = StringToBase64(login.Password);
                if (await _loginRepository.IsLoginValidoAsync(login.Username, login.Password))
                {
                    var token = GenerateJwtToken(login.Username);
                    var tokenInfo = new DataBaseEntity.Model.Token()
                    {
                        ValorToken = token,
                        DataCadastro = DateTime.Now,
                        DataExpiracao = DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes)
                    };
                    await _tokenRepository.AddAsync(tokenInfo);
                    await _tokenRepository.SaveChangesAsync();
                    return token;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        private string StringToBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, username),
            }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

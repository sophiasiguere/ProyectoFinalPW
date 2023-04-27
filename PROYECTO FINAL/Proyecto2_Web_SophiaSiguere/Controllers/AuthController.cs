using Proyecto2_Web_SophiaSiguere.Models;
using Proyecto2_Web_SophiaSiguere.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proyecto2_Web_SophiaSiguere.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto2_Web_SophiaSiguere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbBolsassiguereContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DbBolsassiguereContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public async Task<UserToken?> Login(UserAuth userCreds)
        {
            var user = await _context.Usuarios
                            .Where(u => u.Usuario1 == userCreds.User)
                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (Encryption.ComparePasswords(user.Contrasena, userCreds.Password))
            {
                return new UserToken
                {
                    Id = user.Id,
                    Username = user.Usuario1,
                    Token = CustomTokenJWT(user.Usuario1)
                };
            }
            return null;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<UserToken>> Register(Usuario usuarionuevo)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'MoviesdbContext.PersonalInformations'  is null.");
            }
            usuarionuevo.Contrasena = Encryption.EncryptPassword(usuarionuevo.Contrasena!);
            _context.Usuarios.Add(usuarionuevo);
            await _context.SaveChangesAsync();

            return new UserToken
            {
                Id = usuarionuevo.Id,
                Username = usuarionuevo.Usuario1,
                Token = CustomTokenJWT(usuarionuevo.Usuario1)
            };
        }

        private string CustomTokenJWT(string username)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
            );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, username)
            };
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2)
            );
            var _Token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}

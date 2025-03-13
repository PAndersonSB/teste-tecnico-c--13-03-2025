using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;

namespace SeuProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint de login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // Valide o usuário aqui. Este exemplo é bem simples. Em produção, você validaria com um banco de dados.
            if (user.Username != "admin" || user.Password != "senha") // Exemplo simples de validação
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Defina as claims para o token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin") // Exemplo de role
            };

            // Defina a chave secreta e credenciais de assinatura
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crie o token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // O token expira em 1 hora
                signingCredentials: creds
            );

            // Retorne o token gerado
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });
        }
    }

    // Classe de modelo do usuário

}

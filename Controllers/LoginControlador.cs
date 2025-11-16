using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Veterinaria.Servicio;
using Microsoft.Extensions.Configuration;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginControlador : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;
        private readonly string _claveSecreta;

        public LoginControlador(ClienteServicio clienteServicio, IConfiguration configuration)
        {
            _clienteServicio = clienteServicio;
            _claveSecreta = configuration["JwtSettings:SecretKey"];
        }

        // Ahora recibe correo + contraseña (la contraseña será la cédula)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var cliente = await _clienteServicio.AuthenticateAsync(request.Email, request.Password);
            if (cliente == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_claveSecreta);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, cliente.ClienteId.ToString()),
                    new Claim(ClaimTypes.Name, cliente.Nombre),
                    new Claim(ClaimTypes.Email, cliente.Correo)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                mensaje = "Login exitoso",
                token = tokenString,
                cliente = new { cliente.ClienteId, cliente.Nombre, cliente.Correo }
            });
        }
    }

    // DTO para la solicitud de login
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // será la cédula del cliente
    }
}
using G5.Denuncias.BE.Domain.Denuncias.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Infraestructure.Token
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(this  IConfiguration config, Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Secreto").Get<string>()!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config.GetSection("Jwt:Editor").Get<string>()!,
                audience: config.GetSection("Jwt:Audiencia").Get<string>()!,
                expires: DateTime.UtcNow.AddHours(config.GetSection("Jwt:Expira:Horas").Get<int>()!),
                claims: new[]
                {
                new Claim("id", user.Id.ToString()),
                new Claim("nombreUsuario", user.NombreUsuario)
                },
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

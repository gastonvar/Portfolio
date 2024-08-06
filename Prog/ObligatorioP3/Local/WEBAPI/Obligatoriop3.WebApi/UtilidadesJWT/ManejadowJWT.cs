using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Obligatoriop3.WebApi.UtilidadesJWT
{
    public class ManejadowJWT
    {
        public static string GenerarToken(string email, string rol)
        {
            var claveDificil = "GastonSecreto_MatiasSecreto_GastonSecreto_MatiasSecreto_GastonSecreto_MatiasSecreto";
            var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
            List<Claim> claims = [
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol)
                ];

            var credenciales = new SigningCredentials(claveDificilEncriptada, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
    }
}

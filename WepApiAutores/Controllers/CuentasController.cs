using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WepApiAutores.Dtos;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration configuration;

        /*Identity nos da un conjunto de clases para realizar toda la funcionalidad de un sistema de usuarios tipico
y eso incluye el registro de un usuario y el servicio que me permite registrar un usuario es 
UserManager<IdentityUser> al UserManager hay que pasarle una clase que identifica a un usuario de nuestra 
aplicacion y ese clase es IdentityUser*/
        public CuentasController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager , IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("Registro")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };

            var resultado = await _userManager.CreateAsync(usuario, credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                //Aqui vamos a retornar el JWT
              return  ConstruirToken(credencialesUsuario);
            }
            else
            {
               return BadRequest(resultado.Errors);
            }
        }

        //login para hacer el login tenemos que utilizar una clase llamada  SignInManager
        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            /*ya el metodo esta configurado y generara los tokens de manera corecta, pro antes recordar que nuestro
             JWT esta firmado con una llave configuration["llavejwt"]) entonces la aplicacion va a tomar el token
            y va a confirmar que halla sido firmado con la llave correcta para ello debemos ir a la clase program
            y trabajar con las configuraciones de .AddJwtBearer()*/
            var resultado = await _signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password,isPersistent:false,lockoutOnFailure:false);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            /* Aqui contruiremos un listado de claims, un claims es una informacion a cerca del usuario en la cual podemos
             confiar, es decir es una informacion que es emitida por una fuente en la cual nosotros confiamos,
            un claim es llave y valor new Claim("llave",valor), luego estos Claims se los vamos a anadir al token
            asi cada ves que el usuario nos mande un token nosotros vamos a poder leer los claims de ese token*/
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email)
            };

   /*Ahora vamos a construir el JWT con la llave secreta, aqui pondremos la llave secreta GetBytes(),
   Donde vamos a colocar esa llave secreta? la podemos colocar en un proveedor de configuracion como el 
   appsettings.Developement.json*/

            var llave =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var credentials = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);

            //Contruimos el token
            var securityToken = new JwtSecurityToken(issuer:null,audience:null,claims:claims,
                expires:expiration,signingCredentials:credentials);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration

            };

        }
    }
}

using Natillera_Parcial.Clases;
using Natillera_Parcial.Models;
using System.Linq;
using System.Web.Http;

namespace Natillera_Parcial.Controllers
{
    [RoutePrefix("api/login")]
    [AllowAnonymous]

    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Ingresar")]
        public IQueryable<LoginRespuesta> Ingresar([FromBody] Login login)
        {
            clsLogin _Login = new clsLogin();
            _Login.login = login;
            return _Login.Ingresar();
        }
    }
}
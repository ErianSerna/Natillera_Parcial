using Natillera_Parcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natillera_Parcial.Clases
{
	public class clsLogin
	{
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public DBNatilleraEntities dbNati = new DBNatilleraEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                //Se lee el usuario de la bd
                Administrador administrador = dbNati.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (administrador == null)
                {
                    //El usuario no exist, se retorna error
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Administrador no existe";
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Administrador administrador = dbNati.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (administrador == null)
                {
                    //La clave no es igual
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in dbNati.Set<Administrador>()
                       where U.Usuario == login.Usuario &&
                               U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.Usuario,
                           Autenticado = true,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }
    }
}
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
                //Creo la clase ded encripción
                clsCypher cifrar = new clsCypher();
                //Se lee el usuario de la bd
                Administrador administrador = dbNati.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (administrador == null)
                {
                    //El usuario no exist, se retorna error
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Administrador no existe";
                    return false;
                }



                ////El usuario existe, se lee el salt
                //byte[] arrBytesSalt = Convert.FromBase64String(administrador.Salt);


                ////Se envia a encriptar la clave
                //string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);


                ////Asigno la clave encriptada en el obj de login
                //login.Clave = ClaveCifrada;



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
                       //join UP in dbSuper.Set<Usuario_Perfil>()
                       //on U.id equals UP.idUsuario
                       //join P in dbSuper.Set<Perfil>()
                       //on UP.idPerfil equals P.id
                       where U.Usuario == login.Usuario &&
                               U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.Usuario,
                           Autenticado = true,
                           //Perfil = P.Nombre,
                           //PaginaInicio = P.PaginaNavegar,
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List <Usuario> Listar()
        {

            return objCapaDato.Listar();
        }
        private bool EsFormatoCorreoValido(string correo)
        {
            // Patrón de expresión regular para validar el correo xd
            string patronCorreo = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            return Regex.IsMatch(correo, patronCorreo);
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Nombre)|| string.IsNullOrWhiteSpace(obj.Nombre)||obj.Nombre.Any(char.IsDigit))
            {
                Mensaje = "El nombre del usuario no puede ser vacio ni tampoco contener números";
            }
            else if(string.IsNullOrEmpty(obj.ApellidoPaterno) || string.IsNullOrWhiteSpace(obj.ApellidoPaterno) || obj.Nombre.Any(char.IsDigit))
            {
                Mensaje = "El ApellidoPaterno del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.ApellidoMaterno) || string.IsNullOrWhiteSpace(obj.ApellidoMaterno) || obj.Nombre.Any(char.IsDigit))
            {
                Mensaje = "El ApellidoMaterno del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo) || !EsFormatoCorreoValido(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio y debe tener un formato valido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();
                

                string asunto = "Creaccion de Cuenta";
                string mensaje_correo = "<h3> cuenta fue creada Correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);
                
                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                    

                }
                else 
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
        
                
                
            }
            else
            {
                return 0;
            }

           




        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.ApellidoPaterno) || string.IsNullOrWhiteSpace(obj.ApellidoPaterno))
            {
                Mensaje = "El ApellidoPaterno del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.ApellidoMaterno) || string.IsNullOrWhiteSpace(obj.ApellidoMaterno))
            {
                Mensaje = "El ApellidoMaterno del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }else { 
                
                return false; 
            
            
            }



        }

        public bool Eliminar(int id, out string Mensaje)
        {

            return objCapaDato.Eliminar(id, out Mensaje);

        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idusuario,nuevaclave, out Mensaje);

        }


        public bool ReestablecerClave(int idusuario,string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario,CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3> Su cuenta fue reestablecida Correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);


                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else 
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false; 
            }


        }


    }
}

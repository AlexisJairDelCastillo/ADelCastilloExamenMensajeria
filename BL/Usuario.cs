using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Login(string correoElectronico)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"Login '{correoElectronico}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.CorreoElectronico = query.CorreoElectronico;
                        usuario.Contrasenia = query.Contrasenia;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (DataL.Usuario resultUsuario in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = resultUsuario.IdUsuario;
                            usuario.Nombre = resultUsuario.Nombre;
                            usuario.ApellidoPaterno = resultUsuario.ApellidoPaterno;
                            usuario.ApellidoMaterno = resultUsuario.ApellidoMaterno;
                            usuario.CorreoElectronico = resultUsuario.CorreoElectronico;
                            usuario.Contrasenia = resultUsuario.Contrasenia;
                            usuario.Foto = resultUsuario.Foto;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.CorreoElectronico}', '{usuario.Contrasenia}', '{usuario.Foto}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Te has registrado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}

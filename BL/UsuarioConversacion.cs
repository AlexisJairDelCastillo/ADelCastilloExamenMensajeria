using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioConversacion
    {
        public static ML.Result GetAll(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    var query = context.UsuarioConversacions.FromSqlRaw($"UsuarioConversacionGetAll {idUsuario}").ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (DataL.UsuarioConversacion resultUsuarioConversacion in query)
                        {
                            ML.UsuarioConversacion usuarioConversacion = new ML.UsuarioConversacion();
                            usuarioConversacion.IdUsuarioConversacion = resultUsuarioConversacion.IdUsuarioConversacion;
                            usuarioConversacion.Usuario = new ML.Usuario();
                            usuarioConversacion.Usuario.IdUsuario = resultUsuarioConversacion.IdUsuario.Value;
                            usuarioConversacion.Usuario.IdUsuario = resultUsuarioConversacion.Contacto.Value;
                            usuarioConversacion.Usuario.Nombre = resultUsuarioConversacion.Nombre;
                            usuarioConversacion.Usuario.Foto = resultUsuarioConversacion.Foto;
                            usuarioConversacion.Conversacion = new ML.Conversacion();
                            usuarioConversacion.Conversacion.IdConversacion = resultUsuarioConversacion.IdConversacion.Value;
                            usuarioConversacion.Conversacion.Fecha = resultUsuarioConversacion.Fecha;

                            result.Objects.Add(usuarioConversacion);
                        }

                        result.Correct = true;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}

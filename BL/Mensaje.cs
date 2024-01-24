using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Mensaje
    {
        public static ML.Result GetAll(int idUsuarioConversacion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    var query = context.Mensajes.FromSqlRaw($"MensajeGetall {idUsuarioConversacion}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (DataL.Mensaje resultMensaje in query)
                        {
                            ML.Mensaje mensaje = new ML.Mensaje();
                            mensaje.IdMensaje = resultMensaje.IdMensaje;
                            mensaje.Texto = resultMensaje.Texto;
                            mensaje.UsuarioConversacion = new ML.UsuarioConversacion();
                            mensaje.UsuarioConversacion.IdUsuarioConversacion = resultMensaje.IdUsuarioConversacion.Value;
                            mensaje.UsuarioConversacion.Usuario = new ML.Usuario();
                            mensaje.UsuarioConversacion.Usuario.Foto = resultMensaje.Foto;

                            result.Objects.Add(mensaje);
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

        public static ML.Result Add(ML.Mensaje mensaje)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"MensajeAdd '{mensaje.Texto}', {mensaje.UsuarioConversacion.IdUsuarioConversacion}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "";
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

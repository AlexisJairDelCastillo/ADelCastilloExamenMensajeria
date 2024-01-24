using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Conversacion
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    var query = context.Conversacions.FromSqlRaw("ConversacionGetAll").ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach(DataL.Conversacion resultConversacion in query)
                        {
                            ML.Conversacion conversacion = new ML.Conversacion();
                            conversacion.IdConversacion = resultConversacion.IdConversacion;
                            conversacion.Fecha = resultConversacion.Fecha;

                            result.Objects.Add(conversacion);
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

        public static ML.Result Add(ML.Conversacion conversacion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DataL.AdelCastilloMensajeriaContext context = new DataL.AdelCastilloMensajeriaContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ConversacionAdd '{conversacion.UsuarioConversacion.Usuario.IdUsuario}', '{conversacion.UsuarioConversacion.Contacto}'");

                    if(query > 0)
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

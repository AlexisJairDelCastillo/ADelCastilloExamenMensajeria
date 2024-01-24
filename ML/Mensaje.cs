using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }

        public string? Texto { get; set; }

        public ML.UsuarioConversacion? UsuarioConversacion { get; set; }

        public ML.Usuario? Usuario { get; set; }

        public List<object>? Mensajes { get; set;}
    }
}

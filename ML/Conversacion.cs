using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Conversacion
    {
        public int IdConversacion { get; set; }

        public DateTime? Fecha { get; set; }

        public ML.Usuario? Usuario { get; set; }

        public ML.UsuarioConversacion? UsuarioConversacion { get; set; }

        public List<object>? Conversaciones { get; set; }
    }
}

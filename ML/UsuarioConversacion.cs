using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class UsuarioConversacion
    {
        public int IdUsuarioConversacion { get; set; }

        public ML.Usuario? Usuario { get; set; }

        public int? Contacto { get; set; }

        public ML.Conversacion? Conversacion { get; set; }

        public List<object>? UsuariosConversaciones { get; set; }
    }
}

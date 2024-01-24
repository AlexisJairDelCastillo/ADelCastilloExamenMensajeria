using System;
using System.Collections.Generic;

namespace DataL;

public partial class Conversacion
{
    public int IdConversacion { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual ICollection<UsuarioConversacion> UsuarioConversacions { get; set; } = new List<UsuarioConversacion>();
}

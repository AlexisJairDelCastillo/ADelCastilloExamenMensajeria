using System;
using System.Collections.Generic;

namespace DataL;

public partial class Mensaje
{
    public int IdMensaje { get; set; }

    public string? Texto { get; set; }

    public int? IdUsuarioConversacion { get; set; }

    public string? Foto { get; set; }

    public virtual UsuarioConversacion? IdUsuarioConversacionNavigation { get; set; }
}

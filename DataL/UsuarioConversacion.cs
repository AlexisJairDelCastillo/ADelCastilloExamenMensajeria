using System;
using System.Collections.Generic;

namespace DataL;

public partial class UsuarioConversacion
{
    public int IdUsuarioConversacion { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdConversacion { get; set; }

    public int? Contacto { get; set; }

    public string? Foto { get; set; }

    public string? Nombre { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Usuario? ContactoNavigation { get; set; }

    public virtual Conversacion? IdConversacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
}

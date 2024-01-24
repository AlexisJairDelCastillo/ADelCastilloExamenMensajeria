using System;
using System.Collections.Generic;

namespace DataL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contrasenia { get; set; }

    public string? Foto { get; set; }

    public virtual ICollection<UsuarioConversacion> UsuarioConversacionContactoNavigations { get; set; } = new List<UsuarioConversacion>();

    public virtual ICollection<UsuarioConversacion> UsuarioConversacionIdUsuarioNavigations { get; set; } = new List<UsuarioConversacion>();
}

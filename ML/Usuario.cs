﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contrasenia { get; set; }

        public string? Foto { get; set; }

        public ML.UsuarioConversacion? UsuarioConversacion { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}

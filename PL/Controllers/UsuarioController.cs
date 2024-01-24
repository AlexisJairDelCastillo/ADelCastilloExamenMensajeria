using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private IHostEnvironment environment;
        private IConfiguration configuration;

        public UsuarioController(IHostEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correoElectronico, string contrasenia)
        {
            ML.Result result = BL.Usuario.Login(correoElectronico);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                if (contrasenia == usuario.Contrasenia)
                {
                    HttpContext.Session.SetInt32("Usuario", usuario.IdUsuario);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Title = "Sesión Incorrecta";
                    ViewBag.Message = "La contraseña es incorrecta.";

                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Title = "Sesión Incorrecta";
                ViewBag.Message = "El correo electronico es incorrecto.";

                return PartialView("ModalLogin");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("Login");
        }

        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.UsuarioConversacion = new ML.UsuarioConversacion();
        //    usuario.UsuarioConversacion.Conversacion = new ML.Conversacion();

        //    ML.Result resultConvesacion = BL.Conversacion.GetAll();
        //    ML.Result resultUsuarioConversacion = BL.UsuarioConversacion.GetAll();

        //    usuario.UsuarioConversacion.UsuariosConversaciones = resultUsuarioConversacion.Objects;
        //    usuario.UsuarioConversacion.Conversacion.Conversaciones = resultConvesacion.Objects;

        //    ML.Result resultUsuario = new ML.Result();
        //    resultUsuario.Objects = new List<object>();

        //    using(HttpClient client = new HttpClient())
        //    {
        //        string webApi = configuration["WebApi"];
        //        client.BaseAddress = new Uri(webApi);

        //        var responseTask = client.GetAsync("usuario/getall");
        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<ML.Result>();
        //            readTask.Wait();

        //            foreach (var item in readTask.Result.Objects)
        //            {
        //                ML.Usuario itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
        //                resultUsuario.Objects.Add(itemList);
        //            }
        //        }

        //        usuario.UsuarioConversacion.UsuariosConversaciones = resultUsuarioConversacion.Objects;
        //        usuario.UsuarioConversacion.Conversacion.Conversaciones = resultConvesacion.Objects;
        //        usuario.Usuarios = resultUsuario.Objects;
                
        //    }

        //    return View(usuario);
        //}

        [HttpGet]
        public ActionResult Registrar(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            ViewBag.Title = "Registrarse";
            ViewBag.Action = "Registrar";

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Registrar(ML.Usuario usuario)
        {
            IFormFile image = Request.Form.Files["fileImage"];

            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                usuario.Foto = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();

            using(HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Usuario>("usuario/add", usuario);
                postTask.Wait();

                HttpResponseMessage resultTask = postTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    result.Correct = true;

                    ViewBag.Title = "Te has registrado exitosamente.";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
                else
                {
                    result.Correct = false;

                    ViewBag.Title = "Ocurrio un error con tu registro, vuelve a intentarlo mas tarde.";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
            }
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpGet]
        public ActionResult Conversaciones(int idUsuario)
        {
            idUsuario = (int)HttpContext.Session.GetInt32("Usuario");

            ML.UsuarioConversacion usuarioConversacion = new ML.UsuarioConversacion();

            ML.Result resultUsuarioConversacion = new ML.Result();
            resultUsuarioConversacion.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("usuarioconversacion/getall/" + idUsuario);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.UsuarioConversacion itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.UsuarioConversacion>(item.ToString());
                        resultUsuarioConversacion.Objects.Add(itemList);
                    }
                }

                usuarioConversacion.UsuariosConversaciones = resultUsuarioConversacion.Objects;
            }

            return View(usuarioConversacion);
        }

        [HttpGet]
        public ActionResult Mensajes(int idUsuarioConversacion)
        {
            ML.Result resultUsuario = BL.Usuario.GetAll();

            ML.Mensaje mensaje = new ML.Mensaje();
            mensaje.UsuarioConversacion = new ML.UsuarioConversacion();
            mensaje.Usuario = new ML.Usuario();

            mensaje.UsuarioConversacion.IdUsuarioConversacion = idUsuarioConversacion;

            ML.Result resultMensaje = new ML.Result();
            resultMensaje.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("mensaje/getall/" + mensaje.UsuarioConversacion.IdUsuarioConversacion);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Mensaje itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Mensaje>(item.ToString());
                        resultMensaje.Objects.Add(itemList);
                    }
                }

                mensaje.Mensajes = resultMensaje.Objects;
            }

            return View(mensaje);
        }

        [HttpPost]
        public JsonResult Mensajes(string textoMensaje, int idUsuarioConversacion)
        {
            ML.Mensaje mensaje = new ML.Mensaje();
            mensaje.UsuarioConversacion = new ML.UsuarioConversacion();
            mensaje.Texto = textoMensaje;
            mensaje.UsuarioConversacion.IdUsuarioConversacion = idUsuarioConversacion;
            ML.Result result = BL.Mensaje.Add(mensaje);


            //result.Obje ct = mensaje.Texto;

            //var nuevoMensaje = result.Object;

            //return Json(nuevoMensaje);

            return Json(result.Correct);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Conversacion conversacion)
        {
            if(conversacion.IdConversacion == 0)
            {
                ML.Result result = new ML.Result();

                using(HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Conversacion>("conversacion/add", conversacion);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult NuevaConversacion(int? idConversacion)
        {
            

            ML.Conversacion conversacion = new ML.Conversacion();
            conversacion.UsuarioConversacion = new ML.UsuarioConversacion();
            conversacion.UsuarioConversacion.Usuario = new ML.Usuario();

            conversacion.UsuarioConversacion.Usuario.IdUsuario = (int)HttpContext.Session.GetInt32("Usuario");

            ML.Result resultUsuario = BL.Usuario.GetAll();

            conversacion.UsuarioConversacion.Usuario.Usuarios = resultUsuario.Objects;

            return View(conversacion);
        }

        [HttpPost]
        public ActionResult NuevaConversacion(ML.Conversacion conversacion)
        {
            conversacion.UsuarioConversacion.Usuario.IdUsuario = (int)HttpContext.Session.GetInt32("Usuario");

            if (conversacion.IdConversacion == 0)
            {
                ML.Result result = new ML.Result();

                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Conversacion>("conversacion/add/" + conversacion.UsuarioConversacion.Usuario.IdUsuario, conversacion);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;

                        ViewBag.Titulo = "Se ha generado una nueva conversacion.";
                        ViewBag.Message = result.Message;
                    }
                }
            }

            return View("Modal");
        }
    }
}

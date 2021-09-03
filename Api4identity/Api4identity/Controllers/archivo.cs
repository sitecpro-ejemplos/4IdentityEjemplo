using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api4identity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class archivo : ControllerBase
    {
        [HttpGet()]
        public IActionResult get()
        {
            return Ok();
        }

        [HttpPost("subir")]
        public IActionResult subirArchivo([FromForm] string documentID, IFormFile attach)
        {
            // _logger.LogInformation("En UploadSignedFile4Identity, id = " + documentID);
            if (attach == null)
                return Ok();
            if (attach.Length == 0)
                return Ok();
            try
            {
                string nombre = Guid.NewGuid().ToString("N") + ".pdf";
                using (var stream = new FileStream(nombre, FileMode.Create))
                    attach.CopyTo(stream);

                if (!System.IO.File.Exists(nombre))
                    throw new Exception("Error inesperado, el archivo firmado por 4Identity no existe");

                return Redirect("http://localhost/4identity/exito.html");
            }
            catch (Exception)
            {
                return Redirect("http://localhost/4identity/fracaso.html");
            }
        }
    }
}

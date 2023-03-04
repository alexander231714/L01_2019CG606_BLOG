using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2019CG606.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2019CG606.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly Context _Context;

        public publicacionesController(Context context)
        {
            _Context = context;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            List<publicaciones> ListadoPublicaciones = (from e in _Context.publicaciones
                                              select e).ToList();
            if (ListadoPublicaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoPublicaciones);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPublicacion([FromBody] publicaciones publicaciones)
        {
            try
            {
                _Context.publicaciones.Add(publicaciones);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarPublicaciones(int id, [FromBody] publicaciones publicacionModificar)
        {
            publicaciones? ModActual = (from e in _Context.publicaciones
                                       where e.publicacionId == id
                                       select e).FirstOrDefault();
            if (ModActual == null)
            {
                return NotFound();
            }

            ModActual.titulo= publicacionModificar.titulo;
            ModActual.descripcion = publicacionModificar.descripcion;
            ModActual.usuarioId = publicacionModificar.usuarioId;

            _Context.Entry(ModActual).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(publicacionModificar);

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult publicacionEliminar(int id)
        {
            publicaciones? publicacionExiste = (from e in _Context.publicaciones
                                       where e.publicacionId == id
                                       select e).FirstOrDefault();
            if (publicacionExiste == null)
            {
                return NotFound();
            }
            _Context.publicaciones.Attach(publicacionExiste);
            _Context.publicaciones.Remove(publicacionExiste);
            _Context.SaveChanges();

            return Ok(publicacionExiste);

        }
    }
}

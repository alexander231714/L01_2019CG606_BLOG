using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2019CG606.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2019CG606.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly Context _Context;

        public comentariosController(Context context)
        {
            _Context = context;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            List<comentarios> ListadoComentarios = (from e in _Context.comentarios
                                                        select e).ToList();
            if (ListadoComentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoComentarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarComentario([FromBody] comentarios comentarios)
        {
            try
            {
                _Context.comentarios.Add(comentarios);
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
        public IActionResult actualizarComentarios(int id, [FromBody] comentarios ComentarioModificar)
        {
            comentarios? ComActual = (from e in _Context.comentarios
                                        where e.cometarioId == id
                                        select e).FirstOrDefault();
            if (ComActual == null)
            {
                return NotFound();
            }

            ComActual.publicacionId = ComentarioModificar.publicacionId;
            ComActual.comentario = ComentarioModificar.comentario;
            ComActual.usuarioId = ComentarioModificar.usuarioId;

            _Context.Entry(ComActual).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(ComentarioModificar);

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult ComentarioEliminar(int id)
        {
            comentarios? comentarioExiste = (from e in _Context.comentarios
                                                where e.cometarioId == id
                                                select e).FirstOrDefault();
            if (comentarioExiste == null)
            {
                return NotFound();
            }
            _Context.comentarios.Attach(comentarioExiste);
            _Context.comentarios.Remove(comentarioExiste);
            _Context.SaveChanges();

            return Ok(comentarioExiste);

        }
    }
}

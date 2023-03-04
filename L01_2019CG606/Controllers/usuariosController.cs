using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2019CG606.Controllers;
using L01_2019CG606.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2019CG606.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly Context _Context;

        public usuariosController(Context context)
        {
            _Context = context;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            List<usuarios> ListadoUsuarios = (from e in _Context.usuarios
                                              select e).ToList();
            if(ListadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoUsuarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody]usuarios usuarios)
        {
            try
            {
                _Context.usuarios.Add(usuarios);
                _Context.SaveChanges();
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarUsuario(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? UsuarioExiste = (from e in _Context.usuarios
                                     where e.usuarioId == id
                                     select e).FirstOrDefault();
            if (UsuarioExiste == null)
            {
                return NotFound();
            }

            UsuarioExiste.rolId = usuarioModificar.rolId;
            UsuarioExiste.nombreUsuario = usuarioModificar.nombreUsuario;
            UsuarioExiste.clave = usuarioModificar.clave;
            UsuarioExiste.nombre = usuarioModificar.nombre;
            UsuarioExiste.apellido = usuarioModificar.apellido;


            _Context.Entry(UsuarioExiste).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(usuarioModificar);

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarEliminar(int id)
        {
            usuarios? usuarioExiste = (from e in _Context.usuarios
                                     where e.usuarioId == id
                                     select e).FirstOrDefault();
            if (usuarioExiste == null)
            {
                return NotFound();
            }
            _Context.usuarios.Attach(usuarioExiste);
            _Context.usuarios.Remove(usuarioExiste);
            _Context.SaveChanges();

            return Ok(usuarioExiste);

        }

    }
}

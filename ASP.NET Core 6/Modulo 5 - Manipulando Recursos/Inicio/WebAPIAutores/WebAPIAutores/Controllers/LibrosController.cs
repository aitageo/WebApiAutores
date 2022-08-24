using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAutores.DTOs;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            //var existeautor = await context.autores.anyasync(x => x.id == libro.autorid);

            //if (!existeautor)
            //{
            //    return badrequest($"no existe el autor de id: {libro.autorid}");
            //}


            var libro = mapper.Map<Libro>(libroCreacionDTO);//lo que hace esto es pasar de DTO a Entidad y de este a la variable
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

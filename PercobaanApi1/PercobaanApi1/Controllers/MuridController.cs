using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PercobaanApi1.Models;
using System;

namespace PercobaanApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuridController : ControllerBase
    {
        private string _constr;

        public MuridController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        // GET: api/Murid/read
        [HttpGet("read")]
        public ActionResult<IEnumerable<Murid>> GetMurid()
        {
            MuridContext context = new MuridContext(_constr);
            List<Murid> persons = context.ListMurid();
            return Ok(persons);
        }

        // GET: api/Murid/read/id_murid
        [HttpGet("read/{id_murid}")]
        public ActionResult<Murid> GetMurid(int id_murid)
        {
            MuridContext context = new MuridContext(_constr);
            Murid person = context.GetMuridById(id_murid);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: api/Murid/create
        [HttpPost("create")]
        public ActionResult<Murid> PostMurid(Murid person)
        {
            try
            {
                MuridContext context = new MuridContext(_constr);
                context.AddMurid(person);
                return CreatedAtAction(nameof(GetMurid), new { id_murid = person.id_murid }, person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Murid/update/id_murid
        [HttpPut("update/{id_murid}")]
        public IActionResult PutMurid(int id_murid, Murid person)
        {
            if (id_murid != person.id_murid)
            {
                return BadRequest();
            }

            try
            {
                MuridContext context = new MuridContext(_constr);
                context.UpdateMurid(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Murid/delete/id_murid
        [HttpDelete("delete/{id_murid}")]
        public IActionResult DeleteMurid(int id_murid)
        {
            try
            {
                MuridContext context = new MuridContext(_constr);
                context.DeleteMurid(id_murid);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
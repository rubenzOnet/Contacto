using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WepApiAgenda.Models;

namespace WepApiAgenda.Controllers
{
    public class ContactoController : ApiController
    {
        private AgendaCoreEntities db = new AgendaCoreEntities();

        // GET: api/Contacto
        [HttpGet]
        public List<Contacto> GetContacto()
        {
            return db.Contacto.ToList();
        }

        // GET: api/Contacto/5
        [ResponseType(typeof(Contacto))]
        [HttpGet]
        public async Task<IHttpActionResult> GetContacto(int id)
        {
            Contacto contacto = await db.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // PUT: api/Contacto/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutContacto(int id, Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacto.Id)
            {
                return BadRequest();
            }

            db.Entry(contacto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contacto
        [ResponseType(typeof(Contacto))]
        [HttpPost]
        public async Task<IHttpActionResult> PostContacto(Contacto contacto)
        {
            contacto.Fecha_alta = DateTime.Now;
            contacto.Estatus = "Activo";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacto.Add(contacto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contacto.Id }, contacto);
        }

        // DELETE: api/Contacto/5
        [ResponseType(typeof(Contacto))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteContacto(int id)
        {
            Contacto contacto = await db.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            db.Contacto.Remove(contacto);
            await db.SaveChangesAsync();

            return Ok(contacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactoExists(int id)
        {
            return db.Contacto.Count(e => e.Id == id) > 0;
        }
    }
}
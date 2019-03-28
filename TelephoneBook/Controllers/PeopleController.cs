using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TelephoneBook;

namespace TelephoneBook.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("Api/People")]
    public class PeopleController : ApiController
    {
        /// <summary>
        /// API CRUD controller for People
        /// </summary>
        ///
        private PhoneContext db = new PhoneContext();

        // GET: api/People
        /// <summary>
        /// Get info about people in DB
        /// </summary>
        /// <returns></returns>
         [HttpGet]
        [Route("ReadAllPeopleInfo")]
        public IQueryable<Person> GetPeople()
        {
            return db.People;
        }

        // GET: api/People/5
        /// <summary>
        /// Get people by known Id
        /// </summary>
        /// <param name="id">Person Id to find</param>
        /// <returns></returns>
        ///  [HttpGet]
     
       
        [ResponseType(typeof(Person))]
        [Route("ReadPersonById/{id}")]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        ///  [HttpPut]
        [Route("UpdatePerson/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        ///  [HttpPost]
        [Route("CreatePerson")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///        [HttpDelete]
        [ResponseType(typeof(Person))]
 
        [Route("DeletePersonById/{id}")]

        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }
    }
}
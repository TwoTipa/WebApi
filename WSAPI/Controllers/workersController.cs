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
using WSAPI.Models;

namespace WSAPI.Controllers
{
    public class workersController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/workers
        [ResponseType(typeof(List<ResponseWorker>))]
        public IHttpActionResult Getworker()
        {
            return Ok(db.worker.ToList().ConvertAll(p => new ResponseWorker(p)));
        }

        // GET: api/workers/5
        [ResponseType(typeof(ResponseWorker))]
        public IHttpActionResult Getworker(int id)
        {
            db.worker.Load();
            worker worker = db.worker.Local.Where(x => x.Id == id).FirstOrDefault();
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(new ResponseWorker(worker));
        }

        // PUT: api/workers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putworker(int id, worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            //db.Entry(worker).State = EntityState.Modified;

            var cur = db.worker.Where(x => x.Id == id).FirstOrDefault();
            cur.FIO = worker.FIO;
            cur.positionID = worker.positionID;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/workers
        [ResponseType(typeof(worker))]
        public IHttpActionResult Postworker(worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.worker.Add(worker);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = worker.Id }, worker);
        }

        // DELETE: api/workers/5
        [ResponseType(typeof(worker))]
        public IHttpActionResult Deleteworker(int id)
        {
            db.worker.Load();
            worker worker = db.worker.Local.Where(x => x.Id == id).FirstOrDefault();
            if (worker == null)
            {
                return NotFound();
            }

            db.worker.Remove(worker);
            db.SaveChanges();

            return Ok(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool workerExists(int id)
        {
            return db.worker.Count(e => e.Id == id) > 0;
        }
    }
}
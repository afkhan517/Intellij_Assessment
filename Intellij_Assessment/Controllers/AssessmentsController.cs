using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intellij_Assessment;

namespace Intellij_Assessment.Controllers
{
    public class AssessmentsController : Controller
    {
        private dbAssessmentEntities db = new dbAssessmentEntities();

        // GET: Assessments
        public ActionResult Index()
        {
            return View(db.Assessments.ToList());
        }

        // GET: Assessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // GET: Assessments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fdId,fdName,fdDescription,fdDateCreated,fdDateModified")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                assessment.fdDateCreated = DateTime.Now;
                db.Assessments.Add(assessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assessment);
        }

        // GET: Assessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fdId,fdName,fdDescription,fdDateCreated,fdDateModified")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                Assessment Result = (from c in db.Assessments.Where(c => c.fdId == assessment.fdId) select c).Single();
                if (Result != null)
                {
                    Result.fdName = assessment.fdName;
                    Result.fdDescription = assessment.fdDescription;
                    Result.fdDateModified = DateTime.Now;
                }
                db.Entry(Result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessment);
        }

        // GET: Assessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            db.Assessments.Remove(assessment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

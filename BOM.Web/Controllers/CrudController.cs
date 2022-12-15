using BOM.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BOM.Web.Controllers
{
    public class CrudController : Controller
    {
        AzureTestEntities azureTestEntities;
        public CrudController()
        {
            azureTestEntities = new AzureTestEntities();
        }
        // GET: Crud
        public ActionResult Index()
        {

            var getPersonData = azureTestEntities.Persons.ToList();
            return View(getPersonData);
        }

        public ActionResult Create(Person Person)
        {
            if (!string.IsNullOrWhiteSpace(Person.FirstName))
            {               
                azureTestEntities.Entry(Person).State = Person.PersonID == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
                azureTestEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            if(Person.PersonID > 0)
            {
                var getPersonData = azureTestEntities.Persons.Where(x => x.PersonID == Person.PersonID).FirstOrDefault();
                return View(getPersonData);               
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Delete(Person Person)
        {
            var getPersonData = azureTestEntities.Persons.Where(x => x.PersonID == Person.PersonID).FirstOrDefault();

            if (getPersonData?.PersonID > 0 )
            {
                azureTestEntities.Persons.Remove(getPersonData);
                azureTestEntities.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
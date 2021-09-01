using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ExpenseCategory> objList = _db.ExpenseCategory;
            return View(objList);
        }

        // GET 
        public IActionResult Create()
        {
            return View();
        }

        // POST - create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseCategory.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // GET - Delete (confirmation page)
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ExpenseCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.ExpenseCategory.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET - Update (confirmation page)
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseCategory.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
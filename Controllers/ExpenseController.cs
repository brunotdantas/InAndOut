using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expense;

            foreach (var obj in objList)
            {
                obj.ExpenseCategory = _db.ExpenseCategory.FirstOrDefault(u => u.Id == obj.ExpenseCategoryId); 
            }

            return View(objList);
        }

        // GET 
        public IActionResult Create()
        {
            ExpenseVM expenseVM = new()
            {
                Expense = new Expense(),
                CategoryDropDown = _db.ExpenseCategory.Select(i => new SelectListItem
                {
                    Text = i.ExpenseDescription,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }

        // POST - create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if(ModelState.IsValid)
            {
                //obj.ExpenseCategoryId = 1;
                _db.Expense.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        // GET - Delete (confirmation page)
        public IActionResult Delete(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Expense.Find(id);
            if(obj == null)
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
            var obj = _db.Expense.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Expense.Remove(obj);
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

            ExpenseVM expenseVM = new()
            {
                Expense = _db.Expense.Find(id),
                CategoryDropDown = _db.ExpenseCategory.Select(i => new SelectListItem
                {
                    Text = i.ExpenseDescription,
                    Value = i.Id.ToString()
                })
            };
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }

            return View(expenseVM);
        }

        // POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expense.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}

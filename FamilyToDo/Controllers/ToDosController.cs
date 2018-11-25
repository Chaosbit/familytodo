using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyToDo.Data;
using FamilyToDo.Models;
using Microsoft.AspNetCore.Identity;

namespace FamilyToDo.Controllers
{
    public class ToDosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // GET: ToDos/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            ViewData["ToDoLists"] = await _context.ToDoList.ToListAsync();
            if(id == null)
            {
                return View();
            }

            ToDoModel toDoModel = new ToDoModel { ToDoListID = (Guid)id };
            return View(toDoModel);
        }

        // POST: ToDos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Status,ToDoListID")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                toDoModel.ID = Guid.NewGuid();
                _context.Add(toDoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), nameof(ToDoListsController), new { id = toDoModel.ToDoListID });
            }
            ViewData["ToDoLists"] = await _context.ToDoList.ToListAsync();
            return View(toDoModel);
        }

        // GET: ToDos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }
            ViewData["ToDoLists"] = await _context.ToDoList.ToListAsync();
            return View(toDoModel);
        }

        // POST: ToDos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Title,Status,ToDoListID")] ToDoModel toDoModel)
        {
            if (id != toDoModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoModelExists(toDoModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), nameof(ToDoListsController), new { id = toDoModel.ToDoListID });
            }
            return View(toDoModel);
        }

        // GET: ToDos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // POST: ToDos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var toDoModel = await _context.ToDoModel.FindAsync(id);
            _context.ToDoModel.Remove(toDoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), nameof(ToDoListsController), new { id = toDoModel.ToDoListID });
        }

        private bool ToDoModelExists(Guid id)
        {
            return _context.ToDoModel.Any(e => e.ID == id);
        }
    }
}

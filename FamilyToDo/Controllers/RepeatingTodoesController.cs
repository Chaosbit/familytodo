using FamilyToDo.Data;
using FamilyToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyToDo.Controllers
{
    public class RepeatingTodoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepeatingTodoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RepeatingTodoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RepeatingTodos.ToListAsync());
        }

        // GET: RepeatingTodoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepeatingTodo repeatingTodo = await _context.RepeatingTodos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (repeatingTodo == null)
            {
                return NotFound();
            }

            return View(repeatingTodo);
        }

        // GET: RepeatingTodoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RepeatingTodoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Start,Modus,RepeatingValue")] RepeatingTodo repeatingTodo)
        {
            if (ModelState.IsValid)
            {
                repeatingTodo.ID = Guid.NewGuid();
                _context.Add(repeatingTodo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repeatingTodo);
        }

        // GET: RepeatingTodoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepeatingTodo repeatingTodo = await _context.RepeatingTodos.FindAsync(id);
            if (repeatingTodo == null)
            {
                return NotFound();
            }
            return View(repeatingTodo);
        }

        // POST: RepeatingTodoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Start,Modus,RepeatingValue,MasterTodoID")] RepeatingTodo repeatingTodo)
        {
            if (id != repeatingTodo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    RepeatingTodo existing = await _context.RepeatingTodos.FindAsync(repeatingTodo.ID);
                    if (existing != null)
                    {
                        existing.Start = repeatingTodo.Start;
                        existing.Modus = repeatingTodo.Modus;
                        existing.RepeatingValue = repeatingTodo.RepeatingValue;
                    }
                    else
                    {
                        await _context.AddAsync(repeatingTodo);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepeatingTodoExists(repeatingTodo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ToDoModel model = await _context.ToDoModel.FirstOrDefaultAsync(x => x.ID == repeatingTodo.MasterTodoID);
            return RedirectToAction(nameof(Details), "TodoLists", new { ID = model?.ToDoListID });
        }

        // GET: RepeatingTodoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepeatingTodo repeatingTodo = await _context.RepeatingTodos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (repeatingTodo == null)
            {
                return NotFound();
            }

            return View(repeatingTodo);
        }

        // POST: RepeatingTodoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            RepeatingTodo repeatingTodo = await _context.RepeatingTodos.FindAsync(id);
            _context.RepeatingTodos.Remove(repeatingTodo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepeatingTodoExists(Guid id)
        {
            return _context.RepeatingTodos.Any(e => e.ID == id);
        }
    }
}

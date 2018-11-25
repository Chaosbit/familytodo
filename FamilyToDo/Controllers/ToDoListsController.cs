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
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace FamilyToDo.Controllers
{
    [Authorize]
    public class ToDoListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        
        public ToDoListsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            return View(await _context.ToDoList.Where(x => x.UserID == user.Id).OrderBy(x => x.Order).ToListAsync());
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.GetUserAsync(User);
            var toDoList = await _context.ToDoList.Include(x => x.ToDos)
                .FirstOrDefaultAsync(m => m.ID == id && m.UserID == user.Id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Order")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                toDoList.ID = Guid.NewGuid();
                var user = await userManager.GetUserAsync(User);
                toDoList.User = user;
                _context.Add(toDoList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            var user = await userManager.GetUserAsync(User);
            if (toDoList.UserID != user.Id)
            {
                return Unauthorized();
            }

            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Order")] ToDoList toDoList)
        {
            if (id != toDoList.ID)
            {
                return NotFound();
            }

            var oldTodoList = await _context.ToDoList.FirstOrDefaultAsync(x => x.ID == id);

            var user = await userManager.GetUserAsync(User);
            if (oldTodoList.UserID != user.Id)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    oldTodoList.Name = toDoList.Name;
                    oldTodoList.Order = toDoList.Order;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var toDoList = await _context.ToDoList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoList == null)
            {
                return NotFound();
            }


            var user = await userManager.GetUserAsync(User);
            if (toDoList.UserID != user.Id)
            {
                return Unauthorized();
            }


            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var toDoList = await _context.ToDoList.FindAsync(id);



            var user = await userManager.GetUserAsync(User);
            if (toDoList?.UserID != user.Id)
            {
                return Unauthorized();
            }

            _context.ToDoList.Remove(toDoList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(Guid id)
        {
            return _context.ToDoList.Any(e => e.ID == id);
        }
    }
}

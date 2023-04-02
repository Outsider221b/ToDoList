using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;
        
        public ToDoController(ToDoContext context)
        {
            this.context= context;
        }

        public async Task<ActionResult> Index()
        {
            IQueryable<ToDo> items = from i in context.ToDoList orderby i.Id select i;
            List<ToDo> todoList = await items.ToListAsync();
            return View(todoList);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDo newToDo)
        {
            if(ModelState.IsValid)
            {
                context.Add(newToDo);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been added!";

                return RedirectToAction(nameof(Index));
            }
            return View(newToDo);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ToDo item = await context.ToDoList.FindAsync(id);
            if(item == null)
            {
                return NotFound(item);
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDo item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been updated!";

                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<ActionResult> Delete(int id)
        {
            ToDo item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been deleted!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

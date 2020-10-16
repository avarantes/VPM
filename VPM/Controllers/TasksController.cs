using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPM.Data;
using VPM.Models;

namespace VPM.Controllers
{
    public class TasksController : Controller
    {
        private readonly VPMDBContext _context;

        public TasksController(VPMDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IIncludableQueryable<Models.Task, Project> vPMDBContext = _context.Task.Include(t => t.Project);
            List<Models.Task> tasks = await vPMDBContext.ToListAsync();

            return View(tasks);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Models.Task task = await _context.Task
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");

            Models.Task task = new Models.Task
            {
                CreateDate = DateTime.UtcNow
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Title,Description,BillableTime,CreateDate,EndDate,ProjectId,CostPerHour")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                task.TaskCost = CalculateTaskCost(task);

                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);
            return View(task);
        }

        private decimal? CalculateTaskCost(Models.Task task)
        {
            if (task.BillableTime.HasValue)
            {
                string billableTimeString = task.BillableTime.Value.ToShortTimeString();
                decimal time = Convert.ToDecimal(TimeSpan.Parse(billableTimeString).TotalHours);

                return time * task.CostPerHour;
            }
            return 0;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Models.Task task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.TaskCost = CalculateTaskCost(task);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Title,Description,BillableTime,CreateDate,EndDate,ProjectId, CostPerHour")] Models.Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.TaskCost = CalculateTaskCost(task);
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);
            return View(task);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Models.Task task = await _context.Task
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Models.Task task = await _context.Task.FindAsync(id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskId == id);
        }
    }
}
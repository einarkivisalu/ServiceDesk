using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ServiceDeskContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TicketsController(ServiceDeskContext context)
        {
            _context = context;
        }

        // GET: Tickets
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_asc" : "Status";
            ViewData["PrioritySortParm"] = sortOrder == "Priority" ? "priority_asc" : "Priority";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_asc" : "Date";
            var tickets = from t in _context.Ticket
                          where t.TicketStatus != Status.Closed //Show only open tickets
                          select t;
            switch (sortOrder)
            {
                case "Status":
                    tickets = tickets.OrderByDescending(t => t.TicketStatus);
                    break;
                case "status_asc":
                    tickets = tickets.OrderBy(t => t.TicketStatus);
                    break;
                case "Priority":
                    tickets = tickets.OrderByDescending(t => t.TicketPriority);
                    break;
                case "priority_asc":
                    tickets = tickets.OrderBy(t => t.TicketPriority);
                    break;
                case "Date":
                    tickets = tickets.OrderByDescending(t => t.TicketDate);
                    break;
                case "date_asc":
                    tickets = tickets.OrderBy(t => t.TicketDate);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.TicketId);
                    break;
            }

             return View(await tickets.AsNoTracking().ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,TicketTitle,TicketDate,Email,TicketProblem,TicketPriority,TicketStatus")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));               
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,TicketTitle,TicketDate,Email,TicketProblem,TicketPriority,TicketStatus")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            return View(ticket);
        }

/*      //Tickets deleting is excluded from actual version
        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        // GET: Tickets/Close/5
        public async Task<IActionResult> Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Close/5
        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            ticket.TicketStatus = Status.Closed;
            _context.Update(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketId == id);
        }
    }
}

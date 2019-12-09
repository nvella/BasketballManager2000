using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballManager2000.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BasketballManager2000.Controllers
{
    public class GamesPaidBySelectItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static async Task<IEnumerable<GamesPaidBySelectItem>> GetItems(IQueryable<Member> memberSet)
        => (await memberSet.Include(m => m.User).Select(m => new GamesPaidBySelectItem
        {
            Id = m.MemberId.ToString(),
            Name = m.User.Email
        }).ToListAsync()).Prepend(new GamesPaidBySelectItem { Id = "", Name = "<not paid>" });
    }

    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Games.OrderBy(g => g.GameDate).Where(m => m.GameDate >= DateTime.Now).Include(g => g.PaidByMember).Include(g => g.PaidByMember.User);
            ViewData["Title"] = "Upcoming Games";
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Games, past
        [HttpGet("PastGames")]
        public async Task<IActionResult> PastIndex()
        {
            var applicationDbContext = _context.Games.OrderBy(g => g.GameDate).Where(m => m.GameDate < DateTime.Now).Include(g => g.PaidByMember).Include(g => g.PaidByMember.User);
            ViewData["Title"] = "Past Games";
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.PaidByMember)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        [ServiceFilter(typeof(MemberMustBeManager))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(MemberMustBeManager))]
        public async Task<IActionResult> Create([Bind("GameId,GameDate,Venue")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }


            ViewData["PaidByMemberId"] = new SelectList(await GamesPaidBySelectItem.GetItems(_context.Members), "Id", "Name", game.PaidByMemberId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GameDate,Venue,PaidAmount,PaidByMemberId")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
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

            ViewData["PaidByMemberId"] = new SelectList(await GamesPaidBySelectItem.GetItems(_context.Members), "Id", "Name", game.PaidByMemberId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.PaidByMember)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}

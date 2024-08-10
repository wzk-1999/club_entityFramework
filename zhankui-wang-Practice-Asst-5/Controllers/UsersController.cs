using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zhankui_wang_Practice_Asst_5.Migrations;
using zhankui_wang_Practice_Asst_5.Models;
using zhankui_wang_Practice_Asst_5.Utilities;

namespace zhankui_wang_Practice_Asst_5.Controllers
{
    public class UsersController : Controller
    {
        private readonly PracticeAsst5DbContext _context;

        public UsersController(PracticeAsst5DbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            // Fetch users and clubs
            var users = await _context.Users.Include(u => u.Clubs).ToListAsync();
            var allClubs = await _context.Clubs.ToListAsync();

            // Create a dictionary to hold available clubs for each user
            var userAvailableClubs = new Dictionary<int, List<BridgeClub>>();

            // Populate the dictionary with filtered clubs for each user
            foreach (var user in users)
            {
                var userClubs = user.Clubs.Select(c => c.ClubID).ToList();
                var availableClubs = allClubs.Where(c => !userClubs.Contains(c.ClubID)).ToList();
                userAvailableClubs[user.Id] = availableClubs;
            }

            // Prepare ViewData for the view
            ViewData["UserAvailableClubs"] = userAvailableClubs;

            return View(users);
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CityNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["CityName"] = new SelectList(_context.Cities, "Name", "Name");
            return View();
        }

        [HttpGet]
        public JsonResult GetProvinceByCity(string cityName)
        {
            string province = CRUDutilities.ProvinceOfCity(cityName);
            return Json(province);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,CityName,PostalCode")] User user)
        {
            if (!string.IsNullOrWhiteSpace(user.PostalCode))
            {
                user.PostalCode = Utility.PostalCode(user.PostalCode);
            }
            ModelState.Remove(nameof(user.CityNameNavigation));

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityName"] = new SelectList(_context.Cities, "Name", "Name", user.CityName);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["CityName"] = new SelectList(_context.Cities, "Name", "Name", user.CityName);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,CityName,PostalCode")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["CityName"] = new SelectList(_context.Cities, "Name", "Name", user.CityName);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CityNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult NewClub()
        {
            ViewBag.CityNames = new SelectList(_context.Cities, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewClub(BridgeClub club)
        {
            ModelState.Remove(nameof(club.City));
            if (ModelState.IsValid)
            {
                _context.Clubs.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Clubs)); // Redirect to the Clubs action
            }

            ViewBag.CityNames = new SelectList(_context.Cities, "Name", "Name");
            return View(club);
        }

        public async Task<IActionResult> Clubs()
        {
            var clubs = await _context.Clubs
                .Include(c => c.Members)
                .ToListAsync(); return View(clubs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinClub([FromBody] JoinClubRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _context.Users.Include(u => u.Clubs).FirstOrDefaultAsync(u => u.Id == request.UserId);
            var club = await _context.Clubs.FindAsync(request.ClubId);

            if (user == null || club == null)
            {
                return NotFound();
            }

            if (!user.Clubs.Contains(club))
            {
                user.Clubs.Add(club);
                club.Members.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("User is already a member of this club.");
        }



        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

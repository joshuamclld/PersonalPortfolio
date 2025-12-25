// My Personal Portfolio's Main Controller
// This controller is responsible for showing my main homepage.

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalProjectPortfolio.Models;
using FinalProjectPortfolio.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // This is the main action for my portfolio.
    // It gathers all my information from the database and puts it into a single view model.
    public async Task<IActionResult> Index()
    {
        var viewModel = new PortfolioViewModel
        {
            Profile = await _context.Profiles.FirstOrDefaultAsync(),
            About = await _context.Abouts.FirstOrDefaultAsync(),
            Experiences = await _context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(),
            Projects = await _context.Projects.ToListAsync(),
            Contact = await _context.Contacts.FirstOrDefaultAsync(),
            Skills = await _context.Skills.ToListAsync(),
            SocialLinks = await _context.SocialLinks.ToListAsync(),
            Educations = await _context.Educations.OrderByDescending(e => e.StartDate).ToListAsync(),
            Services = await _context.Services.ToListAsync()
        };
        return View(viewModel);
    }

    // This is the standard privacy page.
    public IActionResult Privacy()
    {
        return View();
    }

    // This is the standard error page.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

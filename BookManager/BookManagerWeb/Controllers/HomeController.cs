using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookManagerWeb.Models;
using BookManagerWeb.Services;

namespace BookManagerWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FileSystemService _fileSystemService;

    public HomeController(ILogger<HomeController> logger, FileSystemService fileSystemService)
    {
        _logger = logger;
        _fileSystemService = fileSystemService;
    }

    public IActionResult Index()
    {
        // Get available drives for initial navigation
        var drives = _fileSystemService.GetDrives();
        ViewBag.Drives = drives;
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

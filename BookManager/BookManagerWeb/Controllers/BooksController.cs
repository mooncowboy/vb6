using Microsoft.AspNetCore.Mvc;
using BookManagerWeb.Models;
using BookManagerWeb.Services;

namespace BookManagerWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookInfoService _bookInfoService;
        private readonly FileSystemService _fileSystemService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            BookInfoService bookInfoService,
            FileSystemService fileSystemService,
            ILogger<BooksController> logger)
        {
            _bookInfoService = bookInfoService;
            _fileSystemService = fileSystemService;
            _logger = logger;
        }

        /// <summary>
        /// Browse folders at the specified path
        /// </summary>
        public IActionResult Browse(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                var drives = _fileSystemService.GetDrives();
                ViewBag.CurrentPath = string.Empty;
                ViewBag.Drives = drives;
                ViewBag.Folders = new List<FolderItem>();
                ViewBag.ParentPath = null;
                return View();
            }

            if (!_fileSystemService.IsValidPath(path))
            {
                TempData["Error"] = "Invalid or inaccessible path.";
                return RedirectToAction(nameof(Browse));
            }

            var folders = _fileSystemService.GetDirectories(path);
            var parentPath = _fileSystemService.GetParentDirectory(path);

            ViewBag.CurrentPath = path;
            ViewBag.Folders = folders;
            ViewBag.ParentPath = parentPath;
            ViewBag.Drives = _fileSystemService.GetDrives();

            return View();
        }

        /// <summary>
        /// Display book details
        /// </summary>
        public IActionResult Details(string path)
        {
            if (string.IsNullOrEmpty(path) || !_fileSystemService.IsValidPath(path))
            {
                TempData["Error"] = "Invalid book directory.";
                return RedirectToAction(nameof(Browse));
            }

            var bookInfo = _bookInfoService.LoadFromDirectory(path);
            if (bookInfo == null)
            {
                TempData["Error"] = "No book information found in this directory.";
                return RedirectToAction(nameof(Browse), new { path });
            }

            return View(bookInfo);
        }

        /// <summary>
        /// Edit book information
        /// </summary>
        public IActionResult Edit(string path)
        {
            if (string.IsNullOrEmpty(path) || !_fileSystemService.IsValidPath(path))
            {
                TempData["Error"] = "Invalid book directory.";
                return RedirectToAction(nameof(Browse));
            }

            var bookInfo = _bookInfoService.LoadFromDirectory(path) ?? new BookInfo { Directory = path };
            return View(bookInfo);
        }

        /// <summary>
        /// Save edited book information
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookInfo bookInfo)
        {
            if (!_fileSystemService.IsValidPath(bookInfo.Directory))
            {
                TempData["Error"] = "Invalid book directory.";
                return RedirectToAction(nameof(Browse));
            }

            if (ModelState.IsValid)
            {
                var success = _bookInfoService.SaveToDirectory(bookInfo, bookInfo.Directory);
                if (success)
                {
                    TempData["Success"] = "Book information saved successfully.";
                    return RedirectToAction(nameof(Details), new { path = bookInfo.Directory });
                }
                else
                {
                    TempData["Error"] = "Failed to save book information.";
                }
            }

            return View(bookInfo);
        }

        /// <summary>
        /// Search for books in a directory tree
        /// </summary>
        public IActionResult Search(string? rootPath)
        {
            if (string.IsNullOrEmpty(rootPath))
            {
                ViewBag.Books = new List<FolderItem>();
                return View();
            }

            if (!_fileSystemService.IsValidPath(rootPath))
            {
                TempData["Error"] = "Invalid or inaccessible path.";
                ViewBag.Books = new List<FolderItem>();
                return View();
            }

            var bookFolders = _fileSystemService.SearchBookDirectories(rootPath);
            ViewBag.Books = bookFolders;
            ViewBag.RootPath = rootPath;

            return View();
        }
    }
}

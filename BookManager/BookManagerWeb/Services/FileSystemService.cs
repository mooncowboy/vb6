using BookManagerWeb.Models;

namespace BookManagerWeb.Services
{
    /// <summary>
    /// Service for browsing file system and finding book directories
    /// </summary>
    public class FileSystemService
    {
        private readonly BookInfoService _bookInfoService;

        public FileSystemService(BookInfoService bookInfoService)
        {
            _bookInfoService = bookInfoService;
        }

        /// <summary>
        /// Gets directories in the specified path
        /// </summary>
        public List<FolderItem> GetDirectories(string path)
        {
            var folders = new List<FolderItem>();

            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return folders;

            try
            {
                var directories = Directory.GetDirectories(path);
                
                foreach (var dir in directories)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    var bookInfoPath = Path.Combine(dir, "bookinfo.dat");
                    
                    folders.Add(new FolderItem
                    {
                        Name = dirInfo.Name,
                        Path = dir,
                        HasBookInfo = File.Exists(bookInfoPath),
                        LastModified = dirInfo.LastWriteTime
                    });
                }
            }
            catch
            {
                // Ignore access errors
            }

            return folders.OrderBy(f => f.Name).ToList();
        }

        /// <summary>
        /// Gets logical drives on the system
        /// </summary>
        public List<string> GetDrives()
        {
            try
            {
                return Directory.GetLogicalDrives().ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Validates if a path exists and is accessible
        /// </summary>
        public bool IsValidPath(string path)
        {
            try
            {
                return !string.IsNullOrEmpty(path) && Directory.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets parent directory path
        /// </summary>
        public string? GetParentDirectory(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                return dirInfo.Parent?.FullName;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Searches for directories containing bookinfo.dat files
        /// </summary>
        public List<FolderItem> SearchBookDirectories(string rootPath, int maxDepth = 2)
        {
            var bookFolders = new List<FolderItem>();

            if (!IsValidPath(rootPath))
                return bookFolders;

            SearchRecursive(rootPath, 0, maxDepth, bookFolders);
            return bookFolders.OrderBy(f => f.Path).ToList();
        }

        private void SearchRecursive(string path, int currentDepth, int maxDepth, List<FolderItem> results)
        {
            if (currentDepth > maxDepth)
                return;

            try
            {
                var bookInfoPath = Path.Combine(path, "bookinfo.dat");
                if (File.Exists(bookInfoPath))
                {
                    var dirInfo = new DirectoryInfo(path);
                    results.Add(new FolderItem
                    {
                        Name = dirInfo.Name,
                        Path = path,
                        HasBookInfo = true,
                        LastModified = dirInfo.LastWriteTime
                    });
                }

                // Continue searching subdirectories
                foreach (var subDir in Directory.GetDirectories(path))
                {
                    SearchRecursive(subDir, currentDepth + 1, maxDepth, results);
                }
            }
            catch
            {
                // Ignore access errors
            }
        }
    }
}

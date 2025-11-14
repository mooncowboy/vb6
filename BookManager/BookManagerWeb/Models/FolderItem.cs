namespace BookManagerWeb.Models
{
    /// <summary>
    /// Represents a folder in the file system that may contain book data
    /// </summary>
    public class FolderItem
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public bool HasBookInfo { get; set; }
        public DateTime? LastModified { get; set; }
    }
}

namespace BookManagerWeb.Models
{
    /// <summary>
    /// Represents book metadata information stored in bookinfo.dat files
    /// </summary>
    public class BookInfo
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string SsId { get; set; } = string.Empty;
        public string IsJpgBook { get; set; } = string.Empty;
        public string PagesCount { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string PublishDate { get; set; } = string.Empty;
        public string StartPage { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string PageUrl { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string SsUrl { get; set; } = string.Empty;
        public string JpgUrl { get; set; } = string.Empty;
        public string IeJpgUrl { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string SavedIn { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public string Downloader { get; set; } = string.Empty;
        public string DownloadDate { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
        
        // Additional properties for display
        public string Directory { get; set; } = string.Empty;
        public string CoverImagePath { get; set; } = string.Empty;
        public bool HasCover { get; set; }
    }
}

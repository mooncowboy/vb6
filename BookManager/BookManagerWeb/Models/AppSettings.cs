namespace BookManagerWeb.Models
{
    /// <summary>
    /// Application settings model
    /// </summary>
    public class AppSettings
    {
        public string PdgProgram { get; set; } = string.Empty;
        public string FolderProgram { get; set; } = string.Empty;
        public string FormatString { get; set; } = "%t - %a - %s";
        public string DefaultPath { get; set; } = string.Empty;
    }
}

using BookManagerWeb.Models;

namespace BookManagerWeb.Services
{
    /// <summary>
    /// Service for reading and writing BookInfo data from/to bookinfo.dat INI files
    /// </summary>
    public class BookInfoService
    {
        private const string BookInfoFileName = "bookinfo.dat";
        private const string SectionName = "General Information";

        /// <summary>
        /// Loads book information from a bookinfo.dat file in the specified directory
        /// </summary>
        public BookInfo? LoadFromDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return null;

            var infoFilePath = Path.Combine(directoryPath, BookInfoFileName);
            if (!File.Exists(infoFilePath))
                return null;

            var bookInfo = new BookInfo
            {
                Directory = directoryPath
            };

            try
            {
                var lines = File.ReadAllLines(infoFilePath);
                bool inSection = false;

                foreach (var line in lines)
                {
                    var trimmedLine = line.Trim();

                    // Check for section header
                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    {
                        var section = trimmedLine.Substring(1, trimmedLine.Length - 2);
                        inSection = section.Equals(SectionName, StringComparison.OrdinalIgnoreCase);
                        continue;
                    }

                    if (!inSection)
                        continue;

                    // Parse key=value pairs
                    var separatorIndex = trimmedLine.IndexOf('=');
                    if (separatorIndex < 0)
                        continue;

                    var key = trimmedLine.Substring(0, separatorIndex).Trim();
                    var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                    // Map Chinese field names to BookInfo properties
                    SetFieldValue(bookInfo, key, value);
                }

                // Check for cover image
                CheckForCover(bookInfo, directoryPath);

                return bookInfo;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Saves book information to a bookinfo.dat file
        /// </summary>
        public bool SaveToDirectory(BookInfo bookInfo, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return false;

            var infoFilePath = Path.Combine(directoryPath, BookInfoFileName);

            try
            {
                using var writer = new StreamWriter(infoFilePath, false, System.Text.Encoding.GetEncoding("GB2312"));
                writer.WriteLine($"[{SectionName}]");

                WriteField(writer, "书名", bookInfo.Title);
                WriteField(writer, "作者", bookInfo.Author);
                WriteField(writer, "SS号", bookInfo.SsId);
                WriteField(writer, "是JPG书", bookInfo.IsJpgBook);
                WriteField(writer, "页数", bookInfo.PagesCount);
                WriteField(writer, "出版社", bookInfo.Publisher);
                WriteField(writer, "出版日期", bookInfo.PublishDate);
                WriteField(writer, "起始页", bookInfo.StartPage);
                WriteField(writer, "主题", bookInfo.Subject);
                WriteField(writer, "备注", bookInfo.Comments);
                WriteField(writer, "页面地址", bookInfo.PageUrl);
                WriteField(writer, "地址", bookInfo.Url);
                WriteField(writer, "SS地址", bookInfo.SsUrl);
                WriteField(writer, "JPG地址", bookInfo.JpgUrl);
                WriteField(writer, "IEJPG地址", bookInfo.IeJpgUrl);
                WriteField(writer, "参数", bookInfo.Params);
                WriteField(writer, "保存在", bookInfo.SavedIn);
                WriteField(writer, "书名号", bookInfo.Header);
                WriteField(writer, "下载人", bookInfo.Downloader);
                WriteField(writer, "下载日期", bookInfo.DownloadDate);
                WriteField(writer, "HTML内容", bookInfo.HtmlContent);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteField(StreamWriter writer, string fieldName, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteLine($"{fieldName}={value}");
            }
        }

        private void SetFieldValue(BookInfo bookInfo, string key, string value)
        {
            // Map Chinese field names to properties
            switch (key)
            {
                case "书名":
                    bookInfo.Title = value;
                    break;
                case "作者":
                    bookInfo.Author = value;
                    break;
                case "SS号":
                    bookInfo.SsId = value;
                    break;
                case "是JPG书":
                    bookInfo.IsJpgBook = value;
                    break;
                case "页数":
                    bookInfo.PagesCount = value;
                    break;
                case "出版社":
                    bookInfo.Publisher = value;
                    break;
                case "出版日期":
                    bookInfo.PublishDate = value;
                    break;
                case "起始页":
                    bookInfo.StartPage = value;
                    break;
                case "主题":
                    bookInfo.Subject = value;
                    break;
                case "备注":
                    bookInfo.Comments = value;
                    break;
                case "页面地址":
                    bookInfo.PageUrl = value;
                    break;
                case "地址":
                    bookInfo.Url = value;
                    break;
                case "SS地址":
                    bookInfo.SsUrl = value;
                    break;
                case "JPG地址":
                    bookInfo.JpgUrl = value;
                    break;
                case "IEJPG地址":
                    bookInfo.IeJpgUrl = value;
                    break;
                case "参数":
                    bookInfo.Params = value;
                    break;
                case "保存在":
                    bookInfo.SavedIn = value;
                    break;
                case "书名号":
                    bookInfo.Header = value;
                    break;
                case "下载人":
                    bookInfo.Downloader = value;
                    break;
                case "下载日期":
                    bookInfo.DownloadDate = value;
                    break;
                case "HTML内容":
                    bookInfo.HtmlContent = value;
                    break;
            }
        }

        private void CheckForCover(BookInfo bookInfo, string directoryPath)
        {
            var coverPdg = Path.Combine(directoryPath, "cov001.pdg");
            var coverJpg = Path.Combine(directoryPath, "cov001.jpg");

            if (File.Exists(coverPdg))
            {
                bookInfo.CoverImagePath = coverPdg;
                bookInfo.HasCover = true;
            }
            else if (File.Exists(coverJpg))
            {
                bookInfo.CoverImagePath = coverJpg;
                bookInfo.HasCover = true;
            }
        }
    }
}

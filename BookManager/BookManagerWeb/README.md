# BookManager Web - .NET 8 ASP.NET MVC

This is a modern .NET 8 ASP.NET MVC conversion of the original VB6 BookManager desktop application.

## Overview

BookManager is a web-based application for managing PDG (Chinese e-book format) book files and their associated metadata stored in `bookinfo.dat` files.

## Features

- **Browse Directories**: Navigate through your file system to find book collections
- **View Book Details**: Display comprehensive book metadata from bookinfo.dat files
- **Edit Metadata**: Modify and save book information
- **Search Books**: Scan directory trees to find all books with metadata
- **Cover Display**: View book covers (JPG format supported)
- **Chinese Metadata Support**: Full support for Chinese book metadata fields

## Technology Stack

- **.NET 8**: Latest .NET runtime
- **ASP.NET Core MVC**: Modern web framework
- **C#**: Primary programming language
- **Bootstrap 5**: UI framework
- **Bootstrap Icons**: Icon library

## Getting Started

### Prerequisites

- .NET 8 SDK or later

### Running the Application

1. Navigate to the BookManagerWeb directory:
   ```bash
   cd BookManager/BookManagerWeb
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to:
   - HTTPS: `https://localhost:5001`
   - HTTP: `http://localhost:5000`

### Building the Application

```bash
dotnet build
```

### Publishing the Application

```bash
dotnet publish -c Release -o ./publish
```

## Project Structure

```
BookManagerWeb/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   └── BooksController.cs
├── Models/              # Data models
│   ├── BookInfo.cs
│   ├── AppSettings.cs
│   └── FolderItem.cs
├── Services/            # Business logic services
│   ├── BookInfoService.cs
│   └── FileSystemService.cs
├── Views/               # Razor views
│   ├── Home/
│   ├── Books/
│   └── Shared/
├── wwwroot/             # Static files
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration
```

## Book Metadata Fields

The application manages the following fields in bookinfo.dat files:

- Title (书名)
- Author (作者)
- SS ID (SS号)
- Publisher (出版社)
- Publish Date (出版日期)
- Pages Count (页数)
- Subject (主题)
- Comments (备注)
- URL (地址)
- And more...

## Migration from VB6

This application is a complete rewrite of the original VB6 BookManager application with the following improvements:

- **Web-based**: Access from any device with a browser
- **Modern UI**: Responsive Bootstrap 5 design
- **Cross-platform**: Runs on Windows, Linux, and macOS
- **No dependencies**: Self-contained .NET application
- **Maintainable**: Clean architecture with separation of concerns

## Notes

- The application uses GB2312 encoding for reading and writing bookinfo.dat files to maintain compatibility with the VB6 version
- PDG cover files cannot be displayed in the browser; only JPG covers are supported
- The application is designed for local file system access

## License

This project maintains compatibility with the original VB6 BookManager application.

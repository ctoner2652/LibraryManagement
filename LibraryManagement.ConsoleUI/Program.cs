
using LibraryManagement.Application;
using LibraryManagement.ConsoleUI;

var config = new AppConfiguration();

App app = new App(config);

app.Run();
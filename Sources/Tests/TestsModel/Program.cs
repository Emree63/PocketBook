// See https://aka.ms/new-console-template for more information
using Model;

StubLib.LibraryStub libStub = new StubLib.LibraryStub();
var book = await libStub.GetBookByISBN("9782330033118");
Console.WriteLine(book.Title);

Manager manager = new Manager(libStub);
var book2 = await manager.GetBookByISBN("9782330033118");
Console.WriteLine(book2.Title);

var booksFromHerbert = await manager.GetBooksByAuthor("herbert", 0, 10);
foreach(var b in booksFromHerbert.books)
{
    Console.WriteLine($"\t{b.Title}");
}

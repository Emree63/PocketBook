// See https://aka.ms/new-console-template for more information
using Model;
using static System.Console;

WriteLine("Test LibraryStub.GetBookByISBN");
StubLib.LibraryStub libStub = new StubLib.LibraryStub();
var book = await libStub.GetBookByISBN("9782330033118");
WriteLine(book.Title);
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetBookByISBN");
Manager manager = new Manager(libStub, new StubLib.UserLibraryStub(libStub));
var book2 = await manager.GetBookByISBN("9782330033118");
WriteLine(book2.Title);
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetBooksByAuthor");
var booksFromHerbert = await manager.GetBooksByAuthor("herbert", 0, 10);
foreach(var b in booksFromHerbert.books)
{
    WriteLine($"\t{b.Title}");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetBooks");
var books = await manager.GetBooksByTitle("", 0, 100);
foreach(var b in books.books)
{
    WriteLine($"\t{b.Title}");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.AddBook");
var book1 = await manager.AddBookToCollection("/books/OL25910297M");
if(book1 == null) book1 = await manager.GetBookByIdFromCollection("/books/OL25910297M");
book1.Status = Status.Finished;
book1.UserRating = 5;
book1.UserNote = "Trop bien !";
manager.UpdateBook(book1);

manager.AddBookToCollection("/books/OL26210208M");
manager.AddBookToCollection("/books/OL27258011M");
var mybooks = await manager.GetBooksFromCollection(0, 100);
foreach(var b in mybooks.books)
{
    WriteLine($"\t{b.Title} {b.UserRating ?? -1}");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetContacts");
var contacts = await manager.GetContacts(0, 100);
foreach(var c in contacts.contacts)
{
    WriteLine($"\t{c.FirstName} {c.LastName}");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetCurrentLoans");
var loans = await manager.GetCurrentLoans(0, 100);
foreach(var l in loans.loans)
{
    WriteLine($"\t{l.Book.Title} -> {l.Loaner.FirstName} {l.Loaner.LastName} ({l.LoanedAt.ToShortDateString()})");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetPastLoans");
var loans2 = await manager.GetPastLoans(0, 100);
foreach(var l in loans2.loans)
{
    WriteLine($"\t{l.Book.Title} -> {l.Loaner.FirstName} {l.Loaner.LastName} ({l.LoanedAt.ToShortDateString()})");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetCurrentBorrowings");
var borrowings = await manager.GetCurrentBorrowings(0, 100);
foreach(var b in borrowings.borrowings)
{
    WriteLine($"\t{b.Book.Title} -> {b.Owner.FirstName} {b.Owner.LastName} ({b.BorrowedAt.ToShortDateString()})");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

WriteLine("Test Manager.GetPastBorrowings");
var borrowings2 = await manager.GetPastBorrowings(0, 100);
foreach(var b in borrowings2.borrowings)
{
    WriteLine($"\t{b.Book.Title} -> {b.Owner.FirstName} {b.Owner.LastName} ({b.BorrowedAt.ToShortDateString()})");
}
WriteLine(new string('*', WindowWidth));
WriteLine();

ReadLine();
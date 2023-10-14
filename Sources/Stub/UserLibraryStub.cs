using System;
using System.Collections.ObjectModel;
using Model;
using System.Linq;
using LibraryDTO;
using System.Xml.Linq;

namespace StubLib
{
	public class UserLibraryStub : IUserLibraryManager
	{
        public ReadOnlyCollection<Book> Favorites { get; private set; }
        private List<Book> favorites = new List<Book>();

        public ReadOnlyCollection<Book> Books { get; private set; }
        private List<Book> books = new List<Book>();

        public ReadOnlyCollection<Contact> Contacts { get; private set; }
        private List<Contact> contacts = new List<Contact>();

        public ReadOnlyCollection<Loan> Loans { get; private set; }
        private List<Loan> loans = new List<Loan>();

        public ReadOnlyCollection<Borrowing> Borrowings { get; private set; }
        private List<Borrowing> borrowings = new List<Borrowing>();

        public ILibraryManager LibraryMgr { get; private set; }

		public UserLibraryStub(ILibraryManager libraryMgr)
		{
            LibraryMgr = libraryMgr;
            Favorites = new ReadOnlyCollection<Book>(favorites);
            Books = new ReadOnlyCollection<Book>(books);
            Borrowings = new ReadOnlyCollection<Borrowing>(borrowings);
            Loans = new ReadOnlyCollection<Loan>(loans);
            Contacts = new ReadOnlyCollection<Contact>(contacts);

            contacts.AddRange(new Contact[]
            {
                new Contact { Id = "/contacts/01", FirstName = "Audrey", LastName = "Pouclet" },
                new Contact { Id = "/contacts/02", FirstName = "Malika", LastName = "More" },
                new Contact { Id = "/contacts/03", FirstName = "Antoine" },
            });
            books.AddRange(new Book[]
            {
                LibraryMgr.GetBookById("/books/OL25910297M").Result,
                LibraryMgr.GetBookById("/books/OL26210208M").Result,
                LibraryMgr.GetBookById("/books/OL27258011M").Result,
                LibraryMgr.GetBookById("/books/OL28294024M").Result,
                LibraryMgr.GetBookById("/books/OL28639494M").Result,
                LibraryMgr.GetBookById("/books/OL35699439M").Result,
                LibraryMgr.GetBookById("/books/OL37758347M").Result,
                LibraryMgr.GetBookById("/books/OL38218739M").Result,
                LibraryMgr.GetBookById("/books/OL38586212M").Result,
                LibraryMgr.GetBookById("/books/OL8839071M").Result,
                LibraryMgr.GetBookById("/books/OL8198056M").Result,
            });
            books[0].Status = Status.Finished;
            books[0].UserNote = "Super bouquin de SF !";
            books[0].UserRating = 4.5f;
            loans.Add(new Loan { Id = "/loans/01", Book = books[0], Loaner = contacts[0], LoanedAt = new DateTime(2022, 7, 12), ReturnedAt = new DateTime(2023, 9, 1) });
            books[1].Status = Status.ToBeRead;
            books[2].Status = Status.Finished;
            books[2].UserNote = "Des nouvelles de SF. Super auteur à découvrir !";
            books[2].UserRating = 4.8f;
            books[3].Status = Status.Finished;
            books[3].UserRating = 4.0f;
            loans.Add(new Loan { Id = "/loans/02", Book = books[3], Loaner = contacts[2], LoanedAt = new DateTime(2020, 12, 23), ReturnedAt = new DateTime(2021, 8, 13) } );
            books[4].Status = Status.Finished;
            books[4].UserNote = "Déjà moins connu que le premier, et pourtant...";
            books[4].UserRating = 4.2f;
            books[5].Status = Status.Finished;
            books[5].UserNote = "Coup de coeur. Poétique, anarchique, philosophique... + SF. Du Deleuze et du Foucault chez Damasio";
            books[5].UserRating = 4.9f;
            books[6].Status = Status.NotRead;
            books[7].Status = Status.Finished;
            books[7].UserRating = 4.9f;
            books[7].UserNote = "Chef d'oeuvre";
            books[8].Status = Status.Finished;
            books[8].UserRating = 4.2f;
            books[8].UserNote = "Des nouvelles très réussies dont Rapport Minoritaire";
            books[9].Status = Status.ToBeRead;
            books[9].Status = Status.Reading;

            borrowings.Add(new Borrowing
            {
                Id = "/borrowing/01", Owner = contacts[0],
                Book = LibraryMgr.GetBookById("/books/OL27328194M").Result,
                BorrowedAt = new DateTime(2023, 9, 7)
            });
            borrowings.Add(new Borrowing
            {
                Id = "/borrowing/02", Owner = contacts[1],
                Book = LibraryMgr.GetBookById("/books/OL27989051M").Result,
                BorrowedAt = new DateTime(2022, 7, 7),
                ReturnedAt = new DateTime(2023, 3, 1)
            });
            borrowings.Add(new Borrowing
            {
                Id = "/borrowing/03", Owner = contacts[1],
                Book = LibraryMgr.GetBookById("/books/OL35698073M").Result,
                BorrowedAt = new DateTime(2022, 7, 7),
                ReturnedAt = new DateTime(2022, 9, 1)
            });
            borrowings.Add(new Borrowing
            {
                Id = "/borrowing/04", Owner = contacts[1],
                Book = LibraryMgr.GetBookById("/books/OL35698083M").Result,
                BorrowedAt = new DateTime(2022, 7, 7),
                ReturnedAt = new DateTime(2023, 8, 30)
            });
		}

        public Task<Book> AddBook(Book book)
        {
            if(Books.Contains(book))
            {
                return Task.FromResult<Book>(null);
            }
            books.Add(book);
            return Task.FromResult(book);
        }

        public async Task<Book> AddBook(string id)
        {
            if(Books.SingleOrDefault(b => b.Id == id) != null)
            {
                return null;
            }
            var book = await LibraryMgr.GetBookById(id);
            books.Add(book);
            return book;
        }

        public async Task<Book> AddBookByIsbn(string isbn)
        {
            if(Books.SingleOrDefault(b => b.ISBN13 == isbn) != null)
            {
                return null;
            }
            var book = await LibraryMgr.GetBookByISBN(isbn);
            books.Add(book);
            return book;
        }

        public Task<bool> RemoveBook(Book book)
        {
            return Task.FromResult(books.Remove(book));
        }

        public Task<bool> RemoveBook(string id)
        {
            return Task.FromResult(books.RemoveAll(b => b.Id == id) >= 0);
        }

        public Task<bool> RemoveBookByIsbn(string isbn)
        {
            return Task.FromResult(books.RemoveAll(b => b.ISBN13 == isbn) >= 0);
        }

        public Task<bool> AddToFavorites(Book book)
        {
            if(Favorites.Contains(book))
            {
                return Task.FromResult(false);
            }
            var bookToAdd = Books.SingleOrDefault(b => b.Id == book.Id);
            if(bookToAdd == null)
            {
                return Task.FromResult(false);
            }
            favorites.Add(bookToAdd);
            return Task.FromResult(true);
        }

        public Task<bool> AddToFavorites(string bookId)
        {
            if(Favorites.SingleOrDefault(b => b.Id == bookId) != null)
            {
                return Task.FromResult(false);
            }
            var book = Books.SingleOrDefault(b => b.Id == bookId);
            if(book == null)
            {
                return Task.FromResult(false);
            }
            favorites.Add(book);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveFromFavorites(Book book)
        {
            return Task.FromResult(favorites.Remove(book));
        }

        public Task<bool> RemoveFromFavorites(string bookId)
        {
            return Task.FromResult(favorites.RemoveAll(b => b.Id == bookId) >= 0);
        }

        public Task<Contact> AddContact(Contact contact)
        {
            if(Contacts.Contains(contact))
            {
                return Task.FromResult<Contact>(null);
            }
            contacts.Add(contact);
            return Task.FromResult(contact);
        }

        public Task<bool> RemoveContact(Contact contact)
        {
            return Task.FromResult(contacts.Remove(contact));
        }

        public Task<bool> LendBook(Book book, Contact contact, DateTime? loanDate = null)
        {
            if(!Books.Contains(book))
                return Task.FromResult(false);
            if(!Contacts.Contains(contact))
                AddContact(contact);
            Loan loan = new Loan { Book = book, Loaner = contact, LoanedAt = loanDate.GetValueOrDefault(DateTime.Now) };
            if(Loans.Contains(loan))
                return Task.FromResult(false);
            loans.Add(loan);
            return Task.FromResult(true);
        }

        public Task<bool> GetBackBook(Book book, DateTime? returnedDate = null)
        {
            if(!Books.Contains(book))
                return Task.FromResult(false);
            var loan = loans.SingleOrDefault(l => l.Book == book);
            if(loan == null)
                return Task.FromResult(false);
            loan.ReturnedAt = returnedDate.GetValueOrDefault(DateTime.Now);
            return Task.FromResult(true);
        }

        public Task<bool> BorrowBook(Book book, Contact owner, DateTime? borrowedDate = null)
        {
            if(!Books.Contains(book))
                return Task.FromResult(false);
            if(!Contacts.Contains(owner))
                AddContact(owner);
            Borrowing borrow = new Borrowing { Book = book, Owner = owner, BorrowedAt = borrowedDate.GetValueOrDefault(DateTime.Now) };
            if(Borrowings.Contains(borrow))
                return Task.FromResult(false);
            borrowings.Add(borrow);
            return Task.FromResult(true);
        }

        public Task<bool> GiveBackBook(Book book, DateTime? returnedDate = null)
        {
            if(!Books.Contains(book))
                return Task.FromResult(false);
            var borrow = borrowings.SingleOrDefault(b => b.Book == book);
            if(borrow == null)
                return Task.FromResult(false);
            borrow.ReturnedAt = returnedDate.GetValueOrDefault(DateTime.Now);
            return Task.FromResult(true);
        }

        public Task<Book> GetBookById(string id)
        {
            return Task.FromResult(Books.SingleOrDefault(b => b.Id == id));
        }

        public Task<Book> GetBookByISBN(string isbn)
        {
            return Task.FromResult(Books.SingleOrDefault(b => b.ISBN13 == isbn));
        }

        public Task<Tuple<long, IEnumerable<Book>>> GetBooksByTitle(string title, int index, int count, string sort = "")
        {
            var foundBooks = Books.Where(b => b.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase));
            return OrderBooks(foundBooks, index, count, sort);
        }

        public Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthorId(string authorId, int index, int count, string sort = "")
        {
            var foundBooks = Books.Where(b => b.Authors.Exists(a => a.Id.Contains(authorId))
                                        || b.Works.Exists(w => w.Authors.Exists(a => a.Id.Contains(authorId))));
            return OrderBooks(books, index, count, sort);
        }

        public Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthor(string author, int index, int count, string sort = "")
        {
            var foundBooks = Books.Where(b => ContainsAuthorName(b, author));
            return OrderBooks(books, index, count, sort);
        }

        public IEnumerable<Author> Authors
        {
            get
            {
                var bookAuthors = Books.SelectMany(b => b.Authors);
                var workAuthors = Books.SelectMany(b => b.Works).SelectMany(w => w.Authors);
                return bookAuthors.Union(workAuthors).Distinct();
            }
        }

        public Task<Author> GetAuthorById(string id)
        {
            return Task.FromResult(Authors.SingleOrDefault(a => a.Id == id));
        }

        private Task<Tuple<long, IEnumerable<Author>>> OrderAuthors(IEnumerable<Author> authors, int index, int count, string sort = "")
        {
            switch(sort)
            {
                case "name":
                    authors = authors.OrderBy(a => a.Name);
                    break;
                case "name_reverse":
                    authors = authors.OrderByDescending(a => a.Name);
                    break;
            }
            return Task.FromResult(Tuple.Create((long)authors.Count(), authors.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Author>>> GetAuthorsByName(string substring, int index, int count, string sort = "")
        {
            var foundAuthors = Authors.Where(a => a.Name.Contains(substring, StringComparison.InvariantCultureIgnoreCase)
                                                  || a.AlternateNames.Exists(alt => alt.Contains(substring, StringComparison.InvariantCultureIgnoreCase)));
            return OrderAuthors(foundAuthors, index, count, sort);
        }

        public Task<Book> UpdateBook(Book updatedBook)
        {
            if(!books.Contains(updatedBook))
            {
                return Task.FromResult<Book>(null);
            }
            books.Remove(updatedBook);
            books.Add(updatedBook);
            return Task.FromResult(updatedBook);
        }

        private Task<Tuple<long, IEnumerable<Book>>> OrderBooks(IEnumerable<Book> books, int index, int count, string sort = "")
        {
            switch(sort)
            {
                case "title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "title_reverse":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "new":
                    books = books.OrderByDescending(b => b.PublishDate);
                    break;
                case "old":
                    books = books.OrderBy(b => b.PublishDate);
                    break;

            }
            return Task.FromResult(Tuple.Create(books.LongCount(), books.Skip(index*count).Take(count)));
        }

        private bool ContainsAuthorName(Book book, string name)
        {
            IEnumerable<Author> authors = new List<Author>();

            if(book.Authors != null && book.Authors.Count > 0)
            {
                authors = authors.Union(book.Authors);
            }
            if(book.Works != null)
            {
                var worksAuthors = book.Works.SelectMany(w => w.Authors).ToList();
                if(worksAuthors.Count > 0)
                    authors = authors.Union(worksAuthors);
            }
            foreach(var author in authors)
            {
                if(author.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                    || author.AlternateNames.Exists(alt => alt.Contains(name, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        }

        public Task<Tuple<long, IEnumerable<Book>>> GetBooksFromCollection(int index, int count, string sort = "")
        {
            return OrderBooks(Books, index, count, sort);
        }

        public Task<Tuple<long, IEnumerable<Loan>>> GetCurrentLoans(int index, int count)
        {
            var currentLoans = Loans.Where(l => !l.ReturnedAt.HasValue);
            return Task.FromResult(Tuple.Create(currentLoans.LongCount(), currentLoans.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Loan>>> GetPastLoans(int index, int count)
        {
            var currentLoans = Loans.Where(l => l.ReturnedAt.HasValue);
            return Task.FromResult(Tuple.Create(currentLoans.LongCount(), currentLoans.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Borrowing>>> GetCurrentBorrowings(int index, int count)
        {
            var currentBorrowings = Borrowings.Where(l => !l.ReturnedAt.HasValue);
            return Task.FromResult(Tuple.Create(currentBorrowings.LongCount(), currentBorrowings.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Borrowing>>> GetPastBorrowings(int index, int count)
        {
            var currentBorrowings = Borrowings.Where(l => l.ReturnedAt.HasValue);
            return Task.FromResult(Tuple.Create(currentBorrowings.LongCount(), currentBorrowings.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Contact>>> GetContacts(int index, int count)
        {
            return Task.FromResult(Tuple.Create(Contacts.LongCount(), Contacts.Skip(index*count).Take(count)));
        }

        public Task<Tuple<long, IEnumerable<Author>>> GetAuthorsFromCollection(int index, int count, string sort = "")
        {
            return OrderAuthors(Authors, index, count, sort);
        }
    }
}


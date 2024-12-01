using System;
using System.Collections.Generic;

interface IIterator<T>
{
    bool HasNext();
    T Next();
}

interface ICollection<T>
{
    IIterator<T> CreateIterator();
}

class Book
{
    public string Title { get; }

    public Book(string title)
    {
        Title = title;
    }

    public override string ToString()
    {
        return Title;
    }
}

class Library : ICollection<Book>
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public IIterator<Book> CreateIterator()
    {
        return new BookIterator(this);
    }

    int Count => books.Count;

    Book this[int index] => books[index];

    class BookIterator : IIterator<Book>
    {
        private readonly Library _library;
        private int _currentIndex = 0;

        public BookIterator(Library library)
        {
            _library = library;
        }

        public bool HasNext()
        {
            return _currentIndex < _library.Count;
        }

        public Book Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements to iterate.");
            }
            return _library[_currentIndex++];
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        library.AddBook(new Book("1kla$$"));
        library.AddBook(new Book("bob the bean"));
        library.AddBook(new Book("my fight"));

        IIterator<Book> iterator = library.CreateIterator();

        Console.WriteLine("Books in Library:");
        while (iterator.HasNext())
        {
            Book currentBook = iterator.Next();
            Console.WriteLine(currentBook);
        }
    }
}

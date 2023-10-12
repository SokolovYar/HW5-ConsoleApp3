using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public Book(string name, string author, int pages)
        {
            Name = name;
            Author = author;
            Pages = pages;
        }   
        public Book ()
        {
            Name = null;
            Author = null;
            Pages = 0;
        }
        public override string ToString()
        {
            return $"Book: \"{ Name ?? "NoName"}\", Author: {Author ?? "Unknown"}, volume: {Pages} pages";
        }
        public static bool operator == (Book obj1, Book obj2)
        {
            return obj1.Name == obj2.Name && obj1.Author == obj2.Author;
        }
        public static bool operator !=(Book obj1, Book obj2)
        {
            return obj1.Name != obj2.Name && obj1.Author != obj2.Author;
        }
    }

    internal class LibraryList
    {
        private Book[] _library;
        public LibraryList(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException("count can`t be less than zero!");
            _library = new Book[count];
            for (int i = 0; i< _library.Length; i++)
                _library[i] = new Book();
        }
        public void Print()
        {
            foreach (var it in _library)
                Console.WriteLine(it);
            Console.WriteLine();
        }
        public Book this[int index]
            {
            get { return _library[index]; }
            set { _library[index] = value; }
            }

        public static LibraryList operator + (LibraryList obj, Book book)
        {
            Book[] temp = new Book[obj._library.Length + 1];
            for (int i = 0; i < obj._library.Length; i++)
                temp[i] = obj._library[i];
            temp[obj._library.Length] = book;
            obj._library = temp;
            return obj;
        }

        public static LibraryList operator +(LibraryList obj, int count)
        {
            Book[] temp = new Book[obj._library.Length + count];
            int i = 0;
            for (; i < obj._library.Length; i++)
                temp[i] = obj._library[i];
            for (; i < obj._library.Length + count; i++)
                temp[i] = new Book();
            obj._library = temp;
            return obj;
        }

        public static LibraryList operator - (LibraryList obj, int count)
        {
            if (obj._library.Length - count < 1) throw new ArgumentOutOfRangeException("Library can`t consist of zero or less books");
            Book[] temp = new Book[obj._library.Length - count];
            for (int i = 0; i < obj._library.Length - count; i++)
                temp[i] = obj._library[i];
            obj._library = temp;
            return obj;
        }

        public bool Contain(Book book)
        {
            foreach (var it in _library)
                if (it == book) return true;
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryList MyWishList = new LibraryList(3);
            MyWishList[0] = new Book("Qt 5.10 Professional programming in C++", "Maks Shlee", 1052);
            MyWishList[1] = new Book("Electrical installation rules", "MPE", 572);
            MyWishList[2] = new Book("Fairytale about the golden fish", "A.S. Pushkin", 24);
            MyWishList.Print();

            MyWishList = MyWishList + 2;
            MyWishList.Print();
            MyWishList = MyWishList - 2;
            MyWishList.Print();
            Book MyBook = new Book("Azbuka", "MONU", 68);
            MyWishList = MyWishList + MyBook;
            MyWishList.Print();

            if (MyWishList.Contain(MyBook)) Console.WriteLine(MyBook + " is contain in the library");
        }
    }
}

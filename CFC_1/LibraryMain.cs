using System;
using Com.Wipro.Library.Dao;
using Com.Wipro.Library.Bean;
using Com.Wipro.Library.Util;

namespace Com.Wipro.Library.Service
{
    class LibraryMain
    {
        static void Main(string[] args)
        {
            LibraryDAO dao = new LibraryDAO();

            while (true)
            {
                Console.WriteLine("\n==== LIBRARY MANAGEMENT SYSTEM ====");
                Console.WriteLine("1. View Book");
                Console.WriteLine("2. Issue Book");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Book ID: ");
                        string bookID = Console.ReadLine()!;
                        dao.ViewBook(bookID);
                        break;

                    case "2":
                        Console.Write("Enter Book ID: ");
                        string bid = Console.ReadLine()!.Trim().ToUpper();

                        if (!dao.ValidateBook(bid))
                        {
                            Console.WriteLine("INVALID BOOK ID");
                            break;
                        }

                        if (!dao.CheckBookAvailability(bid))
                        {
                            Console.WriteLine("BOOK NOT AVAILABLE");
                            break;
                        }

                        Console.Write("Enter Member Name: ");
                        string name = Console.ReadLine()!;

                        BookIssueBean bean = new BookIssueBean
                        {
                            BookID = bid,
                            MemberName = name,
                            IssueDate = DateTime.Now
                        };

                        dao.IssueBook(bean);
                        Console.WriteLine("SUCCESS");
                        break;

                    case "3":
                        Console.WriteLine("Thank you!");
                        return;

                    default:
                        Console.WriteLine("INVALID OPTION");
                        break;
                }
            }
        }
    }
}

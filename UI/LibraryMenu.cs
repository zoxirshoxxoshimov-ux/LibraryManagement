using System.Collections.Generic;
using LibraryManagement.Common.Models;
using LibraryManagement.Interfaces;

namespace LibraryManagement.UI;

public class LibraryMenu
{
    private readonly ILibraryItemService _itemService;
    private readonly IMemberService _memberService;
    private readonly ILoanService _loanService;

    public LibraryMenu(
        ILibraryItemService itemService,
        IMemberService memberService,
        ILoanService loanService)
    {
        _itemService = itemService;
        _memberService = memberService;
        _loanService = loanService;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== KUTUBXONA ==========");
            Console.WriteLine("1. Material qo'shish");
            Console.WriteLine("2. Materialni ID bo'yicha ko'rish");
            Console.WriteLine("3. Barcha materiallar");
            Console.WriteLine("4. Nom bo'yicha qidirish");
            Console.WriteLine("5. Yil bo'yicha qidirish");
            Console.WriteLine("6. Materialni yangilash");
            Console.WriteLine("7. Materialni o'chirish");
            Console.WriteLine("8. A'zo qo'shish");
            Console.WriteLine("9. A'zoni ID bo'yicha ko'rish");
            Console.WriteLine("10. Barcha a'zolar");
            Console.WriteLine("11. A'zoni yangilash");
            Console.WriteLine("12. A'zoni o'chirish");
            Console.WriteLine("13. Material berish");
            Console.WriteLine("14. Materialni qaytarish");
            Console.WriteLine("15. Faol ijaralar");
            Console.WriteLine("16. A'zoning ijara tarixi");
            Console.WriteLine("17. Statistika");
            Console.WriteLine("0. Chiqish");
            Console.WriteLine("================================");

            int choice = ConsoleInput.ReadInt("Choose: ", 0, 17);

            switch (choice)
            {
                case 1:
                    AddItem();
                    break;

                case 2:
                    GetItem();
                    break;

                case 3:
                    GetAllItems();
                    break;

                case 4:
                    SearchByTitle();
                    break;

                case 5:
                    SearchByYear();
                    break;

                case 6:
                    UpdateItem();
                    break;

                case 7:
                    DeleteItem();
                    break;

                case 8:
                    AddMember();
                    break;

                case 9:
                    GetMember();
                    break;

                case 10:
                    GetAllMembers();
                    break;

                case 11:
                    UpdateMember();
                    break;

                case 12:
                    DeleteMember();
                    break;

                case 13:
                    BorrowItem();
                    break;

                case 14:
                    ReturnItem();
                    break;

                case 15:
                    GetActiveLoans();
                    break;

                case 16:
                    GetMemberLoans();
                    break;

                case 17:
                    ShowStatistics();
                    break;

                case 0:
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    private void AddItem()
    {
        Console.WriteLine("\n1. Book");
        Console.WriteLine("2. Magazine");
        Console.WriteLine("3. AudioBook");

        int type = ConsoleInput.ReadInt("Choose material type: ", 1, 3);

        int id = ConsoleInput.ReadInt("ID: ", 1, 100000);
        string title = ConsoleInput.ReadText("Title: ");

        int year = ConsoleInput.ReadInt(
            "Published year: ", 1450, DateTime.Now.Year);

        LibraryItem item = null;

        if (type == 1)
        {
            string author = ConsoleInput.ReadText("Author: ");
            int pages = ConsoleInput.ReadInt("Page count: ", 1, 100000);

            item = new Book(id, title, year, author, pages);
        }
        else if (type == 2)
        {
            int issue = ConsoleInput.ReadInt("Issue number: ", 1, 100000);

            item = new Magazine(id, title, year, issue);
        }
        else
        {
            string narrator = ConsoleInput.ReadText("Narrator: ");

            int duration = ConsoleInput.ReadInt(
                "Duration in minutes: ", 1, 100000);

            item = new AudioBook(id, title, year, narrator, duration);
        }

        ConsolePrinter.PrintResult(_itemService.AddItem(item));
    }

    private void GetItem()
    {
        int id = ConsoleInput.ReadInt("Material ID: ", 1, 100000);

        ConsolePrinter.Print(_itemService.GetItemById(id));
    }

    private void GetAllItems()
    {
        ConsolePrinter.PrintMany(_itemService.GetAllItems());
    }

    private void SearchByTitle()
    {
        string title = ConsoleInput.ReadText("Enter title: ");

        ConsolePrinter.PrintMany(_itemService.SearchItems(title));
    }

    private void SearchByYear()
    {
        int year = ConsoleInput.ReadInt(
            "Enter year: ", 1450, DateTime.Now.Year);

        ConsolePrinter.PrintMany(_itemService.SearchItems(year));
    }

    private void UpdateItem()
    {
        int id = ConsoleInput.ReadInt("Material ID: ", 1, 100000);
        string title = ConsoleInput.ReadText("New title: ");

        int year = ConsoleInput.ReadInt(
            "New published year: ", 1450, DateTime.Now.Year);

        ConsolePrinter.PrintResult(
            _itemService.UpdateItem(id, title, year));
    }

    private void DeleteItem()
    {
        int id = ConsoleInput.ReadInt("Material ID: ", 1, 100000);

        ConsolePrinter.PrintResult(_itemService.DeleteItem(id));
    }

    private void AddMember()
    {
        Console.WriteLine("\n1. Student");
        Console.WriteLine("2. Teacher");

        int type = ConsoleInput.ReadInt("Choose member type: ", 1, 2);

        int id = ConsoleInput.ReadInt("ID: ", 1, 100000);
        string name = ConsoleInput.ReadText("Full name: ");
        string phone = ConsoleInput.ReadText("Phone: ");

        Member member;

        if (type == 1)
        {
            string group = ConsoleInput.ReadText("Group: ");

            member = new StudentMember(id, name, phone, group);
        }
        else
        {
            string department = ConsoleInput.ReadText("Department: ");

            member = new TeacherMember(id, name, phone, department);
        }

        ConsolePrinter.PrintResult(_memberService.RegisterMember(member));
    }

    private void GetMember()
    {
        int id = ConsoleInput.ReadInt("Member ID: ", 1, 100000);

        ConsolePrinter.Print(_memberService.GetMemberById(id));
    }

    private void GetAllMembers()
    {
        ConsolePrinter.PrintMany(_memberService.GetAllMembers());
    }

    private void UpdateMember()
    {
        int id = ConsoleInput.ReadInt("Member ID: ", 1, 100000);
        string name = ConsoleInput.ReadText("New name: ");
        string phone = ConsoleInput.ReadText("New phone: ");

        ConsolePrinter.PrintResult(
            _memberService.UpdateMember(id, name, phone));
    }

    private void DeleteMember()
    {
        int id = ConsoleInput.ReadInt("Member ID: ", 1, 100000);

        ConsolePrinter.PrintResult(_memberService.DeleteMember(id));
    }

    private void BorrowItem()
    {
        int memberId = ConsoleInput.ReadInt("Member ID: ", 1, 100000);
        int itemId = ConsoleInput.ReadInt("Material ID: ", 1, 100000);

        ConsolePrinter.PrintResult(
            _loanService.BorrowItem(memberId, itemId));
    }

    private void ReturnItem()
    {
        int itemId = ConsoleInput.ReadInt("Material ID: ", 1, 100000);
        int days = ConsoleInput.ReadInt("Days kept: ", 1, 100000);

        ConsolePrinter.PrintResult(
            _loanService.ReturnItem(itemId, days));
    }

    private void GetActiveLoans()
    {
        ConsolePrinter.PrintMany(_loanService.GetActiveLoans());
    }

    private void GetMemberLoans()
    {
        int memberId = ConsoleInput.ReadInt("Member ID: ", 1, 100000);

        ConsolePrinter.PrintMany(_loanService.GetLoansByMember(memberId));
    }

    private void ShowStatistics()
    {
        int books = 0;
        int magazines = 0;
        int audioBooks = 0;
        int available = 0;
        int students = 0;
        int teachers = 0;

        List<string> authors = new();

        foreach (LibraryItem item in _itemService.GetAllItems())
        {
            if (item is Book)
            {
                books++;

                Book book = item as Book;

                if (!authors.Contains(book.Author))
                {
                    authors.Add(book.Author);
                }
            }

            if (item is Magazine)
                magazines++;

            if (item is AudioBook)
                audioBooks++;

            if (item.IsAvailable)
                available++;
        }

        foreach (Member member in _memberService.GetAllMembers())
        {
            if (member is StudentMember)
                students++;

            if (member is TeacherMember)
                teachers++;
        }

        Console.WriteLine("\n========== STATISTICS ==========");
        Console.WriteLine("Books: " + books);
        Console.WriteLine("Magazines: " + magazines);
        Console.WriteLine("AudioBooks: " + audioBooks);
        Console.WriteLine("Available materials: " + available);
        Console.WriteLine("Students: " + students);
        Console.WriteLine("Teachers: " + teachers);
        Console.WriteLine("Active loans: " +
            _loanService.GetActiveLoans().Length);
        Console.WriteLine("Total fines: " +
            _loanService.GetTotalFines());

        Console.WriteLine("\nAuthors:");

        foreach (string author in authors)
        {
            Console.WriteLine(author);
        }
    }
}

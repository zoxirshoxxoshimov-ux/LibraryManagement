using System;

namespace LibraryManagement.Common.Models;

public class Book : LibraryItem
{
    public string Author { get; }
    public int PageCount { get; }

    public override string TypeName => "Kitob";
    public override int MaxLoanDays => 14;

    public Book(int id, string title, int year, string author, int pageCount)
        : base(id, title, year)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Muallif bo'sh bo'lmasligi kerak.");

        if (pageCount <= 0)
            throw new ArgumentException("Betlar soni musbat bo'lishi kerak.");

        Author = author.Trim();
        PageCount = pageCount;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" | Muallif: {Author} | {PageCount} bet";
    }
}
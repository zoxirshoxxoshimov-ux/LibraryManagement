using System;
namespace  LibraryManagement.Common.Models;

public class Magazine : LibraryItem
{
    public int IssueNumber { get; }

    public override string TypeName => "Jurnal";
    public override int MaxLoanDays => 7;
    public override decimal DailyFine => 500;

    public Magazine(int id, string title, int year, int issueNumber)
        : base(id, title, year)
    {
        if (issueNumber <= 0)
            throw new ArgumentException("Son raqami musbat bo'lishi kerak.");

        IssueNumber = issueNumber;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" | Son: {IssueNumber}";
    }
}
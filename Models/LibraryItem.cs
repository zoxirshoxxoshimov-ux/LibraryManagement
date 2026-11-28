using System;
using LibraryManagement.Common;

namespace LibraryManagement.Common.Models;

public abstract class LibraryItem : IDisplayable
{
    public int Id { get; }
    public string Title { get; private set; }
    public int PublishedYear { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public abstract string TypeName { get; }
    public abstract int MaxLoanDays { get; }

    public virtual decimal DailyFine => 1000;

    protected LibraryItem(int id, string title, int publishedYear)
    {
        if (id <= 0)
            throw new ArgumentException("ID musbat bo'lishi kerak.");

        CheckValues(title, publishedYear);

        Id = id;
        Title = title.Trim();
        PublishedYear = publishedYear;
    }

    public virtual string GetSummary()
    {
        string status = IsAvailable ? "Mavjud" : "Ijarada";

        return $"[{TypeName}]: {Id} | {Title} | {PublishedYear} | {status}";
    }

    public void UpdateInfo(string title, int publishedYear)
    {
        CheckValues(title, publishedYear);

        SetTitle(title);
        SetPublishedYear(publishedYear);
    }

    public void MarkAsBorrowed()
    {
        IsAvailable = false;
    }

    public void MarkAsReturned()
    {
        IsAvailable = true;
    }

    private void SetTitle(string title)
    {
        Title = title.Trim();
    }

    private void SetPublishedYear(int publishedYear)
    {
        PublishedYear = publishedYear;
    }

    private static void CheckValues(string title, int year)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Nom bo'sh bo'lmasligi kerak.");

        if (year < 1450 || year > DateTime.Now.Year)
            throw new ArgumentException("Yil noto'g'ri.");
    }
}
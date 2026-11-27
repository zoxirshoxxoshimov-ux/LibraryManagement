using System;
namespace LibraryManagement.Common;

public abstract class LibraryItem
{
    public int Id {get;}
    public string Title {get; private set;}
    public int PublishedYear{get; private set;}
    public bool IsAvailable{get; private set;}
    public abstract string TypeName { get; }
    public abstract int MaxLoanDays { get; }
    public virtual decimal DailyFine { get; set; }


protected LibraryItem(int id, string title, int publishedYear)
    {
        this.Id = id;
        this.Title = title.Trim();
        this.PublishedYear = publishedYear;
    }
    public virtual string GetSummary()
    {
        string status = IsAvailable ? "Mavjud" : "Ijarada";
        return $"[{TypeName}]: {Id} | {Title} | {PublishedYear} | {status}";
    }
public void UpdatedInfo(string title, int publishedYear)
    {
       SetTitle(title);
       SetPublishedYear(publishedYear);
    }
public void MarkAsBorrowed() => this.IsAvailable = false;
public void MarkAsReturned() => this.IsAvailable = true;

private void SetTitle (string title)
    {
        if(!string.IsNullOrWhiteSpace(title))
        this.Title = title;
    }
private void SetPublishedYear(int publishedYear)
    {
        if(publishedYear > 0)
        this.PublishedYear = publishedYear;
    }
// protected LibraryItem(int id, string title, int publishedYear)
//     {
//         this.Id = id;
//         SetTitle(title);
//         SetPublishedYear(publishedYear);
//     }
private static void CheckValues(string title, int year)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Nom bo'sh bo'lmasligi kerak.");
        if (year < 1450 || year > DateTime.Now.Year)
            throw new ArgumentException("Yil 1450 dan joriy yilgacha bo'lishi kerak.");
    }
}
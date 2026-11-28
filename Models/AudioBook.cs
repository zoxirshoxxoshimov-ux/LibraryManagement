using System;

namespace LibraryManagement.Common.Models;

public class AudioBook : LibraryItem
{
    public string Narrator { get; }
    public int DurationMinutes { get; }

    public override string TypeName => "Audiokitob";
    public override int MaxLoanDays => 10;
    public override decimal DailyFine => 1500;

    public AudioBook(int id, string title, int year, string narrator, int durationMinutes)
        : base(id, title, year)
    {
        if (string.IsNullOrWhiteSpace(narrator))
            throw new ArgumentException("Ovoz egasi bo'sh bo'lmasligi kerak.");

        if (durationMinutes <= 0)
            throw new ArgumentException("Davomiylik musbat bo'lishi kerak.");

        Narrator = narrator.Trim();
        DurationMinutes = durationMinutes;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" | Ovoz: {Narrator} | {DurationMinutes} daqiqa";
    }
}
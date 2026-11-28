using System;
using LibraryManagement.Common;

namespace LibraryManagement.Common.Models;

public abstract class Member : IDisplayable
{
    public int Id { get; }
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public int ActiveLoanCount { get; private set; }

    public abstract string MemberType { get; }
    public abstract int BorrowLimit { get; }

    protected Member(int id, string fullName, string phoneNumber)
    {
        if (id <= 0)
            throw new ArgumentException("ID musbat bo'lishi kerak.");

        CheckValues(fullName, phoneNumber);

        Id = id;
        FullName = fullName.Trim();
        PhoneNumber = phoneNumber.Trim();
    }

    public bool CanBorrow()
    {
        return ActiveLoanCount < BorrowLimit;
    }

    public virtual decimal CalculateFine(int lateDays, decimal dailyFine)
    {
        if (lateDays <= 0)
            return 0;

        return lateDays * dailyFine;
    }

    public virtual string GetSummary()
    {
        return $"[{MemberType}] #{Id} | {FullName} | {PhoneNumber} | Ijaralar: {ActiveLoanCount}/{BorrowLimit}";
    }

    public void UpdateContact(string fullName, string phoneNumber)
    {
        CheckValues(fullName, phoneNumber);

        FullName = fullName.Trim();
        PhoneNumber = phoneNumber.Trim();
    }

    public void IncreaseActiveLoans()
    {
        ActiveLoanCount++;
    }

    public void DecreaseActiveLoans()
    {
        if (ActiveLoanCount > 0)
            ActiveLoanCount--;
    }

    private static void CheckValues(string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("F.I.Sh. bo'sh bo'lmasligi kerak.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Telefon bo'sh bo'lmasligi kerak.");
    }
}
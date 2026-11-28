using System;

namespace LibraryManagement.Common.Models;

public class StudentMember : Member
{
    public string GroupName { get; }

    public override string MemberType => "Talaba";
    public override int BorrowLimit => 3;

    public StudentMember(int id, string fullName, string phone, string groupName)
        : base(id, fullName, phone)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Guruh bo'sh bo'lmasligi kerak.");

        GroupName = groupName.Trim();
    }

    public override decimal CalculateFine(int lateDays, decimal dailyFine)
    {
        return base.CalculateFine(lateDays, dailyFine) / 2;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" | Guruh: {GroupName}";
    }
}
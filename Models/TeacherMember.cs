using System;

namespace LibraryManagement.Common.Models;

public class TeacherMember : Member
{
    public string Department { get; }

    public override string MemberType => "O'qituvchi";
    public override int BorrowLimit => 5;

    public TeacherMember(int id, string fullName, string phone, string department)
        : base(id, fullName, phone)
    {
        if (string.IsNullOrWhiteSpace(department))
            throw new ArgumentException("Kafedra bo'sh bo'lmasligi kerak.");

        Department = department.Trim();
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" | Kafedra: {Department}";
    }
}
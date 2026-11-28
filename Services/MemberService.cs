using System.Collections.Generic;
using LibraryManagement.Common;
using LibraryManagement.Common.Models;
using LibraryManagement.Interfaces;

namespace LibraryManagement.Services;

public class MemberService : IMemberService
{
    private readonly Dictionary<int, Member> _members = new();

    public OperationResult RegisterMember(Member member)
    {
        if (member == null)
            return new OperationResult(false, "Member is empty.");

        if (_members.ContainsKey(member.Id))
            return new OperationResult(false, "This ID already exists.");

        if (IsPhoneTaken(member.PhoneNumber, 0))
            return new OperationResult(false, "Phone number already exists.");

        _members.Add(member.Id, member);

        return new OperationResult(true, "Member registered successfully.");
    }

    public Member GetMemberById(int id)
    {
        if (_members.TryGetValue(id, out Member member))
            return member;

        return null;
    }

    public Member[] GetAllMembers()
    {
        Member[] result = new Member[_members.Count];

        _members.Values.CopyTo(result, 0);

        return result;
    }

    public OperationResult UpdateMember(int id, string newFullName, string newPhoneNumber)
    {
        Member member = GetMemberById(id);

        if (member == null)
            return new OperationResult(false, "Member not found.");

        if (IsPhoneTaken(newPhoneNumber, id))
            return new OperationResult(false, "Phone number already exists.");

        member.UpdateContact(newFullName, newPhoneNumber);

        return new OperationResult(true, "Member updated successfully.");
    }

    public OperationResult DeleteMember(int id)
    {
        Member member = GetMemberById(id);

        if (member == null)
            return new OperationResult(false, "Member not found.");

        if (member.ActiveLoanCount > 0)
            return new OperationResult(false, "Member has active loans.");

        _members.Remove(id);

        return new OperationResult(true, "Member deleted successfully.");
    }

    private bool IsPhoneTaken(string phone, int exceptMemberId)
    {
        foreach (Member member in _members.Values)
        {
            if (member.PhoneNumber == phone && member.Id != exceptMemberId)
            {
                return true;
            }
        }

        return false;
    }
}

using LibraryManagement.Common;
using LibraryManagement.Common.Models;

namespace LibraryManagement.Interfaces;

public interface IMemberService
{
    OperationResult RegisterMember(Member member);

    Member GetMemberById(int id);

    Member[] GetAllMembers();

    OperationResult UpdateMember(int id, string newFullName, string newPhoneNumber);

    OperationResult DeleteMember(int id);
}

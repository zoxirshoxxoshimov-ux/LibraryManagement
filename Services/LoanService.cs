using System.Collections.Generic;
using LibraryManagement.Common;
using LibraryManagement.Common.Models;
using LibraryManagement.Interfaces;

namespace LibraryManagement.Services;

public class LoanService : ILoanService
{
    private readonly ILibraryItemService _itemService;
    private readonly IMemberService _memberService;

    private readonly List<Loan> _loans = new();

    private int _nextLoanId = 1;

    public LoanService(
        ILibraryItemService itemService,
        IMemberService memberService)
    {
        _itemService = itemService;
        _memberService = memberService;
    }

    public OperationResult BorrowItem(int memberId, int itemId)
    {
        Member member = _memberService.GetMemberById(memberId);

        if (member == null)
            return new OperationResult(false, "Member not found.");

        LibraryItem item = _itemService.GetItemById(itemId);

        if (item == null)
            return new OperationResult(false, "Material not found.");

        if (!item.IsAvailable)
            return new OperationResult(false, "Material is already borrowed.");

        if (!member.CanBorrow())
            return new OperationResult(false, "Borrowing limit reached.");

        Loan loan = new Loan(_nextLoanId, itemId, memberId);

        _loans.Add(loan);
        _nextLoanId++;

        item.MarkAsBorrowed();
        member.IncreaseActiveLoans();

        return new OperationResult(true, "Material borrowed successfully.");
    }

    public OperationResult ReturnItem(int itemId, int daysKept)
    {
        if (daysKept <= 0)
            return new OperationResult(false, "Days must be positive.");

        Loan activeLoan = null;

        foreach (Loan loan in _loans)
        {
            if (loan.ItemId == itemId && !loan.IsReturned)
            {
                activeLoan = loan;
                break;
            }
        }

        if (activeLoan == null)
            return new OperationResult(false, "Active loan not found.");

        LibraryItem item = _itemService.GetItemById(itemId);

        Member member = _memberService.GetMemberById(activeLoan.MemberId);

        int lateDays = daysKept - item.MaxLoanDays;

        decimal fine = member.CalculateFine(lateDays, item.DailyFine);

        activeLoan.Close(daysKept, fine);

        item.MarkAsReturned();
        member.DecreaseActiveLoans();

        if (fine > 0)
        {
            return new OperationResult(true,
                $"Material returned. Fine: {fine} so'm.");
        }

        return new OperationResult(true, "Material returned. No fine.");
    }

    public Loan[] GetActiveLoans()
    {
        List<Loan> result = new();

        foreach (Loan loan in _loans)
        {
            if (!loan.IsReturned)
            {
                result.Add(loan);
            }
        }

        return result.ToArray();
    }

    public Loan[] GetLoansByMember(int memberId)
    {
        List<Loan> result = new();

        foreach (Loan loan in _loans)
        {
            if (loan.MemberId == memberId)
            {
                result.Add(loan);
            }
        }

        return result.ToArray();
    }

    public decimal GetTotalFines()
    {
        decimal total = 0;

        foreach (Loan loan in _loans)
        {
            total += loan.Fine;
        }

        return total;
    }
}

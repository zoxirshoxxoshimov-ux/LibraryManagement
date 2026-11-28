using System;
using LibraryManagement.Common;

namespace LibraryManagement.Common.Models;

public class Loan : IDisplayable
{
    public int Id { get; }
    public int ItemId { get; }
    public int MemberId { get; }
    public bool IsReturned { get; private set; }
    public int DaysKept { get; private set; }
    public decimal Fine { get; private set; }

    public Loan(int id, int itemId, int memberId)
    {
        Id = id;
        ItemId = itemId;
        MemberId = memberId;
    }

    public void Close(int daysKept, decimal fine)
    {
        DaysKept = daysKept;
        Fine = fine;
        IsReturned = true;
    }

    public string GetSummary()
    {
        if (!IsReturned)
            return $"Ijara #{Id} | Material: {ItemId} | A'zo: {MemberId} | Faol";

        return $"Ijara #{Id} | Material: {ItemId} | A'zo: {MemberId} | {DaysKept} kun | Jarima: {Fine} so'm";
    }
}
using LibraryManagement.Interfaces;
using LibraryManagement.Services;
using LibraryManagement.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;

ILibraryItemService itemService = new LibraryItemService();

IMemberService memberService = new MemberService();

ILoanService loanService = new LoanService(
    itemService,
    memberService
);

LibraryMenu menu = new LibraryMenu(
    itemService,
    memberService,
    loanService
);

menu.Run();

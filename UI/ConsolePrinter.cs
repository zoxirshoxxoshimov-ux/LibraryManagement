using LibraryManagement.Common;

namespace LibraryManagement.UI;

public class ConsolePrinter
{
    public static void Print(IDisplayable item)
    {
        if (item == null)
        {
            Console.WriteLine("Item not found.");
            return;
        }

        Console.WriteLine(item.GetSummary());
    }

    public static void PrintMany(IDisplayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        foreach (IDisplayable item in items)
        {
            Print(item);
        }
    }

    public static void PrintResult(OperationResult result)
    {
        if (result.IsSuccess)
        {
            Console.WriteLine("SUCCESS: " + result.Message);
        }
        else
        {
            Console.WriteLine("ERROR: " + result.Message);
        }
    }
}

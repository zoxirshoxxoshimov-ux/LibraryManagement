namespace LibraryManagement.UI;

public class ConsoleInput
{
    public static int ReadInt(string label, int min, int max)
    {
        while (true)
        {
            Console.Write(label);

            string input = Console.ReadLine();

            int number;

            if (int.TryParse(input, out number))
            {
                if (number >= min && number <= max)
                {
                    return number;
                }
            }

            Console.WriteLine($"Enter a number between {min} and {max}.");
        }
    }

    public static string ReadText(string label)
    {
        while (true)
        {
            Console.Write(label);

            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }
}

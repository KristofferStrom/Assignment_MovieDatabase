namespace Assignment_MovieDatabase.Console.ExtensionsMethods;

using System;


public static class UserInterfaceExtensions
{
    public static int GetInt(this string input, string fieldName)
    {
        
        int num;
        while (true)
        {
            if (int.TryParse(input, out num) == true)
                break;
            
            Console.WriteLine("Endast siffror");
            Console.Write(fieldName + ": ");

            input = Console.ReadLine()!;
        }

        return num;
    }

    public static int GetRequiredNumbers(this int input, string fieldName, List<int> requiredNumbers)
    {

        while (true)
        {
            if (requiredNumbers.Contains(input) || input == 0)
                break;

            Console.WriteLine("Fel inmatning");
            Console.Write(fieldName + ": ");

            input = Console.ReadLine()!.GetInt(fieldName);
        }

        return input;
    }
}

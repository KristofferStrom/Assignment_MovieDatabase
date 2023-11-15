namespace Assignment_MovieDatabase.Console.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserInterfaceService
{
    private readonly string _divider = CreateDivider(500);
    public void AddHeader(string title)
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine(_divider[..title.Length]);
    }

    public string GetFieldInput(string title)
    {
        Console.Write($"{title}: ");
        return Console.ReadLine()!;
    }

    public string GetSelectedOption(params string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine(_divider[..27]);
        Console.Write("Välj något av alternativen: ");

        return Console.ReadLine()!;
    }

    private static string CreateDivider(int length)
    {
        string divider = "";
        for (int i = 0; i < length; i++)
        {
            divider += "-";
        }

        return divider;
    }
}

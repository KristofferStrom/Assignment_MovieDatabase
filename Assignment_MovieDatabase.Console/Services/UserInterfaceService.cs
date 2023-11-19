namespace Assignment_MovieDatabase.Console.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserInterfaceService
{
    private readonly string _divider = CreateDivider(500);
    private int _dividerLength;
    private int _columnLength;
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

    public void AddTableHeader(int columnLength, params string[] titles)
    {
        _columnLength = columnLength;

        var sb = new StringBuilder();

        foreach (var title in titles)
        {
            sb.Append(CreateColumn(_columnLength, title));
        }
        _dividerLength = sb.Length -1;
        Console.WriteLine(sb);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(_divider[..(_dividerLength)]);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void AddTableRow(params string[] row)
    {
        var sb = new StringBuilder();

        foreach (var item in row)
        {
            sb.Append(CreateColumn(_columnLength, item));
        }
        Console.WriteLine(sb);
    }

    private string CreateColumn(int columnLength, string tableItem)
    {
        var whiteSpace = "                                    ";
        if (tableItem.Length >= columnLength)
        {
            return tableItem[..(columnLength - 4)] + "... ";
        }

        if (tableItem.Length < columnLength)
        {
            return tableItem + whiteSpace[..(columnLength - tableItem.Length)];
        }

        return tableItem;
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

    

    public void ReadKey()
    {
        Console.ReadKey();
    }

    internal void Print(string text)
    {
        Console.WriteLine(text);

        Console.ReadKey();
    }

    public void AddDivider()
    {
        Console.WriteLine(_divider[.._dividerLength]);
    }
}

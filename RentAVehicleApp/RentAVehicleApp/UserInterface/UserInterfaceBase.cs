using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAVehicleApp.UserInterface;

public class UserInterfaceBase
{
    protected string GetUserInput(string comment)
    {
        Console.WriteLine(comment);
        return Console.ReadLine()!;
    }

    protected void EmptyInputWarning(ref string? input, string inputName)
    {
        while (String.IsNullOrEmpty(input))
        {
            input = GetUserInput($"Brak wartości! Podaj {inputName}:");
        }
    }
}

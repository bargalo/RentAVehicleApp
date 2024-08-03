using RentAVehicleApp.Data;
using RentAVehicleApp.Entities;
using RentAVehicleApp.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RentAVehicleApp.UserInterface;

public class UserInterface : UserInterfaceBase
{
    private readonly IRepository<Rentier> _rentierRepository;

    public UserInterface(IRepository<Rentier> rentierRepository)
    {
        _rentierRepository = rentierRepository;
    }

    public void MainMenu()
    {
        Console.WriteLine("Witamy w Bazie Danych Wypożyczalni Samochodów Osobowych");
        Console.WriteLine();

        bool CloseApp = false;

        while (!CloseApp)
        {
            Console.WriteLine(
                "====================================\n" +
                "MENU:\n" +
                "1 - Wyświetl wszystkich klientów\n" +
                "2 - Dodaj nowego klienta\n" +
                "3 - Wyszukaj klienta po id\n" +
                "4 - Usuń klienta z bazy danych\n" +
                "5 - Zapisz zmiany i zakończ\n"
                 );

            var userInput = GetUserInput("Wybierz akcję (od 1 do 5) którą chcesz wykonać...");

            switch (userInput)
            {
                case "1":

                    var userChoiceToShowAll = GetUserInput("Aby wyświetlić wszystkich klientów wpisz \"all\".");

                    while (true)
                    {
                        if (userChoiceToShowAll == "all")
                        {
                            WriteAllToConsole(_rentierRepository);
                            break;
                        }
                        else
                        {
                            userChoiceToShowAll = GetUserInput("Złe polecenie! Spróbuj ponownie...\n");
                        }
                    }
                    break;

                case "2":

                    var userChoiceToAdd = GetUserInput("Aby dodać nowego klienta wpisz \"add\".");

                    while (true)
                    {
                        if (userChoiceToAdd == "add")
                        {
                            AddNewRentier();
                            break;
                        }
                        else
                        {
                            userChoiceToAdd = GetUserInput("Złe polecenie! Spróbuj ponownie...\n");
                        }
                    }
                    break;

                case "3":

                    FindEntityById(_rentierRepository);
                    break;

                case "4":

                    var userChoiceToRemove = GetUserInput("Wpisz \"delete\" aby usunąć klienta z bazy danych lub \"q\" aby wrócić do MENU");

                    while (true)
                    {
                        if (userChoiceToRemove == "delete")
                        {
                            RemoveEntity(_rentierRepository);
                            break;
                        }
                        else if (userChoiceToRemove == "q")
                        {
                            break;
                        }
                        else
                        {
                            userChoiceToRemove = GetUserInput("Złe polecenie! Spróbuj ponownie...\n");
                        }
                    }
                    break;
                case "5":

                    var userWantsToSave = GetUserInput("Aby potwierdzić zapis wybierz \"y\" lub anuluj wybierając \"n\"...");
                    while (true)
                    {
                        if (userWantsToSave == "y")
                        {
                            _rentierRepository.Save();
                            Console.WriteLine("Zmiany zostały zapisane.\n");
                            break;
                        }
                        else if (userWantsToSave == "n")
                        {
                            break;
                        }
                        else
                        {
                            userWantsToSave = GetUserInput("Złe polecenie! Spróbuj ponownie...\n");
                        }
                    }
                    CloseApp = true;
                    break;

                default:
                    
                    Console.WriteLine("Błędna komenda.\n");
                    continue;
            }
        }

        Console.WriteLine("\n\nNaciśnij klawisz aby wyjść.");

    }

    private void WriteAllToConsole<T>(IRepository<T> repository) where T : class, IEntity
    {
        Console.WriteLine();
        Console.WriteLine("=====Lista klientów...\n");
        var items = repository.GetAll();

        if (items.ToList().Count == 0)
        {
            Console.WriteLine("Żaden klient nie został jeszcze zapisany do bazy danych.\n");
        }
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    private void AddNewRentier()
    {
        var name = GetUserInput("Imię:");
        EmptyInputWarning(ref name, "Imię:");
        var surname = GetUserInput("Nazwisko:");
        EmptyInputWarning(ref surname, "Nazwisko:");

        var newRentier = new Rentier { Name = name, Surname = surname };
        _rentierRepository.Add(newRentier);
    }

    private T? FindEntityById<T>(IRepository<T> entityRepository) where T : class, IEntity
    {
        while (true)
        {
            var choiceID = GetUserInput($"Podaj ID klienta {typeof(T).Name} którego chcesz znaleźć lub wybierz \"q\" aby wrócić do MENU");
            int choiceIDValue;
            var isParsed = int.TryParse(choiceID, out choiceIDValue);
            if (!isParsed && choiceID != "q")
            {
                Console.WriteLine("ID musi być liczbą!\n");
            }
            else if (choiceID == "q")
            {
                return null;
            }
            else
            {
                var entity = entityRepository.GetById(choiceIDValue);

                if (entity != null)
                {
                    Console.WriteLine();
                    Console.WriteLine($"KLIENT O PODANYM ID TO ===> {entity}\n");

                }
                else if (entity == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Klient o podanym ID nie istnieje w bazie danych.\n");

                }
                return entity;
            }
        }
    }

    private void RemoveEntity<T>(IRepository<T> entityRepository) where T : class, IEntity
    {
        var entityFound = FindEntityById(entityRepository);
        if (entityFound != null)
        {
            while (true)
            {
                Console.WriteLine($"Czy napewno chcesz usunąć {entityFound} ?");
                var choice = GetUserInput("Wybierz \"y\" aby potwierdzić lub \"n\" aby wrócić do MENU.");
                if (choice == "y")
                {
                    entityRepository.Remove(entityFound);
                    break;
                }
                else if (choice == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wybierz \"y\" lub \"n\":");
                }
            }
        }
    }
}

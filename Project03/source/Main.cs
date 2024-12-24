#pragma warning disable CA1303, S4055, S1227, S134, S3776, S1151, S1192, S109, S1541

#region
using System.Globalization;
#endregion

const int PetCapacity = 8;

string[][] pets =
[
    [
        "D1",
        "Dog",
        "Lola",
        "2",
        "Medium sized cream colored female golden retriever weighing about 65 pounds. Housebroken.",
        "Loves to have her belly rubbed and likes to chase her tail. Gives lots of kisses.",
    ],
    [
        "D2",
        "Dog",
        "Loki",
        "9",
        "Large reddish-brown male golden retriever weighing about 85 pounds. Housebroken.",
        "Loves to have his ears rubbed when he greets you at the door, or at any time!",
    ],
    [
        "C3",
        "Cat",
        "Puss",
        "1",
        "Small white female weighing about 8 pounds. Litter box trained.",
        "Friendly",
    ],
    [
        "C4",
        "Cat",
        "Shadow",
        "3",
        "Huge dark male with gray highlights. Strong claws.",
        "Lurking in the shadows, a hunter.",
    ],
    [],
    [],
    [],
    [],
];

string menuSelection = "";
do
{
    Console.Clear();
    PrintMenu();
    string? readResult = Console.ReadLine();

    if (readResult is not null)
    {
        menuSelection = readResult.ToUpperInvariant();
    }

    Console.Clear();

    switch (menuSelection)
    {
        case "1":
        {
            // List pets
            foreach (string[] pet in pets)
            {
                PrintPet(pet);
            }

            Console.WriteLine("Press the Enter key to continue...");
            break;
        }
        case "2":
        {
            // Check if we are full
            int petCount = pets.Count(static pet => pet.Length == 6);

            Console.WriteLine(
                $"We currently have {petCount} pets that need homes. "
              + $"We can manage {PetCapacity - petCount} more."
            );

            if (petCount >= PetCapacity)
            {
                Console.WriteLine($"We cannot have more than {PetCapacity} pets.");
                break;
            }

            do
            {
                Console.Write("\nDo you want to surrender a pet? (y/n): ");

                string? readPet = Console.ReadLine();

                if (readPet is not "y")
                {
                    break;
                }

                // Add a new pet
                for (int i = 0; i < pets.Length; i++)
                {
                    if (pets[i].Length != 6)
                    {
                        pets[i] = GetPet(petCount);
                        break;
                    }
                }

                petCount = pets.Count(static pet => pet.Length == 6);
            } while (petCount < PetCapacity);

            Console.WriteLine("Press the Enter key to continue...");
            break;
        }
        case "3":
        {
            Console.Write("Enter pet's ID you want to edit (without '#'): ");
            string? readPet = Console.ReadLine();

            if (readPet is null)
            {
                break;
            }

            for (int i = 0; i < pets.Length; i++)
            {
                if (pets[i].Length == 6 && pets[i][0]
                    .Equals(
                        value: readPet.ToUpperInvariant(),
                        comparisonType: StringComparison.Ordinal
                    ))
                {
                    Console.WriteLine();
                    PrintPet(pets[i]);
                    Console.WriteLine($"You are editing '#{pets[i][0]} {pets[i][2]}'...");
                    pets[i] = GetPet(i - 1);
                    break;
                }
            }

            Console.WriteLine("Press the Enter key to continue...");
            break;
        }
        case "4":
        {
            // List cats
            foreach (string[] pet in pets.Where(static pet => pet is [_, "Cat", _, _, _, _]))
            {
                PrintPet(pet);
            }

            Console.WriteLine("Press the Enter key to continue...");
            break;
        }
        case "5":
        {
            // List dogs
            foreach (string[] pet in pets.Where(static pet => pet is [_, "Dog", _, _, _, _]))
            {
                PrintPet(pet);
            }

            Console.WriteLine("Press the Enter key to continue...");
            break;
        }
        default:
        {
            Console.WriteLine("Please enter a valid menu selection.");
            break;
        }
    }

    _ = Console.ReadLine();
} while (menuSelection != "EXIT");

return;

static void PrintMenu()
{
    Console.WriteLine("[Welcome to the Patika Pet Club]");
    Console.WriteLine();
    Console.WriteLine("Your options are:");
    Console.WriteLine("1. List all of our current pets");
    Console.WriteLine("2. Add a new pet to Patika club");
    Console.WriteLine("3. Edit a pet in Patika club");
    Console.WriteLine("4. Display all cats in Patika club");
    Console.WriteLine("5. Display all dogs in Patika club");
    Console.WriteLine();
    Console.Write("Enter your selection number (or type 'exit' to exit): ");
}

static void PrintPet(IReadOnlyList<string> pet)
{
    if (pet.Count != 6)
    {
        return;
    }

    Console.WriteLine($"ID        : #{pet[0]}");
    Console.WriteLine($"Species   : {pet[1]}");
    Console.WriteLine($"Name      : {pet[2]}");
    Console.WriteLine($"Age       : {pet[3]}");
    Console.WriteLine($"Physique  : {pet[4]}");
    Console.WriteLine($"Character : {pet[5]}");
    Console.WriteLine();
}

#pragma warning disable CA1308, S4040, S138
static string[] GetPet(int petCount)
{
    string[] pet = ["?", "?", "?", "?", "?", "?"];

    // Get pet's species
    bool validEntry = false;
    do
    {
        Console.Write("Enter 'dog' or 'cat' to begin: ");
        string? readResult = Console.ReadLine();

        if (readResult?.ToUpperInvariant() is not ("DOG" or "CAT"))
        {
            continue;
        }
        string species = readResult.ToUpperInvariant();

        pet[0]     = species[0] + (petCount + 1).ToString(CultureInfo.InvariantCulture);
        pet[1]     = species[0] + species[1..].ToLowerInvariant();
        validEntry = true;
    } while (!validEntry);

    // Get pet's name
    validEntry = false;
    do
    {
        Console.Write("Enter a nickname for the pet: ");
        string? readResult = Console.ReadLine();

        if (readResult is null)
        {
            continue;
        }
        string name = readResult.ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(name))
        {
            pet[2] = "?";
        }
        else if (name.Length < 2)
        {
            pet[2] = name;
        }
        else
        {
            pet[2]     = name[0] + name[1..].ToLowerInvariant();
            validEntry = true;
        }
    } while (!validEntry);

    // Get pet's age
    validEntry = false;
    do
    {
        Console.Write("Enter the pet's age ('?' if unknown): ");
        string? readResult = Console.ReadLine();

        switch (readResult)
        {
            case null: continue;
            case "?":
                pet[3]     = "?";
                validEntry = true;
                break;
            default:
            {
                validEntry = int.TryParse(
                    s: readResult,
                    provider: CultureInfo.InvariantCulture,
                    result: out _
                );

                if (validEntry)
                {
                    pet[3] = readResult;
                }
                break;
            }
        }
    } while (!validEntry);

    // Get pet's physique
    validEntry = false;
    do
    {
        Console.Write("Enter pet's physical description (size, color, gender, weight): ");
        string? readResult = Console.ReadLine();

        if (readResult is null)
        {
            continue;
        }

        if (string.IsNullOrWhiteSpace(readResult))
        {
            continue;
        }

        pet[4]     = readResult;
        validEntry = true;
    } while (!validEntry);

    // Get pet's character
    validEntry = false;
    do
    {
        Console.Write("Enter pet's character description (likes or dislikes, tricks, energy): ");
        string? readResult = Console.ReadLine();

        if (readResult is null)
        {
            continue;
        }

        if (string.IsNullOrWhiteSpace(readResult))
        {
            continue;
        }

        pet[5]     = readResult;
        validEntry = true;
    } while (!validEntry);

    return pet;
}
#pragma warning restore CA1308, S4040, S138
#pragma warning restore CA1303, S4055, S1227, S134, S3776, S1151, S1192, S109, S1541

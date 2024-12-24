#pragma warning disable CA5394, S134, S3776
const int BookCount = 500;
const int MinPage   = 100;
const int MaxPage   = 800;
const int MinRead   = 25;
const int MaxRead   = 250;
const int DayCount = 365;

Random random = new();

// (PageNumber, KnowledgeValue)
List<(int, float)> library = [];

for (int _ = 0; _ < BookCount; _++)
{
    library.Add((random.Next(minValue: MinPage, maxValue: MaxPage), random.NextSingle()));
}

// Name, (Speed, BookValue, PagesToRead, PagesRead, BooksRead, TotalRead, TotalKnowledge)
Dictionary<string, (int, float, int, int, int, int, float)> people = new()
{
    { "Emre", (random.Next(minValue: MinRead, maxValue: MaxRead), 0, 0, 0, 0, 0, 0) },
    { "Rauf", (random.Next(minValue: MinRead, maxValue: MaxRead), 0, 0, 0, 0, 0, 0) },
    { "Mert", (random.Next(minValue: MinRead, maxValue: MaxRead), 0, 0, 0, 0, 0, 0) },
    { "Ufuk", (random.Next(minValue: MinRead, maxValue: MaxRead), 0, 0, 0, 0, 0, 0) },
};

foreach (KeyValuePair<string, (int, float, int, int, int, int, float)> person in people)
{
    Console.WriteLine($"Name: {person.Key} | Speed: {person.Value.Item1}");
}
Console.WriteLine();

List<string> pKeys = [..people.Keys];

for (int dayCount = 0; dayCount < DayCount; dayCount++)
{
    // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
    foreach (string p in pKeys)
    {
        // Speed, BookValue, PagesToRead, PagesRead, BooksRead, TotalRead, TotalKnowledge
        (int, float, int, int, int, int, float) person = people[p];

        // If there are no pages to read
        if (person.Item3 == 0)
        {
            // If book was read
            if (person.Item4 != 0)
            {
                // Increase books read
                person.Item5++;

                // Increase total pages read
                person.Item6 += person.Item4;

                // Increase total knowledge gained with respect to value of the book
                person.Item7 += person.Item4 * person.Item2;

                // Reset pages read
                person.Item4 = 0;

                // Reset knowledge value
                person.Item2 = 0;
            }

            // If library has books
            if (library.Count > 0)
            {
                // Find new book to read
                (int, float) book = library[^1];
                library.RemoveAt(library.Count - 1);

                // Add book's knowledge value
                person.Item2 = book.Item2;

                // Add book's pages to read
                person.Item3 = book.Item1;
            }
        }
        else
        {
            // If today person can finish the book
            if (person.Item3 - person.Item1 <= 0)
            {
                // Add last pages to pages read
                person.Item4 += person.Item3;

                // Reset the pages to read
                person.Item3 = 0;
            }
            else
            {
                // Read the book
                person.Item3 -= person.Item1;

                // Add pages to pages read
                person.Item4 += person.Item1;
            }
        }

        // Update the person
        people[p] = person;
    }

    Console.WriteLine($"[DAY: {dayCount + 1}]");
    foreach (KeyValuePair<string, (int, float, int, int, int, int, float)> person in people)
    {
        Console.Write($"Name: {person.Key} | Current Pages: {person.Value.Item4:N0}    ");
        Console.Write($"\tTotal Books: {person.Value.Item5:N0}    ");
        Console.Write($"\tTotal Pages: {person.Value.Item6:N0}    ");
        Console.Write($"\tTotal Knowledge: {person.Value.Item7:N0}\n");
    }
    Console.WriteLine();
}
#pragma warning restore CA5394, S134, S3776

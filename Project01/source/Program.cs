Dictionary<string, int> coursesAndCredits = new()
{
    { "English", 4 }, { "Calculus", 6 }, { "Physics", 6 }, { "Intro to CS", 8 },
};

Dictionary<string, int[]> studentsAndGrades = new()
{
    { "Emre", [94, 83, 91, 76] },
    { "Rauf", [62, 83, 82, 78] },
    { "Mert", [93, 54, 51, 69] },
    { "Veli", [72, 43, 89, 41] },
    { "Ufuk", [26, 72, 33, 48] },
};

int totalCredits = coursesAndCredits.Values.Sum();

foreach ((string student, int[] grades) in studentsAndGrades)
{
    Console.WriteLine($"Transcript for '{student}'");

    var results = coursesAndCredits.Keys.Zip(
            second: grades,
            resultSelector: (course, grade) => new
            {
                Course = course,
                Grade  = grade,
                Credit = coursesAndCredits[course],
                Score  = grade * coursesAndCredits[course],
            }
        )
        .ToList();

    foreach (var result in results)
    {
        Console.Write($"\tCourse: {result.Course},\tCredit: {result.Credit}\t|\t");
        Console.Write($"Grade: {result.Grade},\tLetter: {ToLetter(result.Grade)}\n");
    }

    decimal totalScore = results.Sum(static x => x.Score);
    decimal percentage = totalScore / totalCredits;
    Console.WriteLine($"Percentage: {percentage:F2},\tGPA: {ToGpa(percentage):F2}");

    Console.WriteLine();
}

return;

#pragma warning disable S1541
static string ToLetter(decimal percentage)
{
    const decimal MaxAa = 100M;
    const decimal MinAa = 90M;
    const decimal MaxBa = 90M;
    const decimal MinBa = 85M;
    const decimal MaxBb = 85M;
    const decimal MinBb = 80M;
    const decimal MaxCb = 80M;
    const decimal MinCb = 75M;
    const decimal MaxCc = 75M;
    const decimal MinCc = 70M;
    const decimal MaxDc = 70M;
    const decimal MinDc = 65M;
    const decimal MaxDd = 65M;
    const decimal MinDd = 60M;
    const decimal MaxFd = 60M;
    const decimal MinFd = 50M;
    const decimal MaxFf = 50M;
    const decimal MinFf = 0M;

    return percentage switch
    {
        >= MinAa and <= MaxAa => "AA",
        >= MinBa and <= MaxBa => "BA",
        >= MinBb and <= MaxBb => "BB",
        >= MinCb and <= MaxCb => "CB",
        >= MinCc and <= MaxCc => "CC",
        >= MinDc and <= MaxDc => "DC",
        >= MinDd and <= MaxDd => "DD",
        >= MinFd and <= MaxFd => "FD",
        >= MinFf and <= MaxFf => "FF",
        _ => throw new ArgumentOutOfRangeException(
            paramName: nameof(percentage),
            actualValue: percentage,
            message: "Percentage must be between 0 and 100."
        ),
    };
}
#pragma warning restore S1541

static decimal ToGpa(decimal percentage)
{
    const int Base  = 100;
    const int Scale = 4;

    return percentage is < 0 or > Base ? throw new ArgumentOutOfRangeException(
        paramName: nameof(percentage),
        actualValue: percentage,
        message: $"Percentage must be between 0 and {Base}."
    ) : percentage / Base * Scale;
}

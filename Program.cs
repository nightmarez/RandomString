Console.WriteLine("Generate random alphanumerical strings with special characters:");

for (int i = 0; i < 5; ++i)
{
    Console.WriteLine(RandomString.Generate(10,
        RandomString.RandomStringType.Alpha |
        RandomString.RandomStringType.CaseSensitive |
        RandomString.RandomStringType.Numbers |
        RandomString.RandomStringType.Special, true));
}

Console.WriteLine();
Console.WriteLine("Generate random numerical strings:");

for (int i = 0; i < 5; ++i)
{
    Console.WriteLine(RandomString.Generate(10, RandomString.RandomStringType.Numbers, true));
}

Console.WriteLine();
Console.WriteLine("Generate unique random strings from limited characters:");

for (int i = 0; i < 8; ++i)
{
    Console.WriteLine(RandomString.Generate(3, "12", true));
}

Console.WriteLine();
Console.WriteLine("Catch exception when can't more generate unique string:");
RandomString.ResetUniqueCache();

try
{
    for (int i = 0; i < 9; ++i)
    {
        Console.WriteLine(RandomString.Generate(3, "12", true));
    }
}
catch (RandomString.RandomStringException e)
{
    Console.WriteLine($"Exception: {e.Message}");
}

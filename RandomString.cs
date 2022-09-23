using System.Text;

public static class RandomString
{
    [Flags]
    public enum RandomStringType
    {
        Numbers = 1,
        Alpha = 2,
        Special = 4,
        CaseSensitive = 8
    }

    public sealed class RandomStringException : ApplicationException
    {
        public RandomStringException(string message)
            : base(message)
        { }
    }

    private static Random _rnd;

    private static Random Rnd => _rnd ??= new Random();

    private static HashSet<string> _cache;

    private static HashSet<string> Cache => _cache ??= new HashSet<string>();

    public static void ResetUniqueCache()
    {
        Cache.Clear();
    }

    private static string GenerateInner(int length, IEnumerable<char> characterSet)
    {
        char[] set = characterSet.ToArray();

        return Enumerable.Range(0, length).Aggregate(new StringBuilder(length), (acc, _) =>
        {
            char c = set[Rnd.Next(set.Length)];
            acc.Append(c);
            return acc;
        }).ToString();
    }

    public static string Generate(int length, IEnumerable<char> characterSet, bool unique = false)
    {
        var characterSetList = characterSet.ToList();

        if (!unique)
        {
            return GenerateInner(length, characterSetList);
        }

        const int maxAttemptsCount = 100;

        for (int i = 0; i < maxAttemptsCount; ++i)
        {
            string result = GenerateInner(length, characterSetList);

            if (!Cache.Contains(result))
            {
                Cache.Add(result);
                return result;
            }
        }

        throw new RandomStringException("Too many attempts to find unique string.");
    }

    public static string Generate(int length, string characterSet, bool unique = false)
    {
        return Generate(length, characterSet.ToCharArray(), unique);
    }

    public static string Generate(int length, RandomStringType stringType, bool unique = false)
    {
        string characterSet = string.Empty;

        if (stringType.HasFlag(RandomStringType.Numbers))
        {
            characterSet += "0123456789";
        }

        if (stringType.HasFlag(RandomStringType.Alpha))
        {
            characterSet += "abcdefghijklmnopqrstuvwxyz";

            if (stringType.HasFlag(RandomStringType.CaseSensitive))
            {
                characterSet += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
        }

        if (stringType.HasFlag(RandomStringType.Special))
        {
            characterSet += "!@#$%^&*():;\"/?\\,.";
        }

        return Generate(length, characterSet, unique);
    }
}

using LanguageExt;
using System;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] _)
        {
            Console.WriteLine("Press key to start");
            Console.ReadKey();

            RunIssue().Wait();

            Console.WriteLine("Press key to terminate");
            Console.ReadKey();
        }

        static Task RunIssue()
        {
            return ValidateAsync();
        }

        static Task<Either<int, string>> ValidateAsync()
        {
            var result =

                from loaded in FindNameAsync("zzz").ToEitherAsync(()
                    => -1)

                from transformed in VerifyName(loaded).ToEitherAsync(()
                    => -2)

                select transformed;

            return result;
        }

        static Task<Option<string>> FindNameAsync(string name)
        {
            var names = GetNames()
                .Where(n => n == name);

            var firstFound = names.FirstOrDefaultAsync();

            var result = firstFound.Map(Optional);

            return result;
        }

        static IQueryable<string> GetNames()
        {
            // emulate EF DBSet

            return new[]
            {
                "aaa",
                "bbb",
                "ccc",

            }.AsQueryable();
        }

        static Option<string> VerifyName(string name)
        {
            // just apply some dummy test

            return (name.Length > 0)
                ? Some(name)
                : None;
        }
    }

    public static class EFCoreDummyExtensions
    {
        public static Task<string> FirstOrDefaultAsync(this IQueryable<string> items)
        {
            // emulate EF extension

            string value = items.FirstOrDefault();

            return Task.FromResult(value);
        }
    }
}

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

        static Task<Either<int, User>> ValidateAsync()
        {
            // pretend `int` stands for some custom error type

            var result =

                from loaded in FindUserAsync("zzz").ToEitherAsync(()
                    => -1)

                from transformed in VerifyUser(loaded).ToEitherAsync(()
                    => -2)

                select transformed;

            return result;
        }

        static Task<Option<User>> FindUserAsync(string name)
        {
            var users = GetUsers()
                .Where(n => (string)n == name);

            var firstFound = users.FirstOrDefaultAsync();

            var result = firstFound.Map(Optional);

            return result;
        }

        static IQueryable<User> GetUsers()
        {
            // emulate EF DBSet

            var names = new[]
            {
                "aaa",
                "bbb",
                "ccc",

            };

            var users = names
                .Select(n => new User(n));

            return users
                .AsQueryable();
        }

        static Option<User> VerifyUser(User user)
        {
            string name = (string)user;

            // just apply some dummy test

            return (name.Length > 0)
                ? Some(user)
                : None;
        }
    }

    public class User : NewType<User, string>
    {
        public User(string name) : base(name)
        { }
    }

    public static class EFCoreDummyExtensions
    {
        public static Task<User> FirstOrDefaultAsync(this IQueryable<User> items)
        {
            // emulate EF extension

            var value = items.FirstOrDefault();

            return Task.FromResult(value);
        }
    }
}

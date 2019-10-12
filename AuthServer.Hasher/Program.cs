using IdentityServer4.Models;
using System;

namespace Hasher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter secret or password:");
            var plaintext = Console.ReadLine();
            var hashed = plaintext.Sha256();
            Console.WriteLine("Hash:");
            Console.WriteLine(hashed);
            Console.ReadKey();
        }
    }
}

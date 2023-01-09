using System;

namespace Stage0 // Note: actual namespace depends on the project name.
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0942();
            Welcome1073();
            Console.ReadKey();
        }
        static partial void Welcome1073();
        private static void Welcome0942()
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console appliction", name);
        }
    }
}
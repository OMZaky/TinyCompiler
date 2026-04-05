using System;
using System.Collections.Generic;
using System.IO; // Required for reading files

namespace TinyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
           
            // If the user types "dotnet run mycode.tiny", it uses "mycode.tiny".
            // If they just type "dotnet run", it defaults to "Sample.tiny".
            string filePath = args.Length > 0 ? args[0] : "Sample.tiny";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[Error] Could not find the file: {filePath}");
                Console.WriteLine("Make sure the file is in the same directory as your project.");
                return;
            }

            Console.WriteLine($"Reading source code from: {filePath}...\n");
            string sourceCode = File.ReadAllText(filePath);

            Console.WriteLine("Starting Lexical Analysis...\n");

            Scanner scanner = new Scanner();
            List<Token> tokenStream = scanner.Tokenize(sourceCode);

            Console.WriteLine(string.Format("{0,-9} | {1,-20} | {2}", "LINE", "TOKEN TYPE", "LEXEME"));
            Console.WriteLine(new string('-', 55));

            foreach (Token token in tokenStream)
            {
                Console.WriteLine(token.ToString());
            }

            Console.WriteLine("\nTask 1 Lexical Analysis Complete.");
        }
    }
}
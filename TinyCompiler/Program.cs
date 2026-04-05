using System;
using System.Collections.Generic;

namespace TinyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            // The factorial sample program from the project description
            string sourceCode = @"
            /* Sample program in Tiny language computes factorial*/
            int main()
            {
                int x;
                read x; /*input an integer*/
                if x > 0 then /*don't compute if x <= 0 */
                    int fact := 1;
                    repeat
                        fact := fact * x;
                        x := x - 1;
                    until x = 0
                    write fact; /*output factorial of x*/
                end
                return 0;
            }";

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
            Console.ReadLine();
        }
    }
}
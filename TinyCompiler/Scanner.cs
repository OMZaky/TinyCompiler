using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TinyCompiler
{
    public class Scanner
    {
        private readonly Dictionary<TokenType, Regex> _regexRules = new Dictionary<TokenType, Regex>
        {
            { TokenType.Number, new Regex(@"^[0-9]+(\.[0-9]+)?", RegexOptions.Compiled) },
            { TokenType.String, new Regex(@"^""[^""]*""", RegexOptions.Compiled) },
            { TokenType.Comment, new Regex(@"^/\*[\s\S]*?\*/", RegexOptions.Compiled) },
            
            // Keywords MUST be checked before Identifiers
            { TokenType.ReservedKeyword, new Regex(@"^(int|float|string|read|write|repeat|until|if|elseif|else|then|return|endl|main)\b", RegexOptions.Compiled) },   
            { TokenType.Identifier, new Regex(@"^[a-zA-Z][a-zA-Z0-9]*", RegexOptions.Compiled) },
            
            { TokenType.AssignmentOp, new Regex(@"^:=", RegexOptions.Compiled) },
            { TokenType.ConditionOp, new Regex(@"^(<>|<=|>=|<|>|=)", RegexOptions.Compiled) },
            { TokenType.ArithmeticOp, new Regex(@"^[\+\-\*/]", RegexOptions.Compiled) },
            { TokenType.BooleanOp, new Regex(@"^(&&|\|\|)", RegexOptions.Compiled) },
            { TokenType.Delimiter, new Regex(@"^[;,{}()]", RegexOptions.Compiled) }
        };

        public List<Token> Tokenize(string sourceCode)
        {
            List<Token> tokens = new List<Token>();
            int position = 0;
            int currentLine = 1;

            while (position < sourceCode.Length)
            {
                string remainingCode = sourceCode.Substring(position);

                // Handle Whitespace and track newlines
                if (char.IsWhiteSpace(remainingCode[0]))
                {
                    if (remainingCode[0] == '\n')
                    {
                        currentLine++;
                    }
                    position++;
                    continue;
                }

                bool matchFound = false;

                foreach (var rule in _regexRules)
                {
                    Match match = rule.Value.Match(remainingCode);

                    if (match.Success)
                    {
                        string lexeme = match.Value;
                        TokenType type = rule.Key;

                        if (type != TokenType.Comment)
                        {
                            tokens.Add(new Token { Type = type, Lexeme = lexeme, LineNumber = currentLine });
                        }

                        // If a token (like a multi-line comment) spans multiple lines, update currentLine
                        int newLinesInMatch = lexeme.Split('\n').Length - 1;
                        currentLine += newLinesInMatch;

                        position += lexeme.Length;
                        matchFound = true;
                        break; 
                    }
                }

                // Error handling for unrecognized characters.
                if (!matchFound)
                {
                    tokens.Add(new Token { Type = TokenType.Unknown, Lexeme = remainingCode[0].ToString(), LineNumber = currentLine });
                    position++; 
                }
            }

            // Inject the EOF token at the very end
            tokens.Add(new Token { Type = TokenType.EOF, Lexeme = "EOF", LineNumber = currentLine });

            return tokens;
        }
    }
}
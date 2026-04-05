using System;

namespace TinyCompiler
{
    public enum TokenType
    {
        Number,
        String,
        Identifier,
        ReservedKeyword,
        AssignmentOp,
        ArithmeticOp,
        ConditionOp,
        BooleanOp,
        Delimiter,
        Comment,
        Unknown,
        EOF // Added End Of File token
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Lexeme { get; set; } = string.Empty;
        public int LineNumber { get; set; } // Tracks the line for parser error handling

        public override string ToString()
        {
            return string.Format("Line {0,-4} | {1,-20} : {2}", LineNumber, Type.ToString(), Lexeme);
        }
    }
}
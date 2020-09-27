using Microsoft.VisualStudio.Text.Differencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = System.Text.RegularExpressions.Match;

namespace CppGS.Source
{
    public class CppMembersParser
    {
        public class BadToken
        {
            public int StartIndex { get; }
            public int Length { get; set; }
            public string Type { get; }
            public int EndIndex => StartIndex + Length;
            public BadToken(int startIndex, int length, string type)
            {
                StartIndex = startIndex;
                Length = length;
                Type = type;

                Range = Enumerable.Range(startIndex, Length);
            }

            public IEnumerable<int> Range { get; }

            public bool isIncludes(BadToken token)
            {
                return StartIndex < token.StartIndex && token.StartIndex < StartIndex + Length;
            }

        }

        public class BadTokens : List<BadToken>
        {
            public void addTokens(Regex re, string text, string type)
            {
                MatchCollection matches = re.Matches(text);

                foreach (Match match in matches)
                {
                    Add(new BadToken(match.Index, match.Length, type));
                }
            }
        }

        public CppMembersParser() { }
        public string NormalizedDocument { get => document; }
        public bool Parse(string fileText)
        {
            FillBadTokens(fileText);
            return true;
        }
        private string document { get; set; }
        public void FillBadTokens(string inputCode, char placeholder = ' ')
        {
            StringBuilder code = new StringBuilder(inputCode);
            BadTokens tokens = new BadTokens();

            Regex stringRe = new Regex(@"""[^\n].*?(?<!\\)(\\\\)*[^\n\\]*""");
            Regex commentRe = new Regex(@"//.*");

            tokens.addTokens(stringRe, inputCode, "string");
            tokens.addTokens(commentRe, inputCode, "comment");

            //var isolatedTokens = tokens.Select(
            //    token =>
            //    {
            //        tokens.ForEach(
            //            parentToken =>
            //            {
            //                if (parentToken != token)
            //                {
            //                    if (parentToken.isIncludes(token) &&
            //                        token.EndIndex > parentToken.EndIndex)
            //                    {
            //                        token.Length = parentToken.EndIndex - token.StartIndex;
            //                    }
            //                }
            //            });

            //        return token;
            //    });

            var rootTokens = tokens.Where(
                token => tokens.Where(
                    otherToken => otherToken != token).All(
                    otherToken => !otherToken.isIncludes(token)));

            var childTokens = tokens.Except(rootTokens);

            foreach (BadToken t in rootTokens)
            {
                foreach (int index in t.Range)
                {
                    if (code[index] != '\r' && code[index] != '\n')
                    {
                        code[index] = placeholder;
                    }

                }
            }

            document = code.ToString();
        }
    }
}

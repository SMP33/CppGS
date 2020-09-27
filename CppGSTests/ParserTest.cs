using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;

namespace CppGSTests
{
    [TestClass]
    public class ParserTest
    {

        [TestMethod]
        public void TestNormalize()
        {


            CppGS.Source.CppMembersParser parser = new CppGS.Source.CppMembersParser();
            parser.FillBadTokens(Resources.CodeExample, '?');
            parser.FillBadTokens(parser.NormalizedDocument, '?');

            var result = parser.NormalizedDocument;

            List<string> resultLines = result.Split("\n").ToList();
            List<string> expectedLines = Resources.NormalizedCodeExample.Split("\n").ToList();

            resultLines = resultLines.Select(s =>
                { return s.Replace(" ", "_").Replace("\r", ""); }).ToList();
            expectedLines = expectedLines.Select(s =>
                { return s.Replace(" ", "_").Replace("\r", ""); }).ToList();

            string message = "\n\nDifference between lines:\n";

            int maxResultLineLength = resultLines.Aggregate(
                (s1, s2) => s1.Length > s2.Length ? s1 : s2).Length;
            int maxExpectedLineLength = expectedLines.Aggregate(
                (s1, s2) => s1.Length > s2.Length ? s1 : s2).Length;

            message += "Expected:".PadRight(maxExpectedLineLength + 5) + "Actual:\n";

            for (int i = 0; i < Math.Min(expectedLines.Count, resultLines.Count); i++)
            {
                //message += "\n";
                string delimeter = expectedLines[i] == resultLines[i] ? "|" : "~";
                message += $"{i + 1} ".PadRight(4);
                message += expectedLines[i].PadRight(maxExpectedLineLength + 1)
                    + delimeter + " " + resultLines[i].PadRight(maxResultLineLength + 1) + "|\n";
            }

            if (expectedLines.Count != resultLines.Count)
            {
                message += "\nFiles have a different number of lines:\n";
                message += $"Left: {expectedLines.Count} lines".PadRight(maxExpectedLineLength + 3)
                    + $"Right: {resultLines.Count} lines";
            }



            Assert.AreEqual(Resources.NormalizedCodeExample, result, message);


        }
    }
}

using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;
using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.Linq;
using EnvDTE80;
using System.Windows.Documents;
using EnvDTE;

namespace CppGS.Source
{
    class GSReader
    {
        public DTE2 Dte { get; set; }
        public GSReader(DTE2 dte)
        {
            Dte = dte;
            var activeDocument = Dte.ActiveDocument;

            // Checking the existence of the.cpp file
            string headerFile = activeDocument.Name;
            DefinitionFileVariants = new List<string> { headerFile };
            Regex regex = new Regex(@"(\.(h|hpp)$)");
            string sourceFile = regex.Replace(headerFile, ".cpp");
            if (headerFile != sourceFile)
            {
                // Adding the. cpp file to the list, if it exists
                if (File.Exists(regex.Replace(activeDocument.FullName, ".cpp")))
                {
                    DefinitionFileVariants.Add(sourceFile);
                }
            }

        }

        public List<string> DefinitionFileVariants { get; }
    }
}

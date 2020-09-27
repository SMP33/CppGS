using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;

namespace CppGS.Source
{
    class GSWindowViewModel
    {
        public static string DebugOutput { get; set; }
        public static void DebugWrileLine(string str="")
        {
            DebugOutput += str + "\n";
        }
        public static void DebugClear()
        {
            DebugOutput = "";
        }

        public GSWindowViewModel(DTE2 dte)
        {

            GSReader reader = new GSReader(dte);
            DefinitionFileVariants = reader.DefinitionFileVariants;
            DeclarationFile = DefinitionFileVariants.First();
            DefinitionFile = DefinitionFileVariants.Last();
        }

        public string DeclarationFile { get; }
        public string DefinitionFile { get; set; }
        public List<string> DefinitionFileVariants { get; }
        

    }
}

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using CppGS;

namespace CppGS.Source
{
    class GSWindow : DialogWindow
    {

        public GSWindow()
        {
            Title = "Getters & Setters";

            Width = 400;
            MaxHeight = 550;

            SizeToContent = System.Windows.SizeToContent.Height;

            Action closeWindow = () =>
            {
                Close();
            };

            Content = new GSWindowControl(closeWindow);
            Closed += new EventHandler(GSWindowControl.OnClosed);
        }

    }
}

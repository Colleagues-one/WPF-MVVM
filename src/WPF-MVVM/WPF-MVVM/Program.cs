using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_MVVM
{
    public static class Program 
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public static void Main()
        {
            WPF_MVVM.App app = new WPF_MVVM.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}

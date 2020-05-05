using System;
using System.Windows.Forms;
using MyProjects.Calculator.View;

namespace MyProjects.Calculator
{
    public static class ProgramCalculator
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());
        }
    }
}
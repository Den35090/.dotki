using System.Diagnostics;

namespace BankSystemDapper.SystemProgramming
{
    public class ProcessService
    {
        public void OpenReport()
        {
            Process.Start("mousepad", "report.txt"); 
        }
    }
}
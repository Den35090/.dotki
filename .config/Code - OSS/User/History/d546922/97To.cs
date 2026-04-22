using System.Diagnostics;

namespace BankSystemDapper.SystemProgramming
{
    public class ProcessService
    {
        public void OpenReport()
        {
            // На Linux откроем текстовый редактор с "отчетом"
            Process.Start("mousepad", "report.txt"); 
            // Если Mousepad нет, можно использовать xdg-open или nano
        }
    }
}
using System.Diagnostics;
using System.IO;

namespace BankSystemDapper.SystemProgramming
{
    public class ProcessService
    {
        public void RunReportProcess() // И это название тоже
        {
            string path = "report.txt";
            File.WriteAllText(path, "Отчет сформирован успешно.");
            Process.Start(new ProcessStartInfo("xdg-open", path) { UseShellExecute = true });
        }
    }
}
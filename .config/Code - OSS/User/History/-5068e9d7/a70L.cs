using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiskMonitorApp
{
    class Program
    {
        static string logPath = "disk_monitor_log.txt";
        static bool isMonitoring = true;

        static void Main()
        {
            Console.Title = "Console Disk Monitor";

            Thread alertThread = new Thread(BackgroundSpaceCheck);
            alertThread.IsBackground = true;
            alertThread.Start();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== МОНИТОР ДИСКОВ =====");
                Console.WriteLine("1 - Список всех дисков");
                Console.WriteLine("2 - Детальная информация");
                Console.WriteLine("3 - Свободное место (кратко)");
                Console.WriteLine("4 - Сохранить отчет в файл");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("==========================");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowAllDrives(); break;
                    case "2": ShowDetailedInfo(); break;
                    case "3": ShowFreeSpace(); break;
                    case "4": SaveReport(); break;
                    case "0": isMonitoring = false; return;
                    default: Console.WriteLine("Ошибка ввода"); Pause(); break;
                }
            }
        }

        static void ShowAllDrives()
        {
            Console.Clear();
            Console.WriteLine("{0,-5} | {1,-10} | {2,-10} | {3,-15}", "Имя", "Тип", "Формат", "Готовность");
            Console.WriteLine(new string('-', 50));

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Console.WriteLine("{0,-5} | {1,-10} | {2,-10} | {3,-15}", 
                    drive.Name, 
                    drive.DriveType, 
                    drive.IsReady ? drive.DriveFormat : "N/A",
                    drive.IsReady ? "Готов" : "Не готов");
            }
            Pause();
        }

        static void ShowDetailedInfo()
        {
            Console.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Диск: {drive.Name}");
                    Console.WriteLine($"  Метка тома: {drive.VolumeLabel}");
                    Console.WriteLine($"  Тип системы: {drive.DriveFormat}");
                    Console.WriteLine($"  Всего памяти: {FormatBytes(drive.TotalSize)}");
                    Console.WriteLine($"  Свободно: {FormatBytes(drive.TotalFreeSpace)}");
                    Console.WriteLine($"  Доступно пользователю: {FormatBytes(drive.AvailableFreeSpace)}");
                    Console.WriteLine(new string('-', 30));
                }
            }
            Pause();
        }

        static void ShowFreeSpace()
        {
            Console.Clear();
            Console.WriteLine("Заполнение дисков:");
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                double percentFree = (double)drive.TotalFreeSpace / drive.TotalSize * 100;
                Console.WriteLine($"{drive.Name} {FormatBytes(drive.TotalFreeSpace)} свободно ({percentFree:F1}%)");
            }
            Pause();
        }

        static void SaveReport()
        {
            try
            {
                string reportName = $"DiskReport_{DateTime.Now:yyyyMMdd_HHmm}.txt";
                using (StreamWriter sw = new StreamWriter(reportName))
                {
                    sw.WriteLine($"Отчет о состоянии дисков от {DateTime.Now}");
                    sw.WriteLine(new string('=', 40));
                    foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
                    {
                        sw.WriteLine($"Диск {drive.Name} ({drive.DriveFormat})");
                        sw.WriteLine($"  Всего: {FormatBytes(drive.TotalSize)}");
                        sw.WriteLine($"  Свободно: {FormatBytes(drive.TotalFreeSpace)}");
                        sw.WriteLine();
                    }
                }
                Console.WriteLine($"Отчет сохранен: {reportName}");
                LogAction($"Создан отчет: {reportName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения: " + ex.Message);
                LogAction("Ошибка сохранения отчета: " + ex.Message);
            }
            Pause();
        }

        static void BackgroundSpaceCheck()
{
    while (isMonitoring)
    {
        try
        {
            var drives = DriveInfo.GetDrives()
                .Where(d => d.IsReady && d.DriveType == DriveType.Fixed);

            foreach (DriveInfo drive in drives)
            {
                try
                {
                    double percentFree = (double)drive.TotalFreeSpace / drive.TotalSize * 100;
                    if (percentFree < 10)
                    {
                        LogAction($"ВНИМАНИЕ: На диске {drive.Name} осталось мало места ({percentFree:F1}%)");
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    continue; 
                }
            }
        }
        catch (Exception ex)
        {
            LogAction($"Ошибка мониторинга: {ex.Message}");
        }
        
        Thread.Sleep(60000); 
    }
}

        static string FormatBytes(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double size = bytes;
            int unitIndex = 0;
            while (size >= 1024 && unitIndex < units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }
            return $"{size:F2} {units[unitIndex]}";
        }

        static void LogAction(string message)
        {
            try
            {
                File.AppendAllText(logPath, $"[{DateTime.Now}] {message}{Environment.NewLine}");
            }
            catch {}
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
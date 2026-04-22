using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace ProcessLibrary
{
    public class ProcessManager
    {
        public void StartProcess(string processName) => Process.Start(processName);

        public List<ProcessInfo> GetProcesses(string filter)
        {
            return Process.GetProcesses()
                .Where(p => p.ProcessName.Contains(filter, System.StringComparison.OrdinalIgnoreCase))
                .Select(p => new ProcessInfo { 
                    Name = p.ProcessName, 
                    Id = p.Id, 
                    Memory = p.WorkingSet64 / 1024 
                })
                .ToList();
        }

        public void KillProcessById(int id)
        {
            var p = Process.GetProcessById(id);
            p.Kill();
        }
    }

    public class ProcessInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Memory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    internal class BackgroundTaskCoordinator : Catalogo.util.singleton<BackgroundTaskCoordinator>
    {
        private System.Collections.Generic.Dictionary<string, Catalogo.util.BackgroundTasks.BackgroundTaskBase> _tasks = new Dictionary<string,BackgroundTaskBase>();
        
        internal void addTask(string taskName, BackgroundTaskBase task)
        {
            util.errorHandling.ErrorLogger.LogMessage("Iniciando proceso " + taskName);
            _tasks[taskName]=task;
        }

        internal void shutdownTasks()
        {
            foreach (string taskName in _tasks.Keys)
            {
                util.errorHandling.ErrorLogger.LogMessage("Finalizando proceso " + taskName);
                _tasks[taskName].shutdown();
            }
        }
    }
}
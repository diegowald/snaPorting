using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    internal class BackgroundTaskCoordinator : Catalogo.util.singleton<BackgroundTaskCoordinator>
    {
        private System.Collections.Generic.Dictionary<string, Catalogo.util.BackgroundTasks.BackgroundTaskBase> _tasks;
        
        public void addTask(string taskName, BackgroundTaskBase task)
        {
            _tasks[taskName]=task;
        }

        public void shutdownTasks()
        {
            foreach(BackgroundTaskBase task in _tasks.Values)
            {
                task.shutdown();
            }
        }
    }
}
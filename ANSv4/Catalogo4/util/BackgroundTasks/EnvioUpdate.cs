using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public sealed class EnvioUpdate : BackgroundTaskBase
    {
        public EnvioUpdate(JOB_TYPE jobType)
            : base(jobType)
        {
        }

        public override void execute(ref bool cancel)
        {
            throw new NotImplementedException();
        }

        public override void cancelled()
        {
            throw new NotImplementedException();
        }

        public override void finished()
        {
            throw new NotImplementedException();
        }
    }
}
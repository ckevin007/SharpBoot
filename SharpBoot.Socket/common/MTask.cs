using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SharpBoot.Sockets.common
{
    public class MTask
    {
        private Task Task { get; set; }
        public Action Action { get; set; }
        private TaskFlag lastTaskFlag;
        public TaskFlag CreateNewTaskFlag()
        {
            lastTaskFlag = new TaskFlag();
            return lastTaskFlag;
        }

        public void Start()
        {
            if (lastTaskFlag == null) lastTaskFlag = new TaskFlag();
            Task = new Task(Action, lastTaskFlag.Token);
            Task.Start();
        }
        public void Cancel()
        {
            if (lastTaskFlag == null) return;
            lastTaskFlag.Cancel();
        }

    }

    public class TaskFlag
    {
        private CancellationTokenSource tokenSource;
        public CancellationToken Token { get; set; }
        public TaskFlag()
        {
            tokenSource = new CancellationTokenSource();
            Token = tokenSource.Token;
        }
        public void Cancel()
        {
            tokenSource.Cancel();
        }
        public bool Continue => !Token.IsCancellationRequested;
    }
}

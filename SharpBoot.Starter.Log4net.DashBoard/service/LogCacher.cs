using Castle.Core.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Log4netDashBoard.Demo.service
{
    public class LogCacher
    {
        private LogCacher()
        {

        }

        public long CacheSize { get; set; }

        public static LogCacher Instance { get; } = new LogCacher();

        private ConcurrentQueue<string> queue = new ConcurrentQueue<string>();


        public void AddLog(string str)
        {
            if (queue.Count > CacheSize) queue.TryDequeue(out _);
            queue.Enqueue(str);
        }


        public void Clear()
        {
            this.queue.Clear();
        }


        public string[] Query(string filterKeyword, int fetchCount)
        {
            var queryable = this.queue.AsQueryable();
            if (!string.IsNullOrEmpty(filterKeyword)) queryable = queryable.Where(a => a.Contains(filterKeyword));
            if (fetchCount > 0) queryable = queryable.Skip(this.queue.Count - fetchCount);
            var tmpList = queryable.ToArray();
            return tmpList;
        }
    }
}

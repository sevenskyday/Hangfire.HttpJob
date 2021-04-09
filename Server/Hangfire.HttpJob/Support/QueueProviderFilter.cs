using System.Collections.Generic;
using System.Linq;
using Hangfire.Common;
using Hangfire.HttpJob.Server;

namespace Hangfire.HttpJob.Support
{
    /// <summary>
    ///     自定义queue名称
    /// </summary>
    public class QueueProviderFilter : IJobFilterProvider
    {
        public IEnumerable<Common.JobFilter> GetFilters(Job job)
        {
            if (job == null)
                return new Common.JobFilter[] { };
            if (!(job.Args.FirstOrDefault() is HttpJobItem arg) || string.IsNullOrEmpty(arg.QueueName)) return new Common.JobFilter[] { };


            return new[]
            {
                new Common.JobFilter(
                    new QueueAttribute(arg.QueueName.ToLower()),
                    JobFilterScope.Method, null
                )
            };
        }
    }
}
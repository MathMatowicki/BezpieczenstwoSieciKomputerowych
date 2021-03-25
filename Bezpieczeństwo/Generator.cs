using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bezpieczeństwo
{
    public class Generator : IHostedService
    {

        public IWorker Worker;

        public long GetOutput()
        {
            return Worker.GetOutput();
        }

        public Generator(IWorker worker)
        {
            Worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
using Bezpieczeństwo.Algorithms;
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

        public Generator(IWorker worker)
        {
            Worker = worker;
        }

        public Lsfr GetOutput()
        {
            return Worker.GetOutput();
        }
        
        public void SetLsfr(int[] bytes)
        {
            Worker.SetLsfr(bytes);
        }
        public void SetActive(bool b)
        {
            Worker.SetActive(b);
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
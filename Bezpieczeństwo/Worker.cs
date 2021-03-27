using Bezpieczeństwo.Algorithms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bezpieczeństwo
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> logger;
        private int number;
        private bool active = false;
        public void SetActive(bool b)
        {
            this.active = b;
        }
        private Lsfr lsfr;
        public void SetLsfr(int[] indexes)
        {
            lsfr = new Lsfr(indexes);
        }

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
        }

        public Lsfr GetOutput()
        {
            return lsfr;
        }
        public void DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (active)
                {
                    lsfr.Iteration();
                }
                else
                {
                    Console.WriteLine("Działam");
                }
            }
        }
    }
}

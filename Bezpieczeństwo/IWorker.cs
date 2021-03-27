using Bezpieczeństwo.Algorithms;
using System.Threading;

namespace Bezpieczeństwo
{
    public interface IWorker
    {
        void DoWork(CancellationToken cancellationToken);
        Lsfr GetOutput();
        void SetActive(bool b);
        void SetLsfr(int[] indexes);
    }
}